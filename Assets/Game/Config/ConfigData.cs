using System;

namespace Game.Config
{
    [Serializable]
    public class ConfigData : IConfig
    {
        public int rows = 4;
        public int columns = 4;
        public float matchDelay = 1.0f;

        public int Rows => rows;
        public int Columns => columns;
        public float MatchDelay => matchDelay;
    }
}
