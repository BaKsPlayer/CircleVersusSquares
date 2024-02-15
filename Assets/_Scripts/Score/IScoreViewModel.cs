using UniRx;

public interface IScoreViewModel
{
    IReactiveProperty<int> EliminatedSquares { get; }
    IReactiveProperty<float> PassedDistance { get; }
}
