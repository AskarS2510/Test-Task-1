using UnityEngine;

namespace _Project.CodeBase.Gameplay.Enemy
{
    public class WayPoints : MonoBehaviour
    {
        [field: SerializeField] public Transform[] Points { get; private set; }
    }
}