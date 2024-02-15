using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseInput : MonoBehaviour, IInput, IBeginDragHandler, IDragHandler, IPointerClickHandler
{
    private Action<PointerEventData> _onClick;
    private Action<PointerEventData> _onBeginDrag;
    private Action<PointerEventData> _onDrag;

    event Action<PointerEventData> IInput.OnClick
    {
        add => _onClick += value;
        remove => _onClick -= value;
    }

    event Action<PointerEventData> IInput.OnBeginDrag
    {
        add => _onBeginDrag += value;
        remove => _onBeginDrag -= value;
    }

    event Action<PointerEventData> IInput.OnDrag
    {
        add => _onDrag += value;
        remove => _onDrag -= value;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _onClick?.Invoke(eventData);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _onBeginDrag?.Invoke(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        _onDrag?.Invoke(eventData);
    }
}
