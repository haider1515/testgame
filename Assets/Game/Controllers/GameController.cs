using Zenject;
using Game.Composers;
using Game.ViewModels;
using Game.Config;
using UnityEngine;
using System.Collections.Generic;

namespace Game.Controllers
{
    public class GameController : IInitializable, ITickable
    {
        private readonly IGameBoardComposer _boardComposer;
        private readonly IConfig _config;
        private readonly IScoreViewModel _scoreVM;

        private IGameBoardViewModel _boardVM;

        private readonly List<ICardViewModel> _session = new();
        private readonly List<ICardViewModel> _matched = new();

        private bool _waitingToHide;
        private float _timer;

        [Inject]
        public GameController(
            IGameBoardComposer boardComposer,
            IConfig config,
            IScoreViewModel scoreVM)
        {
            _boardComposer = boardComposer;
            _config = config;
            _scoreVM = scoreVM;
        }

        public void Initialize()
        {
            _boardVM = _boardComposer.Compose(_config.Rows, _config.Columns);

            foreach (var card in _boardVM.Cards)
                card.OnRevealed += OnCardRevealed;

            _scoreVM.Reset();

            Debug.Log($"[GameController] Game started with {_boardVM.Cards.Count} cards");
        }

        private void OnCardRevealed(ICardViewModel card)
        {
            if (card.IsMatched || !card.IsRevealed) return;

            _session.Add(card);

            if (_session.Count >= 2)
            {
                var last = _session[_session.Count - 1];
                var prev = _session[_session.Count - 2];

                if (last.Id == prev.Id && last != prev)
                {
                    // ✅ Match
                    last.Match();
                    prev.Match();

                    _matched.Add(last);
                    _matched.Add(prev);

                    _session.Remove(last);
                    _session.Remove(prev);

                    _scoreVM.AddPoints(10);
                    Debug.Log($"[MATCH] Pair ID={last.Id} | Score={_scoreVM.Score}");
                }
                else
                {
                    _waitingToHide = true;
                    _timer = Mathf.Max(0.5f, _config.MatchDelay);

                    _scoreVM.SubtractPoints(2);
                    Debug.Log($"[MISMATCH] {_session.Count} open cards | Score={_scoreVM.Score}");
                }
            }
            else
            {
                _waitingToHide = true;
                _timer = Mathf.Max(0.5f, _config.MatchDelay);
            }
        }

        public void Tick()
        {
            if (!_waitingToHide) return;

            _timer -= Time.deltaTime;
            if (_timer <= 0f)
            {
                HideSession();
                _waitingToHide = false;
            }
        }

        private void HideSession()
        {
            foreach (var card in _session)
            {
                if (!card.IsMatched && card.IsRevealed)
                {
                    card.Hide();
                    Debug.Log($"[HIDE] ID={card.Id}");
                }
            }
            _session.Clear();
        }
    }
}
