using Unity.Cinemachine;
using UnityEngine;

namespace _Project.CodeBase.Gameplay.Logic
{
    public class CameraProvider : MonoBehaviour
    {
        [SerializeField] private CinemachineCamera _cinemachine;

        public void Follow(Transform target) => _cinemachine.Follow = target;
    }
}