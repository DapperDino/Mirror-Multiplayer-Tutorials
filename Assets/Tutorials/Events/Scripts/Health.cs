using Mirror;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DapperDino.Mirror.Tutorials.Events
{
    public class Health : NetworkBehaviour
    {
        [Header("Settings")]
        [SerializeField] private int maxHealth = 100;
        [SerializeField] private int damagePerPress = 10;

        [SyncVar]
        private int currentHealth;

        public delegate void HealthChangedDelegate(int currentHealth, int maxHealth);
        [SyncEvent]
        public event HealthChangedDelegate EventHealthChanged;

        #region Server

        [Server]
        private void SetHealth(int value)
        {
            currentHealth = value;
            EventHealthChanged?.Invoke(currentHealth, maxHealth);
        }

        public override void OnStartServer() => SetHealth(maxHealth);

        [Command]
        private void CmdDealDamage() => SetHealth(Mathf.Max(currentHealth - damagePerPress, 0));

        #endregion

        #region Client

        [ClientCallback]
        private void Update()
        {
            if (!hasAuthority) { return; }

            if (!Keyboard.current.spaceKey.wasPressedThisFrame) { return; }

            CmdDealDamage();
        }

        #endregion
    }
}
