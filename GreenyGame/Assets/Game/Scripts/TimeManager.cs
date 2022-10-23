using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class TimeManager : Singleton<TimeManager>
{
    [SerializeField] public  float _playTimeSec;
    [SerializeField] Text _text;
    Coroutine _timeTask;
    private void OnEnable()
    {
        if(_timeTask == null)
        {
            _timeTask = StartCoroutine(Countdown());
        }
        _text.DOFade(1, 1f);
    }
    IEnumerator Countdown()
    {
        while (_playTimeSec > 0)
        {
            yield return new WaitForSeconds(1);
            _playTimeSec -= 1;
        }
    }
}
