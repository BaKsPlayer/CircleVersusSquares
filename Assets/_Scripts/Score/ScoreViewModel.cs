using UniRx;
using Zenject;

public class ScoreViewModel : IScoreViewModel
{
    private readonly ReactiveProperty<int> _eliminatedSquares;
    private readonly ReactiveProperty<float> _passedDistance;
    private readonly ScoreManager _scoreManager;

    public IReactiveProperty<int> EliminatedSquares => _eliminatedSquares;
    public IReactiveProperty<float> PassedDistance => _passedDistance;

    [Inject]
    public ScoreViewModel(ScoreManager scoreManager)
    {
        _scoreManager = scoreManager;
        _eliminatedSquares = new ReactiveProperty<int>(_scoreManager.EliminatedSquaresCount.Value);
        _passedDistance = new ReactiveProperty<float>(_scoreManager.PassedDistance.Value);

        _scoreManager.EliminatedSquaresCount.Subscribe(value => _eliminatedSquares.Value = value);
        _scoreManager.PassedDistance.Subscribe(value => _passedDistance.Value = value);
    }
}
