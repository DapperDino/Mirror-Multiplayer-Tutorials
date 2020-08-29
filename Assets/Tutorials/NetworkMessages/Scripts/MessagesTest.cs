using Mirror;
using TMPro;
using UnityEngine;

namespace DapperDino.Mirror.Tutorials.NetworkMessages
{
    public class Notification : MessageBase
    {
        public string content;
    }

    public class MessagesTest : MonoBehaviour
    {
        [SerializeField] private TMP_Text notificationsText = null;

        private void Start()
        {
            if (!NetworkClient.active) { return; }

            NetworkClient.RegisterHandler<Notification>(OnNotification);
        }

        private void OnNotification(NetworkConnection conn, Notification msg)
        {
            notificationsText.text += $"\n{msg.content}";
        }
    }
}
