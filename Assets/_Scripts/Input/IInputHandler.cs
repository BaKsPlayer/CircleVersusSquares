using System;
using UnityEngine;

public interface IInputHandler 
{
    event Action<Vector2> OnAddPoint;
    event Action OnClickOnPlayer;
}
