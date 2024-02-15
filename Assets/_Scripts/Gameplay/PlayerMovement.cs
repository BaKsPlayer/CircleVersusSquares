using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _movementSpeed = 5f;

    private Vector2 _currentPosition => (Vector2)transform.position;
    private Queue<Vector2> _pointsQueue = new Queue<Vector2>();
    private bool _isMoving;

    private IInputHandler _inputHandler;
    private ScoreManager _scoreManager;

    [Inject]
    private void Construct(IInputHandler inputHandler, ScoreManager scoreManager)
    {
        _inputHandler = inputHandler;
        _inputHandler.OnAddPoint += AddPoint;
        _inputHandler.OnClickOnPlayer += StopMove;
        _scoreManager = scoreManager;
    }

    private void AddPoint(Vector2 position)
    {
        _pointsQueue.Enqueue(position);
        if (_isMoving)
        {
            return;
        }
        LaunchMove();
    }

    private void StopMove()
    {
        _pointsQueue.Clear();
        StopAllCoroutines();
        _isMoving = false;
    }

    private void LaunchMove()
    {
        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        _isMoving = true;
        while (_pointsQueue.Count > 0)
        {
            var targetPosition = _pointsQueue.Dequeue();
            while (_currentPosition != targetPosition)
            {
                var distanceDelta = _movementSpeed * Time.deltaTime;
                transform.position = Vector2.MoveTowards(_currentPosition, targetPosition, _movementSpeed * Time.deltaTime);
                _scoreManager.DistancePassed(distanceDelta);
                yield return null;
            }
        }
        _isMoving = false;
    }

    private void OnDestroy()
    {
        _inputHandler.OnAddPoint -= AddPoint;
        _inputHandler.OnClickOnPlayer -= StopMove;
        StopMove();
    }
}
