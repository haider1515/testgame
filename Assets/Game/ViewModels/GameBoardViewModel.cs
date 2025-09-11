using System.Collections.Generic;

namespace Game.ViewModels
{
    public class GameBoardViewModel : IGameBoardViewModel
    {
        private readonly List<ICardViewModel> _cards = new();

        public IReadOnlyList<ICardViewModel> Cards => _cards;

        public void AddCard(ICardViewModel vm) => _cards.Add(vm);

        public void Clear() => _cards.Clear();
    }
}
