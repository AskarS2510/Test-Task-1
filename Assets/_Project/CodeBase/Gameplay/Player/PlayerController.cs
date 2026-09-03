using _Project.CodeBase.Gameplay.Logic;
using _Project.CodeBase.Gameplay.Movement;
using UnityEngine;

namespace _Project.CodeBase.Gameplay.Player
{
    public class PlayerController : IUpdatable
    {
        private readonly InputService _inputService;
        private readonly DirectionMover _navMeshMover;

        public PlayerController(InputService inputService, DirectionMover directionMover)
        {
            _inputService = inputService;
            _navMeshMover = directionMover;
        }

        public void Update()
        {
            Vector2 inputDirection = _inputService.Motion;

            _navMeshMover.Move(new Vector3(inputDirection.x, 0, inputDirection.y));
        }
    }
}