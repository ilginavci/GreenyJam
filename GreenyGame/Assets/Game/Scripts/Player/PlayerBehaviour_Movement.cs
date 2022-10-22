using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour_Movement : MonoBehaviour
{
   [SerializeField] Entity _playerEntity;
   public void GoToGrid(GridElement _nextGrid, Action<GridElement> OnMoveCompleted)
   {
        if (_playerEntity._currentGrid != null)
        {
            ReleaseGrid(_playerEntity._currentGrid);
        }
        // Entity Check for points 

        SetGrid(_nextGrid);

        //Movement (Animation)
        Vector3 newPos = new Vector3(_playerEntity._currentGrid.position.x, transform.position.y, _playerEntity._currentGrid.position.y);
        transform.position = newPos;

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
