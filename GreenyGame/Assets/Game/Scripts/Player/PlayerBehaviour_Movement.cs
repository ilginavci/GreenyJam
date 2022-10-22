using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour_Movement : MonoBehaviour
{
   [SerializeField] Entity _playerEntity;
   [SerializeField] TurnBasedEntity _turnBasedEntity;
   [SerializeField] float _speed;
   [SerializeField] float _jumpPow;
   [SerializeField] float _jumpSpeed;
    public void GoToGrid(GridElement _nextGrid)
   {
        bool _jump=false;
        if (_playerEntity._currentGrid != null)
        {

            ReleaseGrid(_playerEntity._currentGrid);

            float _distance = Vector2.Distance(_playerEntity._currentGrid.position, _nextGrid.position);
            if(_distance > 1)
            {
                _jump = true;
            }
        }
        // Entity Check for points 

        SetGrid(_nextGrid);

        //Movement (Animation)
        if(_jump)
        {
            Vector3 newPos = new Vector3(_playerEntity._currentGrid.position.x, transform.position.y, _playerEntity._currentGrid.position.y);
            transform.DOJump(newPos, _jumpPow ,1,_jumpSpeed).SetEase(Ease.InOutQuad).
                OnComplete(() =>
                {
                    _turnBasedEntity.TurnFinished();
                }
            );
        }
        else
        {
            Vector3 newPos = new Vector3(_playerEntity._currentGrid.position.x, transform.position.y, _playerEntity._currentGrid.position.y);
            transform.DOMove(newPos, _speed).SetEase(Ease.InOutQuad).SetSpeedBased().
                OnComplete(()=> 
                {
                    _turnBasedEntity.TurnFinished();
                }
            );
        }

    }

    private void SetGrid(GridElement _grid)
    {
        _playerEntity._currentGrid= _grid;
        _playerEntity._currentGrid._entity = _playerEntity;

        _grid.SetBusy();
    }

    private void ReleaseGrid(GridElement _grid)
    {
        _grid._entity = null;
        _grid.SetAvailable();
    }
}
