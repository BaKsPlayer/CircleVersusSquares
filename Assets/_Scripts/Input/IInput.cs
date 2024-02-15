using System;
using UnityEngine.EventSystems;

public interface IInput 
{
    event Action<PointerEventData> OnClick;
    event Action<PointerEventData> OnBeginDrag;
    event Action<PointerEventData> OnDrag;
}
