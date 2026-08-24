using System;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Gameplay.Logic
{
    public class Player : MonoBehaviour
    {
        private InputService _inputService;
        private DirectionMover _navMeshMover;
        private Health _health;
        public event Action Died;

        [Inject]
        public void Construct(InputService inputService)
        {
            _inputService = inputService;
        }

        public void Initialize(DirectionMover directionMover, Health health)
        {
            _navMeshMover = directionMover;
            _health = health;
            _health.Died += RaiseDied;
        }

        [Button]
        public void TakeDamage(int damage)
        {
            _health.TakeDamage(damage);
        }

        private void RaiseDied()
        {
            Died?.Invoke();
        }

        private void Update()
        {
            Vector2 inputDirection = _inputService.Motion;

            _navMeshMover.Move(new Vector3(inputDirection.x, 0, inputDirection.y));
        }
    }
}