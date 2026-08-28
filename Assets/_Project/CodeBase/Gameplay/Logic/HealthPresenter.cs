using _Project.CodeBase.UI;

namespace _Project.CodeBase.Gameplay.Logic
{
    public class HealthPresenter
    {
        private readonly HealthView _healthView;
        private Health _health;

        public HealthPresenter(Hud hud) => _healthView = hud.HealthView;

        public void Initialize(Health health)
        {
            _health = health;
            Subscribe();
            OnHealthChanged();
        }

        public void Dispose() => Unsubscribe();

        private void Subscribe() => _health.Changed += OnHealthChanged;

        private void Unsubscribe() => _health.Changed -= OnHealthChanged;

        private void OnHealthChanged() => _healthView.UpdateView(_health.Current, _health.Max);
    }
}