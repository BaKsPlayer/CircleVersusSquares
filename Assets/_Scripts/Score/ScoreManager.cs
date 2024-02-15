using System;
using UniRx;
using UnityEngine;

public class ScoreManager : IDisposable
{
    private const string ELIMINATED_SQUARES_KEY = "EliminatedSquares";
    private const string PASSED_DISTANCE_KEY = "PassedDistance";

    private readonly ReactiveProperty<int> _eliminatedSquaresCount;
    private readonly ReactiveProperty<float> _passedDistance;

    public IReactiveProperty<int> EliminatedSquaresCount => _eliminatedSquaresCount;
    public IReactiveProperty<float> PassedDistance => _passedDistance;

    public ScoreManager()
    {
        _eliminatedSquaresCount = new ReactiveProperty<int>(PlayerPrefs.GetInt(ELIMINATED_SQUARES_KEY));
        _passedDistance = new ReactiveProperty<float>(PlayerPrefs.GetFloat(PASSED_DISTANCE_KEY));
    }

    public void SquareEliminated()
    {
        _eliminatedSquaresCount.Value++;
    }

    public void DistancePassed(float delta)
    {
        _passedDistance.Value += delta;
    }

    private void SaveData()
    {
        PlayerPrefs.SetInt(ELIMINATED_SQUARES_KEY, _eliminatedSquaresCount.Value);
        PlayerPrefs.SetFloat(PASSED_DISTANCE_KEY, _passedDistance.Value);
    }

    public void Dispose()
    {
        SaveData();
    }
}
