using Settings;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Installers
{
    [CreateAssetMenu(menuName = "Snake/Game Settings")]
    public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
    {
        public SnakeSettings SnakeSettings;
        public MapSettings MapSettings;
        
        public override void InstallBindings()
        {
            Container.BindInstance(SnakeSettings);
            Container.BindInstance(MapSettings);
        }
    }
}