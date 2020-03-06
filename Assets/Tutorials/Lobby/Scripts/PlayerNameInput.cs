using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DapperDino.Mirror.Tutorials.Lobby
{
    public class PlayerNameInput : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private TMP_InputField nameInputField = null;
        [SerializeField] private Button continueButton = null;

        public static string DisplayName { get; private set; }

        private const string PlayerPrefsNameKey = "PlayerName";

        private void Start() => SetUpInputField();

        private void SetUpInputField()
        {
            if (!PlayerPrefs.HasKey(PlayerPrefsNameKey)) { return; }

            string defaultName = PlayerPrefs.GetString(PlayerPrefsNameKey);

            nameInputField.text = defaultName;

            SetPlayerName(defaultName);
        }

        public void SetPlayerName(string name)
        {
            continueButton.interactable = !string.IsNullOrEmpty(name);
        }

        public void SavePlayerName()
        {
            DisplayName = nameInputField.text;

            PlayerPrefs.SetString(PlayerPrefsNameKey, DisplayName);
        }
    }
}
