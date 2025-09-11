using System.Collections.Generic;

namespace Game.ViewModels
{
    public interface IGameBoardViewModel
    {
        IReadOnlyList<ICardViewModel> Cards { get; }

        void AddCard(ICardViewModel vm);
        void Clear();
    }
}
