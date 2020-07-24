using UnityEngine;
using UnityEngine.UI;

namespace DapperDino.Mirror.Tutorials.Events
{
    public class HealthDisplay : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Health health = null;
        [SerializeField] private Image healthBarImage = null;

        private void OnEnable()
        {
            health.EventHealthChanged += HandleHealthChanged;
        }

        private void OnDisable()
        {
            health.EventHealthChanged -= HandleHealthChanged;
        }

        private void HandleHealthChanged(int currentHealth, int maxHealth)
        {
            healthBarImage.fillAmount = (float)currentHealth / maxHealth;
        }
    }
}
