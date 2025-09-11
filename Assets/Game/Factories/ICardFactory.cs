using Game.ViewModels;
using Game.Views;

namespace Game.Factories
{
    public interface ICardFactory
    {
        ICardViewModel CreateViewModel(int id, string spriteKey);
        CardView CreateView(ICardViewModel viewModel, UnityEngine.Sprite sprite);
    }
}
