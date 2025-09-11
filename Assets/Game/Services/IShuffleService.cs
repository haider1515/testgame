using System.Collections.Generic;

namespace Game.Services
{
    public interface IShuffleService
    {
        void Shuffle<T>(IList<T> list);
    }
}
