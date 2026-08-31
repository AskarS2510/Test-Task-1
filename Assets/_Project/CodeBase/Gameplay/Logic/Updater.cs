using System.Collections.Generic;
using _Project.CodeBase.Gameplay.Enemy;

namespace _Project.CodeBase.Gameplay.Logic
{
    public class Updater
    {
        private readonly List<IUpdatable> _updatables;
        private readonly List<IUpdatable> _toRegister;
        private readonly List<IUpdatable> _toUnregister;

        public Updater()
        {
            _updatables = new List<IUpdatable>();
            _toRegister = new List<IUpdatable>();
            _toUnregister = new List<IUpdatable>();
        }

        public void Update()
        {
            ApplyModifications();

            foreach (IUpdatable updatable in _updatables)
            {
                updatable.Update();
            }
        }

        public void Register(IUpdatable updatable) => _toRegister.Add(updatable);
        
        public void Unregister(IUpdatable updatable) => _toUnregister.Add(updatable);

        private void ApplyModifications()
        {
            if (_toRegister.Count > 0)
            {
                _updatables.AddRange(_toRegister);
                _toRegister.Clear();
            }

            if (_toUnregister.Count > 0)
            {
                foreach (IUpdatable updatable in _toUnregister)
                {
                    _updatables.Remove(updatable);
                }
                
                _toUnregister.Clear();
            }
        }
    }
}