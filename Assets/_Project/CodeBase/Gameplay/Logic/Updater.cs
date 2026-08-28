using System.Collections.Generic;
using _Project.CodeBase.Gameplay.Enemy;

namespace _Project.CodeBase.Gameplay.Logic
{
    public class Updater
    {
        private readonly List<IUpdatable> _updatables;

        public Updater() => _updatables = new List<IUpdatable>();

        public void Update()
        {
            foreach (IUpdatable updatable in _updatables)
            {
                updatable.Update();
            }
        }

        public void Register(IUpdatable updatable) => _updatables.Add(updatable);

        public void Unregister(IUpdatable updatable) => _updatables.Remove(updatable);
    }
}