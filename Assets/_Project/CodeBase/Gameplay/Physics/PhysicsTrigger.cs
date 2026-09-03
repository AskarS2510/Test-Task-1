using System;
using UnityEngine;

namespace _Project.CodeBase.Gameplay.Physics
{
    public class PhysicsTrigger : MonoBehaviour
    {
        public event Action<Collider> Collided;

        private void OnTriggerEnter(Collider other) => Collided?.Invoke(other);
    }
}