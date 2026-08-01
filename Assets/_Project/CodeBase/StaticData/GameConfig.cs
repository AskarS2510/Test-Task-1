using UnityEngine;
using UnityEngine.AddressableAssets;

namespace _Project.CodeBase.StaticData
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Configs/GameConfig")]
    public class GameConfig : ScriptableObject
    {
        public AssetReference PlayerReference;
        public AssetReference MainCameraReference;
        public AssetReference CinemachineReference;
        public float PlayerMoveSpeed;
        public float PlayerRotationSpeed;
    }
}