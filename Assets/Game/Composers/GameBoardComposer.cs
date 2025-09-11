using Game.ViewModels;
using Game.Factories;
using UnityEngine;

namespace Game.Composers
{
    public class GameBoardComposer : IGameBoardComposer
    {
        private readonly IGameBoardFactory boardFactory;
        private readonly ICardFactory cardFactory;

        public GameBoardComposer(IGameBoardFactory boardFactory, ICardFactory cardFactory)
        {
            this.boardFactory = boardFactory;
            this.cardFactory = cardFactory;
        }

        public IGameBoardViewModel Compose(int rows, int cols)
        {
            var boardVM = boardFactory.CreateViewModel(rows, cols);

            foreach (var cardVM in boardVM.Cards)
            {
                Sprite sprite = boardFactory.GetSprite(cardVM.SpriteKey);
                cardFactory.CreateView(cardVM, sprite);
            }

            return boardVM;
        }
    }
}
