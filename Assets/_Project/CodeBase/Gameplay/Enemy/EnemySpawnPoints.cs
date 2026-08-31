using UnityEngine;

namespace _Project.CodeBase.Gameplay.Enemy
{
    public class EnemySpawnPoints : MonoBehaviour
    {
        [field: SerializeField] public SpawnPoint[] Points { get; private set; }
    }
}