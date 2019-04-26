using Smod2;
using Smod2.Attributes;
using Smod2.EventHandlers;
using Smod2.Events;
using System;
using System.Collections.Generic;

namespace AdvancedPermissions
{
    [PluginDetails(
        author = "Moto (https://github.com/motoenduroboy)",
        name = "AdvancedPermissions",
        description = "More control of permissions",
        id = "moto.advanced.permissions",
        version = "0.0.4",
        SmodMajor = 3,
        SmodMinor = 4,
        SmodRevision = 0
    )]
    public class AdvancedPermissions : Plugin
    {
        public override void OnDisable()
        {
            this.Info(this.Details.name + " was disabled");
        }

        public override void OnEnable()
        {
            this.Info(this.Details.name + " has loaded");
        }

        public override void Register() {
            this.AddConfig(new Smod2.Config.ConfigSetting("AP_DISABLE", false, false, true, "AP_DISABLE"));
            this.AddConfig(new Smod2.Config.ConfigSetting("AP_RM_*", new string[] { string.Empty }, false, true, "AP_RM_*"));
            // Player Info
            this.AddConfig(new Smod2.Config.ConfigSetting("AP_RM_PLAYER_INFO_REQUEST", new string[] { string.Empty }, false, true, "AP_RM_PLAYER_INFO_REQUEST"));
            this.AddConfig(new Smod2.Config.ConfigSetting("AP_RM_PLAYER_INFO_REQUEST_IP", new string[] { string.Empty }, false, true, "AP_RM_PLAYER_INFO_REQUEST_IP"));
            this.AddConfig(new Smod2.Config.ConfigSetting("AP_RM_PLAYER_INFO_REQUEST_AUTH", new string[] { string.Empty }, false, true, "AP_RM_PLAYER_INFO_REQUEST_AUTH"));
            // Kick And Banning
            this.AddConfig(new Smod2.Config.ConfigSetting("AP_RM_PLAYER_KICK", new string[] { string.Empty }, false, true, "AP_RM_PLAYER_KICK"));
            this.AddConfig(new Smod2.Config.ConfigSetting("AP_RM_PLAYER_MINUTES", new string[] { string.Empty }, false, true, "AP_RM_PLAYER_MINUTES"));
            this.AddConfig(new Smod2.Config.ConfigSetting("AP_RM_PLAYER_HOURS", new string[] { string.Empty }, false, true, "AP_RM_PLAYER_HOURS"));
            this.AddConfig(new Smod2.Config.ConfigSetting("AP_RM_PLAYER_DAYS", new string[] { string.Empty }, false, true, "AP_RM_PLAYER_DAYS"));
            this.AddConfig(new Smod2.Config.ConfigSetting("AP_RM_PLAYER_YEARS", new string[] { string.Empty }, false, true, "AP_RM_PLAYER_YEARS"));
            // ForceClass
            this.AddConfig(new Smod2.Config.ConfigSetting("AP_RM_PLAYER_FORCECLASS", new string[] { string.Empty }, false, true, "AP_RM_PLAYER_FORCECLASS"));
            // Give
            this.AddConfig(new Smod2.Config.ConfigSetting("AP_RM_PLAYER_GIVE_KEYCARD", new string[] { string.Empty }, false, true, "AP_RM_PLAYER_GIVE_KEYCARD")); 
            this.AddConfig(new Smod2.Config.ConfigSetting("AP_RM_PLAYER_GIVE_TOOL", new string[] { string.Empty }, false, true, "AP_RM_PLAYER_GIVE_TOOL")); 
            this.AddConfig(new Smod2.Config.ConfigSetting("AP_RM_PLAYER_GIVE_WEAPON", new string[] { string.Empty }, false, true, "AP_RM_PLAYER_GIVE_WEAPON")); 
            this.AddConfig(new Smod2.Config.ConfigSetting("AP_RM_PLAYER_GIVE_GRENADE", new string[] { string.Empty }, false, true, "AP_RM_PLAYER_GIVE_GRENADE")); 
            this.AddConfig(new Smod2.Config.ConfigSetting("AP_RM_PLAYER_GIVE_AMMO", new string[] { string.Empty }, false, true, "AP_RM_PLAYER_GIVE_AMMO")); 
            this.AddConfig(new Smod2.Config.ConfigSetting("AP_RM_PLAYER_GIVE_OTHER", new string[] { string.Empty }, false, true, "AP_RM_PLAYER_GIVE_OTHER"));
            // Admin Tools
            this.AddConfig(new Smod2.Config.ConfigSetting("AP_RM_ADMIN_OVERWATCH", new string[] { string.Empty }, false, true, "AP_RM_ADMIN_OVERWATCH")); 
            this.AddConfig(new Smod2.Config.ConfigSetting("AP_RM_ADMIN_GOD", new string[] { string.Empty }, false, true, "AP_RM_ADMIN_GOD")); 
            this.AddConfig(new Smod2.Config.ConfigSetting("AP_RM_ADMIN_BYPASS", new string[] { string.Empty }, false, true, "AP_RM_ADMIN_BYPASS")); 
            this.AddConfig(new Smod2.Config.ConfigSetting("AP_RM_ADMIN_BRING", new string[] { string.Empty }, false, true, "AP_RM_ADMIN_BRING")); 
            this.AddConfig(new Smod2.Config.ConfigSetting("AP_RM_ADMIN_GOTO", new string[] { string.Empty }, false, true, "AP_RM_ADMIN_GOTO")); 
            this.AddConfig(new Smod2.Config.ConfigSetting("AP_RM_ADMIN_HEAL", new string[] { string.Empty }, false, true, "AP_RM_ADMIN_HEAL")); 
            this.AddConfig(new Smod2.Config.ConfigSetting("AP_RM_ADMIN_LOCKDOWN", new string[] { string.Empty }, false, true, "AP_RM_ADMIN_LOCKDOWN")); 
            // Door Management
            this.AddConfig(new Smod2.Config.ConfigSetting("AP_RM_DOORS_OPEN_ALL", new string[] { string.Empty }, false, true, "AP_RM_DOORS_OPEN_ALL"));
            this.AddConfig(new Smod2.Config.ConfigSetting("AP_RM_DOORS_OPEN_ALL_LISTED", new string[] { string.Empty }, false, true, "AP_RM_DOORS_OPEN_ALL_LISTED"));
            this.AddConfig(new Smod2.Config.ConfigSetting("AP_RM_DOORS_OPEN_ALL_NOT_LISTED", new string[] { string.Empty }, false, true, "AP_RM_DOORS_OPEN_ALL_NOT_LISTED"));
            this.AddConfig(new Smod2.Config.ConfigSetting("AP_RM_DOORS_OPEN_SINGLE", new string[] { string.Empty }, false, true, "AP_RM_DOORS_OPEN_SINGLE"));
            this.AddConfig(new Smod2.Config.ConfigSetting("AP_RM_DOORS_CLOSE_ALL", new string[] { string.Empty }, false, true, "AP_RM_DOORS_CLOSE_ALL"));
            this.AddConfig(new Smod2.Config.ConfigSetting("AP_RM_DOORS_CLOSE_ALL_LISTED", new string[] { string.Empty }, false, true, "AP_RM_DOORS_CLOSE_ALL_LISTED"));
            this.AddConfig(new Smod2.Config.ConfigSetting("AP_RM_DOORS_CLOSE_ALL_NOT_LISTED", new string[] { string.Empty }, false, true, "AP_RM_DOORS_CLOSE_ALL_NOT_LISTED"));
            this.AddConfig(new Smod2.Config.ConfigSetting("AP_RM_DOORS_CLOSE_SINGLE", new string[] { string.Empty }, false, true, "AP_RM_DOORS_CLOSE_SINGLE"));
            this.AddConfig(new Smod2.Config.ConfigSetting("AP_RM_DOORS_LOCK_ALL", new string[] { string.Empty }, false, true, "AP_RM_DOORS_LOCK_ALL"));
            this.AddConfig(new Smod2.Config.ConfigSetting("AP_RM_DOORS_LOCK_ALL_LISTED", new string[] { string.Empty }, false, true, "AP_RM_DOORS_LOCK_ALL_LISTED"));
            this.AddConfig(new Smod2.Config.ConfigSetting("AP_RM_DOORS_LOCK_ALL_NOT_LISTED", new string[] { string.Empty }, false, true, "AP_RM_DOORS_LOCK_ALL_NOT_LISTED"));
            this.AddConfig(new Smod2.Config.ConfigSetting("AP_RM_DOORS_LOCK_SINGLE", new string[] { string.Empty }, false, true, "AP_RM_DOORS_LOCK_SINGLE"));;
            this.AddConfig(new Smod2.Config.ConfigSetting("AP_RM_DOORS_UNLOCK_ALL", new string[] { string.Empty }, false, true, "AP_RM_DOORS_UNLOCK_ALL"));
            this.AddConfig(new Smod2.Config.ConfigSetting("AP_RM_DOORS_UNLOCK_ALL_LISTED", new string[] { string.Empty }, false, true, "AP_RM_DOORS_UNLOCK_ALL_LISTED"));
            this.AddConfig(new Smod2.Config.ConfigSetting("AP_RM_DOORS_UNLOCK_ALL_NOT_LISTED", new string[] { string.Empty }, false, true, "AP_RM_DOORS_UNLOCK_ALL_NOT_LISTED"));
            this.AddConfig(new Smod2.Config.ConfigSetting("AP_RM_DOORS_UNLOCK_SINGLE", new string[] { string.Empty }, false, true, "AP_RM_DOORS_UNLOCK_SINGLE"));
            this.AddConfig(new Smod2.Config.ConfigSetting("AP_RM_DOORS_DESTROY_ALL", new string[] { string.Empty }, false, true, "AP_RM_DOORS_DESTROY_ALL"));
            this.AddConfig(new Smod2.Config.ConfigSetting("AP_RM_DOORS_DESTROY_ALL_LISTED", new string[] { string.Empty }, false, true, "AP_RM_DOORS_DESTROY_ALL_LISTED"));
            this.AddConfig(new Smod2.Config.ConfigSetting("AP_RM_DOORS_DESTROY_ALL_NOT_LISTED", new string[] { string.Empty }, false, true, "AP_RM_DOORS_DESTROY_ALL_NOT_LISTED"));
            this.AddConfig(new Smod2.Config.ConfigSetting("AP_RM_DOORS_DESTROY_SINGLE", new string[] { string.Empty }, false, true, "AP_RM_DOORS_DESTROY_ALL_NOT_LISTED"));
            this.AddConfig(new Smod2.Config.ConfigSetting("AP_RM_DOORS_TELEPORT", new string[] { string.Empty }, false, true, "AP_RM_DOORS_TELEPORT"));
            // Player Management
            this.AddConfig(new Smod2.Config.ConfigSetting("AP_RM_PLAYER_MUTE", new string[] { string.Empty }, false, true, "AP_RM_PLAYER_MUTE"));
            this.AddConfig(new Smod2.Config.ConfigSetting("AP_RM_PLAYER_MUTE_INTERCOM", new string[] { string.Empty }, false, true, "AP_RM_PLAYER_MUTE_INTERCOM"));
            // Server Events
            this.AddConfig(new Smod2.Config.ConfigSetting("AP_RM_RESPAWN_MIF", new string[] { string.Empty }, false, true, "AP_RM_RESPAWN_MIF"));
            this.AddConfig(new Smod2.Config.ConfigSetting("AP_RM_RESPAWN_CI", new string[] { string.Empty }, false, true, "AP_RM_RESPAWN_CI"));
            this.AddConfig(new Smod2.Config.ConfigSetting("AP_RM_RESTART_ROUND", new string[] { string.Empty }, false, true, "AP_RM_RESTART_ROUND"));
            this.AddConfig(new Smod2.Config.ConfigSetting("AP_RM_FORCE_START", new string[] { string.Empty }, false, true, "AP_RM_FORCE_START"));
            this.AddConfig(new Smod2.Config.ConfigSetting("AP_RM_KICK_UNCONNECTED", new string[] { string.Empty }, false, true, "AP_RM_KICK_UNCONNECTED"));
            this.AddConfig(new Smod2.Config.ConfigSetting("AP_RM_INTERCOM_TIMEOUT", new string[] { string.Empty }, false, true, "AP_RM_INTERCOM_TIMEOUT"));
            this.AddConfig(new Smod2.Config.ConfigSetting("AP_RM_INTERCOMM_RESET", new string[] { string.Empty }, false, true, "AP_RM_INTERCOMM_RESET"));
            this.AddConfig(new Smod2.Config.ConfigSetting("AP_RM_DETONATION_START", new string[] { string.Empty }, false, true, "AP_RM_DETONATION_START"));
            this.AddConfig(new Smod2.Config.ConfigSetting("AP_RM_DETONATION_CANCEL", new string[] { string.Empty }, false, true, "AP_RM_DETONATION_CANCEL"));
            this.AddConfig(new Smod2.Config.ConfigSetting("AP_RM_DETONATION_INSTANT", new string[] { string.Empty }, false, true, "AP_RM_DETONATION_INSTANT"));
            // Server Configs
            this.AddConfig(new Smod2.Config.ConfigSetting("AP_RM_SETCONFIG_FRIENDLY_FIRE", new string[] { string.Empty }, false, true, "AP_RM_SETCONFIG_FRIENDLY_FIRE"));
            this.AddConfig(new Smod2.Config.ConfigSetting("AP_RM_SETCONFIG_SPAWNPROTECT_DISABLE", new string[] { string.Empty }, false, true, "AP_RM_SETCONFIG_SPAWNPROTECT_DISABLE"));
            this.AddConfig(new Smod2.Config.ConfigSetting("AP_RM_SETCONFIG_PLAYERLIST_TITLE", new string[] { string.Empty }, false, true, "AP_RM_SETCONFIG_PLAYERLIST_TITLE"));
            this.AddConfig(new Smod2.Config.ConfigSetting("AP_RM_SETCONFIG_PDREFRESH_EXIT", new string[] { string.Empty }, false, true, "AP_RM_SETCONFIG_PDREFRESH_EXIT"));
            this.AddConfig(new Smod2.Config.ConfigSetting("AP_RM_SETCONFIG_SPAWNPROTECT_TIME", new string[] { string.Empty }, false, true, "AP_RM_SETCONFIG_SPAWNPROTECT_TIME"));
            // Commands
            this.AddConfig(new Smod2.Config.ConfigSetting("AP_COMMANDS_*", new string[] { string.Empty }, false, true, "AP_COMMANDS_*"));
            this.AddConfig(new Smod2.Config.ConfigSetting("AP_COMMANDS", new Dictionary<string, string>(), false, true, "AP_COMMANDS"));
            // Hierarchy
            this.AddConfig(new Smod2.Config.ConfigSetting("AP_HIERARCHY_ENABLE", new bool(), false, true, "AP_HIERARCHY_ENABLE"));
            this.AddConfig(new Smod2.Config.ConfigSetting("AP_HIERARCHY", new Dictionary<string, string>(), false, true, "AP_HIERARCHY"));

            if (this.GetConfigBool("AP_DISABLE"))
            {
                this.Info("AP_DISABLE is enabled, Advance Permissions will not start");
                return;
            }
            
            this.AddEventHandler(typeof(IEventHandlerAdminQuery), new OnAdminQueryHandler(this), Priority.Highest);
        }
	}
}
