using UnityEngine;
using UnityEngine.UI;
using Game.ViewModels;
using System.Text;
using Zenject;

namespace Game.Views
{
    public class ScoreView : MonoBehaviour
    {
        public TMPro.TMP_Text scoreText;
        public TMPro.TMP_Text historyText;

        [Inject]
        public void Construct(IScoreViewModel vm)
        {
            vm.OnScoreChanged += UpdateScore;
            vm.OnLastScoresChanged += UpdateHistory;

            vm.LoadScores();
            UpdateScore(vm.Score);
        }

        private void UpdateScore(int score)
        {
            if (scoreText != null)
                scoreText.text = $"Score: {score}";
        }

        private void UpdateHistory(System.Collections.Generic.IReadOnlyList<int> scores)
        {
            if (historyText == null) return;

            StringBuilder sb = new StringBuilder("Last Scores:\n");
            for (int i = 0; i < scores.Count; i++)
            {
                sb.AppendLine($"{i + 1}. {scores[i]}");
            }
            historyText.text = sb.ToString();
        }
    }
}
