using System;

namespace _Project.CodeBase.Gameplay.Logic
{
    public class Health : IDamageable
    {
        public int Max { get; }
        public int Current { get; private set; }
        public event Action Changed;
        public event Action Died;

        public Health(int maxHealth)
        {
            Max = maxHealth;
            Current = Max;
        }

        public void TakeDamage(int damage)
        {
            if (Current <= 0)
                return;

            Current -= damage;

            Changed?.Invoke();

            if (Current <= 0)
                Died?.Invoke();
        }
    }
}