using Game.ViewModels;

namespace Game.Composers
{
    public interface IGameBoardComposer
    {
        IGameBoardViewModel Compose(int rows, int cols);
    }
}
