using UnityEngine;
using UnityEngine.UI;

namespace _Project.CodeBase.Gameplay.Player
{
    public class HealthView : MonoBehaviour
    {
        [SerializeField] private Image _fillImage;
        private Health _health;

        public void Initialize(Health health)
        {
            _health = health;
            Subscribe();
            OnHealthChanged();
        }

        private void Subscribe()
        {
            _health.Changed += OnHealthChanged;
            _health.Died += Unsubscribe;
        }

        private void Unsubscribe()
        {
            _health.Changed -= OnHealthChanged;
            _health.Died -= Unsubscribe;
        }

        private void OnHealthChanged()
        {
            UpdateView(_health.Current, _health.Max);
        }

        private void UpdateView(int current, int max)
        {
            _fillImage.fillAmount = (float)current / max;
        }
    }
}