using Draw;
using Logic;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private AppleDraw appleDraw;
        [SerializeField] private SnakeDraw snakeDraw;
        [SerializeField] private Ui ui;
        [SerializeField] private MapField mapField;

        public override void InstallBindings()
        {
            Container.Bind<IMapField>().FromInstance(mapField);
            Container.Bind<IUiDraw>().FromInstance(ui);
            
            Container.BindInterfacesAndSelfTo<InputSnake>().AsSingle();
            Container.BindInterfacesAndSelfTo<StepSnakeTick>().AsSingle();

            Container.Bind<IApple>().To<Apple>().AsSingle().WithArguments(appleDraw);
            Container.Bind<ISnake>().To<Snake>().AsSingle().WithArguments(snakeDraw);

            Container.BindInterfacesAndSelfTo<GameController>().AsSingle();
        }
    }
}