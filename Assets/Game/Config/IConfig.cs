namespace Game.Config
{
    public interface IConfig
    {
        int Rows { get; }
        int Columns { get; }
        float MatchDelay { get; }
    }
}
