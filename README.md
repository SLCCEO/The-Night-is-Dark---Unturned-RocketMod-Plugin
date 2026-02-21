The Night is Dark - Unturned RocketMod Plugin

A hardcore survival plugin for Unturned (RocketMod) that makes the night cycle significantly more dangerous.

Features

Zombie Power Spike: Zombies deal customizable extra damage during the night (Default: 2.0x).

Command Blocking: Prevents players from using teleportation commands like /home or /tpa during the night to prevent easy escapes.

Global Announcements: Alerts the entire server when the sun sets and the danger begins.

Fully Configurable: Easily adjust multipliers, blocked commands, and messages via the auto-generated XML config.

Installation

Download the compiled .dll.

Drop it into your server's Plugins folder.

Restart the server to generate the configuration file.

Configuration

<TheNightIsDarkConfiguration>
  <ZombieDamageMultiplier>2.0</ZombieDamageMultiplier>
  <BlockedCommands>
    <string>home</string>
    <string>tpa</string>
  </BlockedCommands>
  <BlockMessage>The night is too dark to find your way home...</BlockMessage>
  <NightStartMessage>The Night is Dark... the shadows grow stronger.</NightStartMessage>
</TheNightIsDarkConfiguration>


Requirements

Unturned

RocketMod

License

This project is licensed under the Creative Commons Attribution-NonCommercial-NoDerivatives 4.0 International (CC BY-NC-ND 4.0) License.

You are NOT permitted to sell this software or distribute modified versions of the code. See the LICENSE file in this repository for details.
