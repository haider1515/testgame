using Game.ViewModels;
using UnityEngine;

namespace Game.Factories
{
    public interface IGameBoardFactory
    {
        IGameBoardViewModel CreateViewModel(int rows, int cols);
        void CreateView(IGameBoardViewModel boardVM);
        Sprite GetSprite(string spriteKey);
    }
}
