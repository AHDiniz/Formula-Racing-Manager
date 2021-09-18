namespace FormulaManager.Management.Global
{
    public interface IGameMode
    {
        string SceneName { get; }

        void Initialize();
        void Finish();
    }
}