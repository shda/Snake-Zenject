using Draw;
using Logic;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private AppleDraw appleDraw;
        [SerializeField] private SnakeDraw snakeDraw;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<InputSnake>().AsSingle();
            Container.BindInterfacesAndSelfTo<StepSnakeTick>().AsSingle();

            Container.Bind<IApple>().To<Apple>().AsSingle().WithArguments(appleDraw);
            Container.Bind<ISnake>().To<Snake>().AsSingle().WithArguments(snakeDraw);

            Container.BindInterfacesAndSelfTo<GameController>().AsSingle();
        }
    }
}