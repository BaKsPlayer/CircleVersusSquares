using UnityEngine;
using Zenject;

public class ScoreInstaller : MonoInstaller
{
    [SerializeField]
    private RectTransform _scoreViewContainer;

    [SerializeField]
    private ScoreView _scoreViewPrefab;

    public override void InstallBindings()
    {
        BindScoreManager();
        BindScoreView();
    }

    private void BindScoreManager()
    {
        Container.BindInterfacesAndSelfTo<ScoreManager>().AsSingle();
    }

    private void BindScoreView()
    {
        Container.BindInterfacesAndSelfTo<ScoreViewModel>().AsSingle();
        Container.InstantiatePrefabForComponent<ScoreView>(_scoreViewPrefab, _scoreViewContainer);
    }
}