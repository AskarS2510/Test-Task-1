using System;
using _Project.CodeBase.Gameplay.Enemy;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Gameplay.Logic
{
    public class Player : IUpdatable
    {
        private readonly InputService _inputService;
        private DirectionMover _navMeshMover;
        private Health _health;
        public event Action Died;

        public Player(InputService inputService)
        {
            _inputService = inputService;
        }

        public void Initialize(DirectionMover directionMover, Health health)
        {
            _navMeshMover = directionMover;
            _health = health;
            _health.Died += RaiseDied;
        }

        public void Update()
        {
            Vector2 inputDirection = _inputService.Motion;

            _navMeshMover.Move(new Vector3(inputDirection.x, 0, inputDirection.y));
        }

        public void TakeDamage(int damage) => _health.TakeDamage(damage);

        private void RaiseDied() => Died?.Invoke();
    }
}