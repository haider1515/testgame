using System.Collections.Generic;
using UnityEngine;

namespace Game.Services
{
    public class ShuffleService : IShuffleService
    {
        public void Shuffle<T>(IList<T> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                int j = Random.Range(i, list.Count);
                (list[i], list[j]) = (list[j], list[i]);
            }
        }
    }
}
