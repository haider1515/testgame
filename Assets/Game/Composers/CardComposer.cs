using UnityEngine;
using Game.Factories;
using Game.ViewModels;

namespace Game.Composers
{
    public class CardComposer : ICardComposer
    {
        private readonly ICardFactory cardFactory;

        public CardComposer(ICardFactory cardFactory)
        {
            this.cardFactory = cardFactory;
        }

        public void Compose(int id, string spriteKey, Sprite sprite)
        {
            var vm = cardFactory.CreateViewModel(id, spriteKey);
            cardFactory.CreateView(vm, sprite);
        }

        public void Compose(ICardViewModel viewModel, Sprite sprite)
        {
            cardFactory.CreateView(viewModel, sprite);
        }
    }
}
