using UnityEngine;

namespace _Project.CodeBase.Data
{
    public class ProgressService
    {
        private const string PROGRESS_KEY = "Progress";
        public PlayerProgress Progress { get; private set; }

        public void LoadProgressOrInitNew()
        {
            Progress = LoadProgress() ?? NewProgress();
        }

        public void SaveProgress()
        {
            PlayerPrefs.SetString(PROGRESS_KEY, Progress.ToJson());
        }

        private PlayerProgress LoadProgress()
        {
            return PlayerPrefs.GetString(PROGRESS_KEY)?.ToDeserialized<PlayerProgress>();
        }

        private PlayerProgress NewProgress()
        {
            PlayerProgress progress = new();

            progress.LevelData = new LevelData { CurrentLevel = 1 };

            return progress;
        }
    }
}