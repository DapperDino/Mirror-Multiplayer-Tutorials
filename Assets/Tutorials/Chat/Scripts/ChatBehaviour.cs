using Mirror;
using System;
using TMPro;
using UnityEngine;

namespace DapperDino.Mirror.Tutorials.Chat
{
    public class ChatBehaviour : NetworkBehaviour
    {
        [SerializeField] private GameObject chatUI = null;
        [SerializeField] private TMP_Text chatText = null;
        [SerializeField] private TMP_InputField inputField = null;

        private static event Action<string> OnMessage;

        public override void OnStartAuthority()
        {
            chatUI.SetActive(true);

            OnMessage += HandleNewMessage;
        }

        [ClientCallback]
        private void OnDestroy()
        {
            if (!hasAuthority) { return; }

            OnMessage -= HandleNewMessage;
        }

        private void HandleNewMessage(string message)
        {
            chatText.text += message;
        }

        [Client]
        public void Send(string message)
        {
            if (!Input.GetKeyDown(KeyCode.Return)) { return; }

            if (string.IsNullOrWhiteSpace(message)) { return; }

            CmdSendMessage(message);

            inputField.text = string.Empty;
        }

        [Command]
        private void CmdSendMessage(string message)
        {
            RpcHandleMessage($"[{connectionToClient.connectionId}]: {message}");
        }

        [ClientRpc]
        private void RpcHandleMessage(string message)
        {
            OnMessage?.Invoke($"\n{message}");
        }
    }
}
