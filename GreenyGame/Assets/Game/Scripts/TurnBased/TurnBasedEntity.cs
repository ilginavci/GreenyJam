using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBasedEntity : ComponentBase
{
    [Header("TurnBasedSetting")]
    [SerializeField] int _freezeRoundNumber;
    public PlayerEntity _playerEntity;
    [SerializeField] TurnBasedController _turnBasedController;
    public bool Freeze {
        get { return CheckFreeze(); }
        set { FreezeEntity(); }
    }

    int freezeRemainingRound;
    public int Freeze_RemainingRound => freezeRemainingRound;
    public void TurnStart()
    {
        _playerEntity.GetTurn();
    }
    public void TurnFinished()
    {
        _turnBasedController.OnTurnFinished();
    }
    private bool CheckFreeze()
    {
        if(Freeze_RemainingRound > 0)
        {
            return true;
        }
        return false;
    }
    public void DecreaseFreeze(int value)
    {
        freezeRemainingRound -= value;
    }
    public void FreezeEntity()
    {
        freezeRemainingRound = _freezeRoundNumber;
    }
}
