using UnityEngine;
using UnityEngine.UI;

namespace _Project.CodeBase.Gameplay.Healths
{
    public class HealthView : MonoBehaviour
    {
        [SerializeField] private Image _fillImage;

        public void UpdateView(int current, int max) => _fillImage.fillAmount = (float)current / max;
    }
}