using UnityEngine;
using Zenject;

public class GameplayInstaller : MonoInstaller
{
    [SerializeField]
    private PlayerMovement _playerPrefab;

    [SerializeField]
    private SquaresSpawner _squaresSpawnerPrefab;

    public override void InstallBindings()
    {
        BindPlayer();
        BindSquaresSpawner();
    }

    private void BindPlayer()
    {
        Container.InstantiatePrefabForComponent<PlayerMovement>(_playerPrefab);
    }

    private void BindSquaresSpawner()
    {
        Container.InstantiatePrefabForComponent<SquaresSpawner>(_squaresSpawnerPrefab);
    }
}