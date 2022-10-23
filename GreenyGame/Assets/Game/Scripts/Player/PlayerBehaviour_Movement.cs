using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour_Movement : MonoBehaviour
{
   [SerializeField] PlayerEntity _playerEntity;
   [SerializeField] TurnBasedEntity _turnBasedEntity;
   [SerializeField] PlayerInventory _inventory;
    [SerializeField] float _speed;
   [SerializeField] float _jumpPow;
   [SerializeField] float _jumpSpeed;
    public void GoToGrid(GridElement _nextGrid)
   {
        bool _jump=false;
        if (_playerEntity._currentGrid != null)
        {

            _playerEntity.ReleaseGrid(_playerEntity._currentGrid);

            float _distance = Vector2.Distance(_playerEntity._currentGrid.position, _nextGrid.position);
            if(_distance > 1)
            {
                _jump = true;
            }
        }
        // Entity Check Entity On Grid
        Entity entityOnGrid=null;
        if(_nextGrid._entity != null)

        { entityOnGrid = _nextGrid._entity;
        }

        _playerEntity.SetGrid(_nextGrid);

        //Movement (Animation)
        if(_jump)
        {
            Vector3 newPos = new Vector3(0, transform.localPosition.y, 0);
            transform.DOLocalJump(newPos, _jumpPow ,1,_jumpSpeed).SetEase(Ease.InOutQuad).
                OnComplete(() =>
                {
                    OnMoveCompleted(entityOnGrid);
                }
            );
        }
        else
        {
            Vector3 newPos = new Vector3(0, transform.localPosition.y, 0);
            transform.DOLocalMove(newPos, _speed).SetEase(Ease.InOutQuad).SetSpeedBased().
                OnComplete(()=> 
                {
                    OnMoveCompleted(entityOnGrid);
                }
            );
        }

    }
    void OnMoveCompleted(Entity entity)
    {
        if (entity != null)
        {
            entity.Collected(_inventory);
        }
        _inventory.UpdateScore();
        _turnBasedEntity.TurnFinished();
    }
}
