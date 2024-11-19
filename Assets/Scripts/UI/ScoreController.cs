using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static GameEnums;
using DG.Tweening;

public class ScoreController : MonoBehaviour
{
    [SerializeField] float _duration;

    TextMeshProUGUI _txtScore;
    int _score = 0;

    private void Start()
    {
        _txtScore = GetComponent<TextMeshProUGUI>();
        EventsManager.Instance.Subcribe(EventID.OnTrackedImageSuccess, AddScore);
    }

    private void OnDestroy()
    {
        EventsManager.Instance.Unsubcribe(EventID.OnTrackedImageSuccess, AddScore);
    }

    private void AddScore(object obj)
    {
        Question questInfo = (Question)obj;
        DOTween.To(() => _score, x => _score = x, _score + questInfo.Score, _duration).OnUpdate(() => _txtScore.text = "Score: " +_score.ToString());
    }
}