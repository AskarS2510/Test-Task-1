using UnityEngine;

namespace _Project.CodeBase.Gameplay.Movement
{
    public class WayPoints : MonoBehaviour
    {
        [field: SerializeField] public Transform[] Points { get; private set; }
    }
}