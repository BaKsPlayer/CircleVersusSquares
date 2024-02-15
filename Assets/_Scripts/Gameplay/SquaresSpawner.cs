using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class SquaresSpawner : MonoBehaviour
{
    [SerializeField]
    private int _maxSquaresCount = 5;

    [SerializeField]
    private float _spawnPeriodicity = 2f;

    [SerializeField]
    private Square _squarePrefab;

    private List<Square> _squaresPool = new List<Square>();
    private ScoreManager _scoreManager;
    private int _activeSquaresCount;

    [Inject]
    private void Construct(ScoreManager scoreManager)
    {
        _scoreManager = scoreManager;
    }

    private void Start()
    {
        SpawnSquare();
        Observable.Interval(GetTimeSpan(_spawnPeriodicity)).Subscribe(x => SpawnSquare());
    }

    private void SpawnSquare()
    {
        if (_activeSquaresCount == _maxSquaresCount)
        {
            return;
        }

        var randomPositionInPixels = new Vector2(Random.Range(0, Camera.main.pixelWidth), Random.Range(0, Camera.main.pixelHeight));
        var squarePosition = Camera.main.ScreenToWorldPoint(randomPositionInPixels);
        GetSquare().Spawn(squarePosition);
        _activeSquaresCount++;
    }

    private Square GetSquare()
    {
        foreach (var square in _squaresPool)
        {
            if (!square.gameObject.activeSelf)
            {
                return square;
            }
        }

        var spawnedSquare = Instantiate(_squarePrefab, transform);
        spawnedSquare.Init(OnSquareHit);
        _squaresPool.Add(spawnedSquare);

        return spawnedSquare;
    }

    private void OnSquareHit()
    {
        _activeSquaresCount--;
        _scoreManager.SquareEliminated();
    }

    //TODO: По-хорошему бы вынести в Extension класс, но ради одного метода в маленьком проекте не посчитал нужным
    private TimeSpan GetTimeSpan(float timeInSeconds)
    {
        return new TimeSpan(0, 0, 0, 0, (int)(timeInSeconds * 1000));
    }
}
