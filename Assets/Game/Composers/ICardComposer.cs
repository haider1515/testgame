using Game.ViewModels;
using UnityEngine;

namespace Game.Composers
{
    public interface ICardComposer
    {
        void Compose(int id, string spriteKey, Sprite sprite);
        void Compose(ICardViewModel viewModel, Sprite sprite);
    }
}
