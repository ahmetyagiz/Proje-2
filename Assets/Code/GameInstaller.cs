using Zenject;

/// <summary>
/// Bu kod Zenject bind eklenmesinden sorumludur.
/// </summary>
public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<GameManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<ComboSoundManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<PlatformTransferManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<PlatformInformationManager>().FromComponentInHierarchy().AsSingle();
    }
}