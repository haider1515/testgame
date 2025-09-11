using Zenject;
using Game.Controllers;
using Game.Services;
using Game.ViewModels;
using Game.Views;
using Game.Factories;
using Game.Composers;
using System.Collections.Generic;
using UnityEngine;
using Game.Config;

namespace Game.Installers
{
    public class GameInstaller : MonoInstaller
    {
        public GameBoardView boardView;
        public CardView cardPrefab;
        public List<Sprite> availableSprites;

        public override void InstallBindings()
        {
            var config = ConfigLoader.LoadConfig();
            Container.Bind<IConfig>().FromInstance(config).AsSingle();

            Container.Bind<GameBoardView>().FromInstance(boardView).AsSingle();
            Container.Bind<CardView>().FromInstance(cardPrefab).AsSingle();
            Container.Bind<List<Sprite>>().FromInstance(availableSprites).AsSingle();

            Container.Bind<IGameBoardViewModel>().To<GameBoardViewModel>().AsTransient();
            Container.Bind<ICardViewModel>().To<CardViewModel>().AsTransient();

            Container.Bind<IShuffleService>().To<ShuffleService>().AsSingle();

            Container.Bind<ICardFactory>().To<CardFactory>().AsSingle();
            Container.Bind<IGameBoardFactory>().To<GameBoardFactory>().AsSingle();

            Container.Bind<ICardComposer>().To<CardComposer>().AsSingle();
            Container.Bind<IGameBoardComposer>().To<GameBoardComposer>().AsSingle();

            Container.BindInterfacesAndSelfTo<GameController>().AsSingle().NonLazy();

            Container.Bind<IScoreViewModel>().To<ScoreViewModel>().AsSingle();

        }
    }
}
