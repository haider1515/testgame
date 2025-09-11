using System.Collections.Generic;
using UnityEngine;
using Game.ViewModels;
using Game.Services;

namespace Game.Factories
{
    public class GameBoardFactory : IGameBoardFactory
    {
        private readonly ICardFactory cardFactory;
        private readonly IShuffleService shuffleService;
        private readonly List<Sprite> availableSprites;

        public GameBoardFactory(
            ICardFactory cardFactory,
            IShuffleService shuffleService,
            List<Sprite> availableSprites)
        {
            this.cardFactory = cardFactory;
            this.shuffleService = shuffleService;
            this.availableSprites = availableSprites;
        }

        public IGameBoardViewModel CreateViewModel(int rows, int cols)
        {
            var board = new GameBoardViewModel();

            int total = rows * cols;
            int pairs = total / 2;
            int spriteCount = Mathf.Max(1, availableSprites.Count);

            for (int i = 0; i < pairs; i++)
            {
                string spriteKey = $"sprite{i % spriteCount}";

                var vm1 = cardFactory.CreateViewModel(i, spriteKey);
                var vm2 = cardFactory.CreateViewModel(i, spriteKey);

                board.AddCard(vm1);
                board.AddCard(vm2);
            }

            if (total % 2 != 0 && pairs > 0)
            {
                int lastId = pairs - 1;
                string spriteKey = $"sprite{(pairs - 1) % spriteCount}";
                var extra = cardFactory.CreateViewModel(lastId, spriteKey);
                board.AddCard(extra);
            }

            var tmp = new List<ICardViewModel>(board.Cards);
            shuffleService.Shuffle(tmp);
            board.Clear();
            foreach (var vm in tmp)
                board.AddCard(vm);

            return board;
        }

        public void CreateView(IGameBoardViewModel boardVM)
        {
            foreach (var cardVM in boardVM.Cards)
            {
                var sprite = ResolveSprite(cardVM.SpriteKey);
                cardFactory.CreateView(cardVM, sprite);
            }
        }

        private Sprite ResolveSprite(string spriteKey)
        {
            if (!string.IsNullOrEmpty(spriteKey) && spriteKey.StartsWith("sprite"))
            {
                if (int.TryParse(spriteKey.Substring(6), out int index) && availableSprites.Count > 0)
                {
                    int clamped = Mathf.Abs(index) % availableSprites.Count;
                    return availableSprites[clamped];
                }
            }
            return null;
        }

        public Sprite GetSprite(string spriteKey)
        {
            if (!string.IsNullOrEmpty(spriteKey) && spriteKey.StartsWith("sprite"))
            {
                if (int.TryParse(spriteKey.Substring(6), out int index) && availableSprites.Count > 0)
                {
                    return availableSprites[index % availableSprites.Count];
                }
            }
            return null;
        }

    }
}
