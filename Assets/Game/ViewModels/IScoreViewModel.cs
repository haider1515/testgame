using System;
using System.Collections.Generic;

namespace Game.ViewModels
{
    public interface IScoreViewModel
    {
        int Score { get; }
        IReadOnlyList<int> LastScores { get; }

        event Action<int> OnScoreChanged;
        event Action<IReadOnlyList<int>> OnLastScoresChanged;

        void AddPoints(int amount);
        void SubtractPoints(int amount);
        void Reset();
        void SaveScore();
        void LoadScores();
    }
}
