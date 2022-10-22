using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBasedController : ComponentBase
{
    [SerializeField] private TurnBasedEntity player1;
    [SerializeField] private TurnBasedEntity player2;
    TurnBasedEntity _current;
    private bool _firstPlayerTurn=true;
    protected override void Start()
    {
        base.OnEnable();
        var _nextEntity= GetTurnEntity();
        TryChangeTurn(_nextEntity);
    }
    public void OnTurnStarted()
    {

    }
    public void OnTurnFinished()
    {
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

}
