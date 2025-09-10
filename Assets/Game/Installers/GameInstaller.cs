using UnityEngine;
using Zenject;

using Game.Views;
using System.Collections.Generic;

namespace Game.Installers
{
    public class GameInstaller : MonoInstaller
    {
        public CardView cardPrefab;
        public List<Sprite> availableSprites;

        public override void InstallBindings()
        {
            Container.Bind<CardView>().FromInstance(cardPrefab).AsSingle();
            Container.Bind<List<Sprite>>().FromInstance(availableSprites).AsSingle();
        }
    }
}
