using UnityEngine;

namespace FormulaManager.Management.Global
{
    public interface IGameMode
    {
        string SceneName { get; }

        void Initialize(GameObject[] managers);
        void Finish();
    }
}