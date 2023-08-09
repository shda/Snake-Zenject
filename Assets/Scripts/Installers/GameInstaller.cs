using Draw;
using Logic;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<InputSnake>().AsSingle();
            Container.BindInterfacesAndSelfTo<StepSnakeTick>().AsSingle();
            
            Container.Bind<IApple>().To<Apple>().AsSingle();
            Container.Bind<ISnake>().To<Snake>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<EatAppleHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameOverHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<MovingSnakeHandler>().AsSingle();
        }
    }
}