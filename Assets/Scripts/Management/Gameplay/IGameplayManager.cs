namespace FormulaManager.Management.Gameplay
{
    public interface IGameplayManager
    {
        void Initialize();
        void Tick();
        void Finish();
    }
}