using Mirror;
using Steamworks;

namespace DapperDino.Mirror.Tutorials.SteamProfile
{
    public class MyNetworkManager : NetworkManager
    {
        public override void OnServerAddPlayer(NetworkConnection conn)
        {
            base.OnServerAddPlayer(conn);

            CSteamID steamId = SteamMatchmaking.GetLobbyMemberByIndex(
                SteamLobby.LobbyId,
                numPlayers - 1);

            var playerInfoDisplay = conn.identity.GetComponent<PlayerInfoDisplay>();

            playerInfoDisplay.SetSteamId(steamId.m_SteamID);
        }
    }
}
