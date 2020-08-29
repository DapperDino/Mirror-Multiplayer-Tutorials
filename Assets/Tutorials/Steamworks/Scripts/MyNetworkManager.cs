using Mirror;
using UnityEngine;

namespace DapperDino.Mirror.Tutorials.Steamworks
{
    public class MyNetworkManager : NetworkManager
    {
        [SerializeField] private string notificationMessage = string.Empty;

        public override void OnStartServer()
        {
            ServerChangeScene("Scene_SteamworksLobby");
        }

        [ContextMenu("Send Notification")]
        private void SendNotification()
        {
            NetworkServer.SendToAll(new Notification { content = notificationMessage });
        }
    }
}
