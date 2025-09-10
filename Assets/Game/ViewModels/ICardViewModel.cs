using System;

namespace Game.ViewModels
{
    public interface ICardViewModel
    {
        int Id { get; }
        string SpriteKey { get; }
        bool IsRevealed { get; }
        bool IsMatched { get; }

        event Action<ICardViewModel> OnRevealed;
        event Action<ICardViewModel> OnHidden;
        event Action<ICardViewModel> OnMatched;

        void Reveal();
        void Hide();
        void Match();
    }
}
