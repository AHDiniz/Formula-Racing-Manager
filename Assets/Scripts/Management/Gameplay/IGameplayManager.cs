using UnityEngine;

namespace FormulaManager.Management.Gameplay
{
    public interface IGameplayManager
    {
        bool IsDone { get; }

        void Initialize();
        void Tick();
        void Finish();
    }
}