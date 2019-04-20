using Smod2;
using Smod2.API;
using Smod2.EventHandlers;
using Smod2.Events;
using System;
using System.Collections.Generic;
using System.Linq;

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
            string Query = ev.Query.ToLower();
            if (Query == "request_data player_list silent") return;
            else if (Query.StartsWith("request_data short-player ")) handler(ev, "AP_RM_PlAYER_INFO_REQUEST");
            else if (Query.StartsWith("request_data player ")) handler(ev, "AP_RM_PlAYER_INFO_REQUEST_IP");
            else if (Query.StartsWith("request_data auth ")) handler(ev, "AP_RM_PlAYER_INFO_REQUEST_AUTH");

            else if (Query.StartsWith("ban ")) KickBanHandler(ev);

            else if (Query.StartsWith("forceclass ")) handler(ev, "AP_RM_PLAYER_FORCECLASS", true);

            else if (Query.StartsWith("give ")) GiveHandler(ev);

            else if (Query.StartsWith("overwatch ")) handler(ev, "AP_RM_ADMIN_OVERWATCH", true);
            else if (Query.StartsWith("god ")) handler(ev, "AP_RM_ADMIN_GOD", true);
            else if (Query.StartsWith("bypass ")) handler(ev, "AP_RM_ADMIN_BYPASS", true);
            else if (Query.StartsWith("bring ")) handler(ev, "AP_RM_ADMIN_BRING", true);
            else if (Query.StartsWith("goto ")) handler(ev, "AP_RM_ADMIN_GOTO", true);
            else if (Query.StartsWith("heal ")) handler(ev, "AP_RM_ADMIN_HEAL", true);
            else if (Query.StartsWith("lockdown ")) handler(ev, "AP_RM_ADMIN_LOCKDOWN", true);

            else if (Query == "open **") handler(ev, "AP_RM_DOORS_OPEN_ALL"); // ALL
            else if (Query == "open *") handler(ev, "AP_RM_DOORS_OPEN_ALL_LISTED");  // ALL Listed
            else if (Query == "open !*") handler(ev, "AP_RM_DOORS_OPEN_ALL_NOT_LISTED"); // ALL Not listed
            else if (Query.StartsWith("open ")) handler(ev, "AP_RM_DOORS_OPEN_SINGLE"); // Single
            else if (Query == "close **") handler(ev, "AP_RM_DOORS_CLOSE_ALL"); // ALL
            else if (Query == "close *") handler(ev, "AP_RM_DOORS_CLOSE_ALL_LISTED");  // ALL Listed
            else if (Query == "close !*") handler(ev, "AP_RM_DOORS_CLOSE_ALL_NOT_LISTED"); // ALL Not listed
            else if (Query.StartsWith("close ")) handler(ev, "AP_RM_DOORS_CLOSE_SINGLE"); // Single
            else if (Query == "lock **") handler(ev, "AP_RM_DOORS_LOCK_ALL"); // ALL
            else if (Query == "lock *") handler(ev, "AP_RM_DOORS_LOCK_ALL_LISTED");  // ALL Listed
            else if (Query == "lock !*") handler(ev, "AP_RM_DOORS_LOCK_ALL_NOT_LISTED"); // ALL Not listed
            else if (Query.StartsWith("lock ")) handler(ev, "AP_RM_DOORS_LOCK_SINGLE"); // Single
            else if (Query == "unlock **") handler(ev, "AP_RM_DOORS_UNLOCK_ALL"); // ALL
            else if (Query == "unlock *") handler(ev, "AP_RM_DOORS_UNLOCK_ALL_LISTED");  // ALL Listed
            else if (Query == "unlock !*") handler(ev, "AP_RM_DOORS_UNLOCK_ALL_NOT_LISTED"); // ALL Not listed
            else if (Query.StartsWith("unlock ")) handler(ev, "AP_RM_DOORS_UNLOCK_SINGLE"); // Single
            else if (Query == "destroy **") handler(ev, "AP_RM_DOORS_DESTROY_ALL"); // ALL
            else if (Query == "destroy *") handler(ev, "AP_RM_DOORS_DESTROY_ALL_LISTED");  // ALL Listed
            else if (Query == "destroy !*") handler(ev, "AP_RM_DOORS_DESTROY_ALL_NOT_LISTED"); // ALL Not listed
            else if (Query.StartsWith("destroy ")) handler(ev, "AP_RM_DOORS_DESTROY_SINGLE"); // Single
            else if (Query.StartsWith("teleport ")) handler(ev, "AP_RM_DOORS_TELEPORT");

            else if (Query.StartsWith("mute ")) handler(ev, "AP_RM_PLAYER_MUTE", true);
            else if (Query.StartsWith("unmute ")) handler(ev, "AP_RM_PLAYER_MUTE", true);
            else if (Query.StartsWith("imute ")) handler(ev, "AP_RM_PLAYER_MUTE_INTERCOM", true);
            else if (Query.StartsWith("iunmute ")) handler(ev, "AP_RM_PLAYER_MUTE_INTERCOM", true);

            else if (Query == "server_event force_mtf_respawn") handler(ev, "AP_RM_RESPAWN_MIF");
            else if (Query == "server_event force_ci_respawn") handler(ev, "AP_RM_RESPAWN_CI");
            else if (Query == "server_event round_restart") handler(ev, "AP_RM_RESTART_ROUND");
            else if (Query == "forcestart") handler(ev, "AP_RM_FORCE_START");
            else if (Query == "server_event terminate_unconn") handler(ev, "AP_RM_KICK_UNCONNECTED");
            else if (Query == "intercom-timeout") handler(ev, "AP_RM_INTERCOM_TIMEOUT");
            else if (Query == "intercom-reset") handler(ev, "AP_RM_INTERCOMM_RESET");
            else if (Query == "server_event detonation_start") handler(ev, "AP_RM_DETONATION_START");
            else if (Query == "server_event detonation_cancel") handler(ev, "AP_RM_DETONATION_CANCEL");
            else if (Query == "server_event detonation_instant") handler(ev, "AP_RM_DETONATION_INSTANT");

            else if (Query.StartsWith("setconfig friendly_fire")) handler(ev, "AP_RM_SETCONFIG_FRIENDLY_FIRE");
            else if (Query.StartsWith("setconfig spawn_protect_disable")) handler(ev, "AP_RM_SETCONFIG_SPAWNPROTECT_DISABLE");
            else if (Query.StartsWith("setconfig player_list_title")) handler(ev, "AP_RM_SETCONFIG_PLAYERLIST_TITLE");
            else if (Query.StartsWith("setconfig pd_refresh_exit")) handler(ev, "AP_RM_SETCONFIG_PDREFRESH_EXIT");
            else if (Query.StartsWith("setconfig spawn_protect_time")) handler(ev, "AP_RM_SETCONFIG_SPAWNPROTECT_TIME");

            else CommandHandler(ev);
        }

        private void handler(AdminQueryEvent ev, string perm, bool hierarchy = false)
        {
            Server server = PluginManager.Manager.Server;
            if (hierarchy && config.GetBoolValue("AP_HIERARCHY_ENABLE", false, false)) {
                bool isHigher = true;

                string[] queryArgs = ev.Query.Split(' ');

                if (queryArgs.Length > 1)
                {
                    List<Player> playerList = server.GetPlayers("");

                    string[] players = queryArgs[1].Split('.');
                    playerList = playerList.FindAll(p => {
                        if (Array.IndexOf(players, p.PlayerId.ToString()) > -1) return true;
                        return false;
                    });

                    if (playerList.Count > 0) {
                        foreach (Player player in playerList) if (!checkHierarchy(ev.Admin, player)) isHigher = false;
                    }
                }

                if (!isHigher) {
                    ev.Successful = false;
                    ev.Handled = true;
                    ev.Output = "Player is higher rank than you!";
                }
            }

            if (!hasPerm(ev.Admin.SteamId, ev.Admin.GetUserGroup().Name, perm))
            {
                ev.Successful = false;
                ev.Handled = true;
                ev.Output = "You Don't Have Permission to do that! You require the permission " + perm + "!";
            }
        }

        private void KickBanHandler(AdminQueryEvent ev)
        {
            Server server = PluginManager.Manager.Server;
            if (config.GetBoolValue("AP_HIERARCHY_ENABLE", false, false))
            {
                bool isHigher = true;

                string[] queryArgs = ev.Query.Split(' ');

                if (queryArgs.Length > 1)
                {
                    List<Player> playerList = server.GetPlayers("");

                    string[] players = queryArgs[1].Split('.');
                    playerList = playerList.FindAll(p => {
                        if (Array.IndexOf(players, p.PlayerId.ToString()) > -1) return true;
                        return false;
                    });

                    if (playerList.Count > 0)
                    {
                        foreach (Player player in playerList) if (!checkHierarchy(ev.Admin, player)) isHigher = false;
                    }
                }

                if (!isHigher)
                {
                    ev.Successful = false;
                    ev.Handled = true;
                    ev.Output = "Player is higher rank than you!";
                }
            }

            if (config.GetBoolValue("AP_DISABLE", false, false))
            {

                bool isHigher = true;

                string[] queryArgs = ev.Query.Split(' ');

                if (queryArgs.Length > 1)
                {
                    List<Player> playerList = new List<Player>();

                    foreach (string plyID in queryArgs[1].Split('.'))
                    {
                        playerList.AddRange(server.GetPlayers(plyID));
                    }

                    if (playerList.Count > 0)
                    {
                        foreach (Player player in playerList) if (!checkHierarchy(ev.Admin, player)) isHigher = false;
                    }
                }

                if (!isHigher)
                {
                    ev.Successful = false;
                    ev.Handled = true;
                    ev.Output = "Player is higher rank than you!";
                }
            }

            string perm = "";

            string[] args = ev.Query.Split(' ');

            if (args.Length < 3)
            {
                return;
            }

            int time = Int32.Parse(args[args.Length - 1]);

            if (time <= 0)
            {
                perm = "AP_RM_PLAYER_KICK";
            }
            else if (time > 0 && time < 60)
            {
                perm = "AP_RM_PLAYER_MINUTES";
            }
            else if (time > 59 && time < 1440)
            {
                perm = "AP_RM_PLAYER_HOURS";
            }
            else if (time > 1439 && time < 525600)
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
                ev.Output = "You Don't Have Permission to do that! You require the permission " + perm + "!";
            }
        }

        private void GiveHandler(AdminQueryEvent ev)
        {
            Server server = PluginManager.Manager.Server;
            if (config.GetBoolValue("AP_HIERARCHY_ENABLE", false, false))
            {
                bool isHigher = true;

                string[] queryArgs = ev.Query.Split(' ');

                if (queryArgs.Length > 1)
                {
                    List<Player> playerList = server.GetPlayers("");

                    string[] players = queryArgs[1].Split('.');
                    playerList = playerList.FindAll(p => {
                        if (Array.IndexOf(players, p.PlayerId.ToString()) > -1) return true;
                        return false;
                    });

                    if (playerList.Count > 0)
                    {
                        foreach (Player player in playerList) if (!checkHierarchy(ev.Admin, player)) isHigher = false;
                    }
                }

                if (!isHigher)
                {
                    ev.Successful = false;
                    ev.Handled = true;
                    ev.Output = "Player is higher rank than you!";
                }
            }

            string perm = "";

            string[] args = ev.Query.Split(' ');

            if (args.Length != 3)
            {
                return;
            }

            int itemCode;
            if (!Int32.TryParse(args[args.Length - 1], out itemCode)) return;

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
                ev.Output = "You Don't Have Permission to do that! You require the permission " + perm + "!";
            }
        }

        private void CommandHandler(AdminQueryEvent ev)
        {
            try
            {
                int pos = Array.IndexOf(config.GetListValue("AP_COMMANDS_*", false), ev.Admin.SteamId);

                if (pos > -1) return;

                pos = Array.IndexOf(config.GetListValue("AP_COMMANDS_*", false), ev.Admin.GetUserGroup().Name);

                if (pos > -1) return;
                
                Dictionary<string, string> dic = config.GetDictValue("AP_COMMANDS");

                string listString;
                if (!dic.TryGetValue(ev.Query.Split(' ')[0].ToLower(), out listString))
                {
                    ev.Successful = false;
                    ev.Handled = true;
                    ev.Output = "You Don't Have Permission to do that!";
                    return;
                }

                string[] users = ParseCommaSeparatedString(listString);

                bool canDo = false;

                if (users.Length > 0) {

                    pos = Array.IndexOf(users, ev.Admin.SteamId);

                    if (pos > -1) canDo = true;

                    pos = Array.IndexOf(users, ev.Admin.GetUserGroup().Name);

                    if (pos > -1) canDo = true;
                }

                if (!canDo)
                {
                    ev.Successful = false;
                    ev.Handled = true;
                    ev.Output = "You Don't Have Permission to do that!";
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

        private bool checkHierarchy(Player admin, Player player)
        {
            if (admin != null && player != null && admin.SteamId == player.SteamId) return true;

            Dictionary<string, string> dic = config.GetDictValue("AP_HIERARCHY");

            if (dic.Count == 0) return true; // Nothing is in dictionary so treat it like hierarchy is disabled

            UserGroup aug = admin.GetUserGroup();
            string adminRankString = dic.FirstOrDefault(x => x.Value == admin.SteamId || x.Value == aug.Name).Key;

            UserGroup pug = player.GetUserGroup();
            string playerRankString = dic.FirstOrDefault(x => {
                if (x.Value == player.SteamId) return true;
                if (pug != null && x.Value == pug.Name) return true;
                else return false;
            }).Key;

            adminRankString = !String.IsNullOrEmpty(adminRankString) ? adminRankString : "100";
            playerRankString = !String.IsNullOrEmpty(playerRankString) ? playerRankString : "100";

            int adminRank = 100;
            int playerRank = 100;

            if (!Int32.TryParse(adminRankString, out adminRank) || !Int32.TryParse(playerRankString, out playerRank)) {
                plugin.Warn("One of the hierarchy dictionary keys is not a number");
                return false;
            }

            if (adminRank < playerRank) return true;
            else return false;
        }

        // Pulled From YamlConfig in Assembly-CSharp (with modifications)
        public string[] ParseCommaSeparatedString(string data)
        {
            if (!data.StartsWith("[") || !data.EndsWith("]"))
                return new string[0];
            data = data.Substring(1, data.Length - 2);
            return data.Split(new string[2] { "| ", "|" }, StringSplitOptions.None);
        }

        public void OnRoundStart(RoundStartEvent ev)
        {
            config = ConfigManager.Manager.Config;
        }
    }
}
