using UnityEngine;
using UnityEngine.AddressableAssets;

namespace _Project.CodeBase.StaticData
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Configs/GameConfig")]
    public class GameConfig : ScriptableObject
    {
        [field: SerializeField] public AssetReference PlayerReference { get; private set; }
        [field: SerializeField] public float PlayerMoveSpeed { get; private set; }
        [field: SerializeField] public float PlayerRotationSpeed { get; private set; }
        [field: SerializeField] public int PlayerHealth { get; private set; }
        [field: SerializeField] public AssetReference PatrolReference { get; private set; }
        [field: SerializeField] public AssetReference HunterReference { get; private set; }
        [field: SerializeField] public float EnemySpeed { get; private set; }
        [field: SerializeField] public float EnemyRotationSpeed { get; private set; }
        [field: SerializeField] public int CollisionDamage { get; private set; }
    }
}