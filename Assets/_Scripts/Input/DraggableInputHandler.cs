using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class DraggableInputHandler : IInputHandler, IDisposable
{
    private Vector3 _lastDragPoint;
    private IInput _input;

    [Inject]
    public DraggableInputHandler(IInput input)
    {
        _input = input;
        _input.OnClick += Click;
        _input.OnBeginDrag += BeginDrag;
        _input.OnDrag += Drag;
    }

    public event Action<Vector2> OnAddPoint;
    public event Action OnClickOnPlayer;

    private void Click(PointerEventData eventData)
    {
        var raycastResult = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raycastResult);
        if (raycastResult.Any(result => result.gameObject.TryGetComponent<PlayerMovement>(out var player)))
        {
            OnClickOnPlayer?.Invoke();
        }
        else
        {
            AddPoint(eventData);
        }
    }

    private void BeginDrag(PointerEventData eventData)
    {
        _lastDragPoint = eventData.position;
    }

    private void Drag(PointerEventData eventData)
    {
        //TODO: настройку дистанции при которой ставится точка лучше вынести в json, но пока не реализую такую систему оставляю магическое число :)
        if ((_lastDragPoint - Camera.main.ScreenToWorldPoint(eventData.position)).magnitude > 1f)
        {
            _lastDragPoint = Camera.main.ScreenToWorldPoint(eventData.position);
            AddPoint(eventData);
        }
    }

    private void AddPoint(PointerEventData eventData)
    {
        OnAddPoint?.Invoke(Camera.main.ScreenToWorldPoint(eventData.position));
    }

    public void Dispose()
    {
        _input.OnBeginDrag -= BeginDrag;
        _input.OnClick -= Click;
        _input.OnDrag -= Drag;
    }
}
