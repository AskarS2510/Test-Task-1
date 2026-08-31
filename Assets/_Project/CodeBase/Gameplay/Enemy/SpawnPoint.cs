using UnityEngine;

namespace _Project.CodeBase.Gameplay.Enemy
{
    public class SpawnPoint : MonoBehaviour
    {
        [field: SerializeField] public EnemyType EnemyType { get; private set; }
    }
}