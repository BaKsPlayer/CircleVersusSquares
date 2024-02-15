using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ScoreView : MonoBehaviour
{
    [SerializeField]
    private Text _eliminatedSquaresLabel;

    [SerializeField]
    private Text _passedDistanceLabel;

    private IScoreViewModel _viewModel;

    [Inject]
    public void Init(IScoreViewModel viewModel)
    {
        _viewModel = viewModel;

        _viewModel.EliminatedSquares.Subscribe(value => _eliminatedSquaresLabel.text = value.ToString());
        _viewModel.PassedDistance.Subscribe(value => _passedDistanceLabel.text = value.ToString());
    }
}
