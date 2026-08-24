using UnityEngine;
using UnityEngine.AddressableAssets;

namespace _Project.CodeBase.StaticData
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Configs/GameConfig")]
    public class GameConfig : ScriptableObject
    {
        public AssetReference PlayerReference;
        public float PlayerMoveSpeed;
        public float PlayerRotationSpeed;
        public int PlayerHealth;
        public AssetReference EnemyReference;
        public float EnemySpeed;
        public float EnemyRotationSpeed;
    }
}