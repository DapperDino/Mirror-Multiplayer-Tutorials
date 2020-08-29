using Mirror;
using UnityEngine;

namespace DapperDino.Mirror.Tutorials.NetworkMessages
{
    public class MyNetworkManager : NetworkManager
    {
        [SerializeField] private string notificationMessage = string.Empty;

        public override void OnStartServer()
        {
            ServerChangeScene("Scene_NetworkMessagesLobby");
        }

        [ContextMenu("Send Notification")]
        private void SendNotification()
        {
            NetworkServer.SendToAll(new Notification { content = notificationMessage });
        }
    }
}
