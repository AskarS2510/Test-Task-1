namespace _Project.CodeBase.Gameplay.Logic
{
    public class Pauser
    {
        public bool IsPaused { get; private set; }

        public Pauser() => IsPaused = true;

        public void Pause() => IsPaused = true;

        public void Resume() => IsPaused = false;
    }
}