using UnityEngine;
using Zenject;
using Game.Models;
using Game.ViewModels;
using Game.Views;

namespace Game.Factories
{
    public class CardFactory : ICardFactory
    {
        private readonly DiContainer container;
        private readonly CardView cardPrefab;
        private readonly GameBoardView boardView;

        public CardFactory(DiContainer container, CardView cardPrefab, GameBoardView boardView)
        {
            this.container = container;
            this.cardPrefab = cardPrefab;
            this.boardView = boardView;
        }

        public ICardViewModel CreateViewModel(int id, string spriteKey)
        {
            var model = new CardModel(id, spriteKey);
            return new CardViewModel(model);
        }

        public CardView CreateView(ICardViewModel viewModel, Sprite sprite)
        {
            var view = container.InstantiatePrefabForComponent<CardView>(cardPrefab, boardView.gridParent);
            view.Bind(viewModel, sprite);
            return view;
        }
    }
}
