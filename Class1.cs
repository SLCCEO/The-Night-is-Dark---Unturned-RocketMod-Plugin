using System;
using System.Collections.Generic;
using Rocket.API;
using Rocket.Core.Plugins;
using Rocket.Core.Commands;
using Rocket.Unturned;
using Rocket.Unturned.Events;
using Rocket.Unturned.Player;
using Rocket.Unturned.Chat;
using SDG.Unturned;
using UnityEngine;
using Logger = Rocket.Core.Logging.Logger;
using Rocket.Core;
using Steamworks;

// Use an alias to resolve the CS0433 collision between Steamworks DLLs
using SteamID = Steamworks.CSteamID;

namespace TheNightIsDark
{
    public class TheNightIsDarkConfiguration : IRocketPluginConfiguration
    {
        public float ZombieDamageMultiplier;
        public List<string> BlockedCommands;
        public string BlockMessage;
        public string NightStartMessage;

        public void LoadDefaults()
        {
            ZombieDamageMultiplier = 2.0f;
            BlockedCommands = new List<string> { "home", "tpa", "tpask", "tpaaccept" };
            BlockMessage = "The night is too dark to find your way home...";
            NightStartMessage = "The Night is Dark... the shadows grow stronger.";
        }
    }

    public class TheNightIsDark : RocketPlugin<TheNightIsDarkConfiguration>
    {
        public static TheNightIsDark Instance;
        private bool _wasNight;

        protected override void Load()
        {
            Instance = this;

            _wasNight = IsNight();

            // Switched from UnturnedPlayerEvents to UnturnedEvents to fix the missing definition error
            UnturnedEvents.OnPlayerDamaged += OnPlayerDamaged;

            R.Commands.OnExecuteCommand += OnExecuteCommand;

            Logger.Log("The Night is Dark (RocketMod) has loaded!");
        }

        protected override void Unload()
        {
            UnturnedEvents.OnPlayerDamaged -= OnPlayerDamaged;
            R.Commands.OnExecuteCommand -= OnExecuteCommand;

            Logger.Log("The Night is Dark has been unloaded.");
        }

        private bool IsNight()
        {
            // Reliable time check for night: Dawn ends at 0.25, Dusk starts at 0.75
            return LevelLighting.time < 0.25f || LevelLighting.time > 0.75f;
        }

        public void FixedUpdate()
        {
            if (State != PluginState.Loaded) return;

            bool isNightNow = IsNight();

            if (isNightNow && !_wasNight)
            {
                ChatManager.say(Configuration.Instance.NightStartMessage, Color.red);
            }

            _wasNight = isNightNow;
        }

        // Updated signature based on successful build requirements
        private void OnPlayerDamaged(
            UnturnedPlayer player,
            ref EDeathCause cause,
            ref ELimb limb,
            ref UnturnedPlayer killer,
            ref UnityEngine.Vector3 direction,
            ref float damage,
            ref float times,
            ref bool canDamage)
        {
            if (cause == EDeathCause.ZOMBIE && IsNight())
            {
                float multi = Configuration.Instance.ZombieDamageMultiplier;
                // Using the updated damage calculation logic
                damage = Mathf.Clamp(damage * multi, 0f, 255f);
            }
        }

        private void OnExecuteCommand(IRocketPlayer player, IRocketCommand command, ref bool cancel)
        {
            if (IsNight())
            {
                string cmd = command.Name.ToLower();

                if (Configuration.Instance.BlockedCommands.Contains(cmd))
                {
                    cancel = true;

                    if (player is UnturnedPlayer unturnedPlayer)
                    {
                        UnturnedChat.Say(unturnedPlayer, Configuration.Instance.BlockMessage, Color.red);
                    }
                }
            }
        }
    }
}