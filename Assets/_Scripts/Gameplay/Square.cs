using System;
using UnityEngine;

public class Square : MonoBehaviour
{
    private Action _onHit;

    public void Init(Action onHit)
    {
        _onHit = onHit;
    }

    public void Spawn(Vector2 position)
    {
        gameObject.SetActive(true);
        transform.position = position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<PlayerMovement>(out var player))
        {
            gameObject.SetActive(false);
            _onHit?.Invoke();
        }
    }
}
