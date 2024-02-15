using UnityEngine;
using Zenject;

public class InputInstaller : MonoInstaller
{
    [SerializeField]
    private RectTransform _mouseInputContainer;

    [SerializeField]
    private MouseInput _mouseInputPrefab;

    public override void InstallBindings()
    {
        BindInput();
        BindInputHandler();
    }

    private void BindInput()
    {
        var mouseInput = Container.InstantiatePrefabForComponent<IInput>(_mouseInputPrefab, _mouseInputContainer);
        Container.BindInstance(mouseInput);
    }

    private void BindInputHandler()
    {
        Container.BindInterfacesAndSelfTo<DraggableInputHandler>().AsSingle();
    }
}