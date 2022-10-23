using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBasedController : Singleton<TurnBasedController>
{
    public event Action OnRoundStart;
    public event Action OnRoundFinished;
    [SerializeField] private TurnBasedEntity player1;
    [SerializeField] private TurnBasedEntity player2;
    [SerializeField] private Power_Earthquake _earthquake;
    public Earthquake_Power _earthquakeObject = null;
    public int  _autoActivateRound;
    int _round = 1;
    public int Round => _round;
    TurnBasedEntity _current;
    private bool _firstPlayerTurn=true; 
    public void StartGame()
    {
        var _nextEntity = GetTurnEntity();
        TryChangeTurn(_nextEntity);
        TimeManager.Instance.enabled = true;
    }
    public void OnTurnStarted()
    {
        OnRoundStart?.Invoke();
    }
    public void OnTurnFinished()
    {
        OnRoundFinished?.Invoke();
        if (TimeManager.Instance._playTimeSec <= 0)
        {
            //finishGame

            return;
        }
        if(_earthquakeObject != null)
        {
            if(_autoActivateRound == _round)
            {
                _earthquakeObject = null;
                _earthquake.StartEartQuake(_earthquakeObject._currentGrid, _earthquakeObject._type);
                _earthquakeObject.DestroyWithAnimation();
                return;
            }
        }
        var _nextEntity = GetTurnEntity();
        TryChangeTurn(_nextEntity);
    }
    public bool TryChangeTurn(TurnBasedEntity _nextEntity)
    {
        if(_nextEntity.Freeze)
        {
            _nextEntity.DecreaseFreeze(1);
            _nextEntity = GetTurnEntity();
            TryChangeTurn(_nextEntity);
            return false;
        }
        _current = _nextEntity;
        OnTurnStarted();
        _current.TurnStart();
        _round++;
        return true;
    }
    private TurnBasedEntity GetTurnEntity()
    {
        if(_firstPlayerTurn)
        {
            _firstPlayerTurn = false;
            return player1;
        }
        else
        {
            _firstPlayerTurn = true;
            return player2;
        }
    }
    public TurnBasedEntity GetPlayer(EntityType _type)
    {
        if (player1._playerEntity._type == _type)
        {
            return player1;
        }
        else
        {
            return player2;
        }
    }

}
