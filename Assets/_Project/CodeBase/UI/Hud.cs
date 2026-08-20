using _Project.CodeBase.Gameplay.Player;
using UnityEngine;

namespace _Project.CodeBase.UI
{
    public class Hud : MonoBehaviour
    {
        [field: SerializeField] public HealthView HealthView { get; private set; }
    }
}