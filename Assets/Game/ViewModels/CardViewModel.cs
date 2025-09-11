using System;
using Game.Models;

namespace Game.ViewModels
{
    public class CardViewModel : ICardViewModel
    {
        private readonly CardModel model;

        public int Id => model.Id;
        public string SpriteKey => model.SpriteKey;
        public bool IsRevealed { get; private set; }
        public bool IsMatched { get; private set; }

        public event Action<ICardViewModel> OnRevealed;
        public event Action<ICardViewModel> OnHidden;
        public event Action<ICardViewModel> OnMatched;

        public CardViewModel(CardModel cardModel)
        {
            model = cardModel;
        }

        public void Reveal()
        {
            if (IsMatched || IsRevealed) return;
            IsRevealed = true;
            OnRevealed?.Invoke(this);
        }

        public void Hide()
        {
            IsRevealed = false;
            OnHidden?.Invoke(this);
        }

        public void Match()
        {
            if (IsMatched) return;
            IsMatched = true;
            OnMatched?.Invoke(this);
        }
    }
}
