using UnityEngine;
using Zenject;

namespace Installers
{
    [CreateAssetMenu(menuName = "Snake/Game Settings")]
    public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
    {
        public override void InstallBindings()
        {
            
        }
    }
}