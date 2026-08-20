using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Gameplay.Player
{
    public class Player : MonoBehaviour
    {
        private InputService _inputService;
        private Mover _mover;
        private Health _health;

        [Inject]
        public void Construct(InputService inputService)
        {
            _inputService = inputService;
        }

        public void Initialize(Mover mover, Health health)
        {
            _mover = mover;
            _health = health;
        }

        [Button]
        public void TakeDamage(int damage)
        {
            _health.TakeDamage(damage);
        }

        private void Update()
        {
            Vector2 inputDirection = _inputService.Motion;

            _mover.Move(inputDirection);
        }
    }
}