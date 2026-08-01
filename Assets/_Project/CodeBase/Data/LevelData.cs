using System;

namespace _Project.CodeBase.Data
{
    [Serializable]
    public class LevelData
    {
        public int CurrentLevel;

        public string GetLevelKey()
        {
            return $"Level_{CurrentLevel}";
        }
    }
}