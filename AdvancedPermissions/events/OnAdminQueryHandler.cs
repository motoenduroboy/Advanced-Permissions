using Smod2;
using Smod2.API;
using Smod2.EventHandlers;
using Smod2.Events;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AdvancedPermissions
{
    public class OnAdminQueryHandler : IEventHandlerAdminQuery, IEventHandlerRoundStart
    {
        private readonly AdvancedPermissions plugin;

        private IConfigFile config;

        public OnAdminQueryHandler(AdvancedPermissions plugin, IConfigFile config)
        {
            this.plugin = plugin;
            this.config = config;
        }

        public void OnAdminQuery(AdminQueryEvent ev) {
            if (ev.Query == "REQUEST_DATA PLAYER_LIST SILENT") return;
            else if (ev.Query.Contains("REQUEST_DATA SHORT-PLAYER ")) handler(ev, "AP_RM_PlAYER_INFO_REQUEST");
            else if (ev.Query.Contains("REQUEST_DATA PLAYER ")) handler(ev, "AP_RM_PlAYER_INFO_REQUEST_IP");
            else if (ev.Query.Contains("REQUEST_DATA AUTH ")) handler(ev, "AP_RM_PlAYER_INFO_REQUEST_AUTH");

            else if (ev.Query.Contains("BAN ")) KickBanHandler(ev);

            else if (ev.Query.Contains("FORCECLASS ")) handler(ev, "AP_RM_PLAYER_FORCECLASS");

            else if (ev.Query.Contains("give ")) GiveHandler(ev);

            else if (ev.Query.Contains("overwatch ")) handler(ev, "AP_RM_ADMIN_OVERWATCH");
            else if (ev.Query.Contains("god ")) handler(ev, "AP_RM_ADMIN_GOD");
            else if (ev.Query.Contains("bypass ")) handler(ev, "AP_RM_ADMIN_BYPASS");
            else if (ev.Query.Contains("bring ")) handler(ev, "AP_RM_ADMIN_BRING");
            else if (ev.Query.Contains("goto ")) handler(ev, "AP_RM_ADMIN_GOTO");
            else if (ev.Query.Contains("heal ")) handler(ev, "AP_RM_ADMIN_HEAL");
            else if (ev.Query.Contains("LOCKDOWN ")) handler(ev, "AP_RM_ADMIN_LOCKDOWN");

            else if (ev.Query == "open **") handler(ev, "AP_RM_DOORS_OPEN_ALL"); // ALL
            else if (ev.Query == "open *") handler(ev, "AP_RM_DOORS_OPEN_ALL_LISTED");  // ALL Listed
            else if (ev.Query == "open !*") handler(ev, "AP_RM_DOORS_OPEN_ALL_NOT_LISTED"); // ALL Not listed
            else if (ev.Query.Contains("open ")) handler(ev, "AP_RM_DOORS_OPEN_SINGLE"); // Single
            else if (ev.Query == "close **") handler(ev, "AP_RM_DOORS_CLOSE_ALL"); // ALL
            else if (ev.Query == "close *") handler(ev, "AP_RM_DOORS_CLOSE_ALL_LISTED");  // ALL Listed
            else if (ev.Query == "close !*") handler(ev, "AP_RM_DOORS_CLOSE_ALL_NOT_LISTED"); // ALL Not listed
            else if (ev.Query.Contains("close ")) handler(ev, "AP_RM_DOORS_CLOSE_SINGLE"); // Single
            else if (ev.Query == "lock **") handler(ev, "AP_RM_DOORS_LOCK_ALL"); // ALL
            else if (ev.Query == "lock *") handler(ev, "AP_RM_DOORS_LOCK_ALL_LISTED");  // ALL Listed
            else if (ev.Query == "lock !*") handler(ev, "AP_RM_DOORS_LOCK_ALL_NOT_LISTED"); // ALL Not listed
            else if (ev.Query.Contains("lock ")) handler(ev, "AP_RM_DOORS_LOCK_SINGLE"); // Single
            else if (ev.Query == "unlock **") handler(ev, "AP_RM_DOORS_UNLOCK_ALL"); // ALL
            else if (ev.Query == "unlock *") handler(ev, "AP_RM_DOORS_UNLOCK_ALL_LISTED");  // ALL Listed
            else if (ev.Query == "unlock !*") handler(ev, "AP_RM_DOORS_UNLOCK_ALL_NOT_LISTED"); // ALL Not listed
            else if (ev.Query.Contains("unlock ")) handler(ev, "AP_RM_DOORS_UNLOCK_SINGLE"); // Single
            else if (ev.Query == "destroy **") handler(ev, "AP_RM_DOORS_DESTROY_ALL"); // ALL
            else if (ev.Query == "destroy *") handler(ev, "AP_RM_DOORS_DESTROY_ALL_LISTED");  // ALL Listed
            else if (ev.Query == "destroy !*") handler(ev, "AP_RM_DOORS_DESTROY_ALL_NOT_LISTED"); // ALL Not listed
            else if (ev.Query.Contains("destroy ")) handler(ev, "AP_RM_DOORS_DESTROY_SINGLE"); // Single
            else if (ev.Query.Contains("teleport ")) handler(ev, "AP_RM_DOORS_TELEPORT");

            else if (ev.Query.Contains("mute ")) handler(ev, "AP_RM_PLAYER_MUTE");
            else if (ev.Query.Contains("unmute ")) handler(ev, "AP_RM_PLAYER_MUTE");
            else if (ev.Query.Contains("imute ")) handler(ev, "AP_RM_PLAYER_MUTE_INTERCOM");
            else if (ev.Query.Contains("iunmute ")) handler(ev, "AP_RM_PLAYER_MUTE_INTERCOM");

            else if (ev.Query == "SERVER_EVENT FORCE_MTF_RESPAWN") handler(ev, "AP_RM_RESPAWN_MIF");
            else if (ev.Query == "SERVER_EVENT FORCE_CI_RESPAWN") handler(ev, "AP_RM_RESPAWN_CI");
            else if (ev.Query == "SERVER_EVENT ROUND_RESTART") handler(ev, "AP_RM_RESTART_ROUND");
            else if (ev.Query == "FORCESTART") handler(ev, "AP_RM_FORCE_START");
            else if (ev.Query == "SERVER_EVENT TERMINATE_UNCONN") handler(ev, "AP_RM_KICK_UNCONNECTED");
            else if (ev.Query == "INTERCOM-TIMEOUT") handler(ev, "AP_RM_INTERCOM_TIMEOUT");
            else if (ev.Query == "INTERCOM-RESET") handler(ev, "AP_RM_INTERCOMM_RESET");
            else if (ev.Query == "SERVER_EVENT DETONATION_START") handler(ev, "AP_RM_DETONATION_START");
            else if (ev.Query == "SERVER_EVENT DETONATION_CANCEL") handler(ev, "AP_RM_DETONATION_CANCEL");
            else if (ev.Query == "SERVER_EVENT DETONATION_INSTANT") handler(ev, "AP_RM_DETONATION_INSTANT");

            else if (ev.Query.Contains("setconfig friendly_fire")) handler(ev, "AP_RM_SETCONFIG_FRIENDLY_FIRE");
            else if (ev.Query.Contains("setconfig spawn_protect_disable")) handler(ev, "AP_RM_SETCONFIG_SPAWNPROTECT_DISABLE");
            else if (ev.Query.Contains("setconfig player_list_title")) handler(ev, "AP_RM_SETCONFIG_PLAYERLIST_TITLE");
            else if (ev.Query.Contains("setconfig pd_refresh_exit")) handler(ev, "AP_RM_SETCONFIG_PDREFRESH_EXIT");
            else if (ev.Query.Contains("setconfig spawn_protect_time")) handler(ev, "AP_RM_SETCONFIG_SPAWNPROTECT_TIME");
            else CommandHandler(ev);
        }

        private void handler(AdminQueryEvent ev, string perm)
        {
            if (!hasPerm(ev.Admin.SteamId, ev.Admin.GetUserGroup().Name, perm))
            {
                ev.Successful = false;
                ev.Handled = true;
                ev.Output = "You Don't Have Permission to do that :D";
            }
        }

        private void KickBanHandler(AdminQueryEvent ev)
        {
            string perm = "";

            string[] args = ev.Query.Split(' ');

            if (args.Length != 3)
            {
                return;
            }

            int time = Int32.Parse(args[args.Length - 1]);

            if (time == 0)
            {
                perm = "AP_RM_PLAYER_KICK";
            }
            else if (time > 0 && time < 31)
            {
                perm = "AP_RM_PLAYER_MINUTES";
            }
            else if (time > 59 && time < 721)
            {
                perm = "AP_RM_PLAYER_HOURS";
            }
            else if (time > 1439 && time < 144001)
            {
                perm = "AP_RM_PLAYER_DAYS";
            }
            else if (time > 525599)
            {
                perm = "AP_RM_PLAYER_YEARS";
            }
            else
            {
                ev.Successful = false;
                ev.Handled = true;
                return;
            }

            if (!hasPerm(ev.Admin.SteamId, ev.Admin.GetUserGroup().Name, perm))
            {
                ev.Successful = false;
                ev.Handled = true;
                ev.Output = "You Don't Have Permission to do that :D";
            }
        }

        private void GiveHandler(AdminQueryEvent ev)
        {
            string perm = "";

            string[] args = ev.Query.Split(' ');

            if (args.Length != 3)
            {
                return;
            }

            int itemCode = Int32.Parse(args[args.Length-1]);

            switch (itemCode)
            {
                case (int) ItemType.JANITOR_KEYCARD:
                case (int) ItemType.SCIENTIST_KEYCARD:
                case (int) ItemType.MAJOR_SCIENTIST_KEYCARD:
                case (int) ItemType.ZONE_MANAGER_KEYCARD:
                case (int) ItemType.GUARD_KEYCARD:
                case (int) ItemType.SENIOR_GUARD_KEYCARD:
                case (int) ItemType.CONTAINMENT_ENGINEER_KEYCARD:
                case (int) ItemType.MTF_LIEUTENANT_KEYCARD:
                case (int) ItemType.MTF_COMMANDER_KEYCARD:
                case (int) ItemType.FACILITY_MANAGER_KEYCARD:
                case (int) ItemType.CHAOS_INSURGENCY_DEVICE:
                case (int) ItemType.O5_LEVEL_KEYCARD:
                    perm = "AP_RM_PLAYER_GIVE_KEYCARD";
                    break;
                case (int) ItemType.RADIO:
                case (int) ItemType.COM15:
                case (int) ItemType.MEDKIT:
                case (int) ItemType.FLASHLIGHT:
                case (int) ItemType.WEAPON_MANAGER_TABLET:
                case (int) ItemType.DISARMER:
                    perm = "AP_RM_PLAYER_GIVE_TOOL";
                    break;
                case (int) ItemType.MICROHID:
                case (int) ItemType.E11_STANDARD_RIFLE:
                case (int) ItemType.P90:
                case (int) ItemType.MP4:
                case (int) ItemType.USP:
                case (int) ItemType.LOGICER:
                    perm = "AP_RM_PLAYER_GIVE_WEAPON";
                    break;
                case (int) ItemType.FRAG_GRENADE:
                case (int) ItemType.FLASHBANG:
                    perm = "AP_RM_PLAYER_GIVE_GRENADE";
                    break;
                case (int)ItemType.DROPPED_7:
                case (int)ItemType.DROPPED_9:
                    perm = "AP_RM_PLAYER_GIVE_AMMO";
                    break;
                case (int) ItemType.COIN:
                case (int) ItemType.CUP:
                    perm = "AP_RM_PLAYER_GIVE_OTHER";
                    break;
            }

            if (!hasPerm(ev.Admin.SteamId, ev.Admin.GetUserGroup().Name, perm))
            {
                ev.Successful = false;
                ev.Handled = true;
                ev.Output = "You Don't Have Permission to do that :D";
            }
        }

        private void CommandHandler(AdminQueryEvent ev)
        {
            try
            {
                bool canDo = false;

                int pos = Array.IndexOf(config.GetListValue("AP_COMMANDS_*", false), ev.Admin.SteamId);

                if (pos > -1) canDo = true;

                pos = Array.IndexOf(config.GetListValue("AP_COMMANDS_*", false), ev.Admin.GetUserGroup().Name);

                if (pos > -1) canDo = true;
                
                Dictionary<string, string> dic = config.GetDictValue("AP_COMMANDS");

                string listString;
                if (!dic.TryGetValue(ev.Query.Split(' ')[0].ToLower(), out listString))
                {
                    ev.Successful = false;
                    ev.Handled = true;
                    return;
                }
                string[] users = ParseCommaSeparatedString(listString);

                pos = Array.IndexOf(users, ev.Admin.SteamId);

                if (pos > -1) canDo = true;

                pos = Array.IndexOf(users, ev.Admin.GetUserGroup().Name);

                if (pos > -1) canDo = true;

                if (!canDo)
                {
                    ev.Successful = false;
                    ev.Handled = true;
                    ev.Output = "You Don't Have Permission to do that :D";
                }
            }
            catch (Exception e)
            {
                plugin.Error(e.ToString());
                ev.Successful = false;
                ev.Handled = true;
            }
            
        }

        private bool hasPerm(string SteamId, string UserGroupName, string Perm)
        {
            int pos = Array.IndexOf(config.GetListValue("AP_RM_*", false), SteamId);

            if (pos > -1) return true;

            pos = Array.IndexOf(config.GetListValue("AP_RM_*", false), UserGroupName);

            if (pos > -1) return true;

            pos = Array.IndexOf(config.GetListValue(Perm, false), SteamId);

            if (pos > -1) return true;

            pos = Array.IndexOf(config.GetListValue(Perm, false), UserGroupName);

            if (pos > -1) return true; else return false;
        }

        // Pulled From YamlConfig in Assembly-CSharp
        private string[] ParseCommaSeparatedString(string data)
        {
            if (!data.StartsWith("[") || !data.EndsWith("]"))
                return (string[])null;
            data = data.Substring(1, data.Length - 2);
            return data.Split(new string[1] { ", " }, StringSplitOptions.None);
        }

        public void OnRoundStart(RoundStartEvent ev)
        {
            config = ConfigManager.Manager.Config;
        }
    }
}
