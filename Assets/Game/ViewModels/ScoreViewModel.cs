using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.ViewModels
{
    public class ScoreViewModel : IScoreViewModel
    {
        private const string LastScoresKey = "LastScores";
        private const int MaxScores = 10;

        private int _score;
        private readonly List<int> _lastScores = new();

        public int Score => _score;
        public IReadOnlyList<int> LastScores => _lastScores.AsReadOnly();

        public event Action<int> OnScoreChanged;
        public event Action<IReadOnlyList<int>> OnLastScoresChanged;

        public void AddPoints(int amount)
        {
            _score += amount;
            OnScoreChanged?.Invoke(_score);
        }

        public void SubtractPoints(int amount)
        {
            _score -= amount;
            if (_score < 0) _score = 0;
            OnScoreChanged?.Invoke(_score);
        }

        public void Reset()
        {
            _score = 0;
            OnScoreChanged?.Invoke(_score);
        }

        public void SaveScore()
        {
            _lastScores.Insert(0, _score);
            if (_lastScores.Count > MaxScores)
                _lastScores.RemoveAt(_lastScores.Count - 1);

            string data = string.Join(",", _lastScores);
            PlayerPrefs.SetString(LastScoresKey, data);
            PlayerPrefs.Save();

            OnLastScoresChanged?.Invoke(_lastScores.AsReadOnly());
        }

        public void LoadScores()
        {
            _lastScores.Clear();

            if (PlayerPrefs.HasKey(LastScoresKey))
            {
                string data = PlayerPrefs.GetString(LastScoresKey);
                string[] parts = data.Split(',');

                foreach (var part in parts)
                {
                    if (int.TryParse(part, out int val))
                        _lastScores.Add(val);
                }
            }

            OnLastScoresChanged?.Invoke(_lastScores.AsReadOnly());
        }
    }
}
