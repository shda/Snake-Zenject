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
        [SerializeField] private Ui ui;
        [SerializeField] private GameField gameField;

        public override void InstallBindings()
        {
            Container.Bind<IGameField>().FromInstance(gameField);
            Container.Bind<IUiDraw>().FromInstance(ui);
            
            Container.BindInterfacesAndSelfTo<InputSnake>().AsSingle();
            Container.BindInterfacesAndSelfTo<StepSnakeTick>().AsSingle();

            Container.Bind<IApple>().To<Apple>().AsSingle().WithArguments(appleDraw);
            Container.Bind<ISnake>().To<Snake>().AsSingle().WithArguments(snakeDraw);

            Container.BindInterfacesAndSelfTo<GameController>().AsSingle();
        }
    }
}