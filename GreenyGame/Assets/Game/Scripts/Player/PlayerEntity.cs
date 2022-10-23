using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : Entity
{
    [SerializeField] private Vector2Int _firstPos;
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private string _movableLayer;
    [SerializeField] private TurnBasedEntity _turnBasedEntity;
    List<GridElement> _highlightedGrids = new List<GridElement>();

    protected override void OnEnable()
    {
        base.OnEnable();
        var grid = _boardBehaviour.GetGrid(_firstPos);
        transform.localPosition = new Vector3(_firstPos.x, transform.localPosition.y, _firstPos.y);

        SetGrid(grid);
        

    }
    public void GetTurn()
    {
        FindMovableGrids();
        _inputManager.enabled = true;
    }
    private void FindMovableGrids()
    {
        if (_currentGrid == null) return;
        Vector2Int pos = _currentGrid.position;
        Vector2Int tempPos = pos;
        tempPos.x--;
        TrySetMovable(tempPos);
        
        tempPos = pos;
        tempPos.x++;
        
        TrySetMovable(tempPos);

        tempPos = pos;
        tempPos.y--;

        TrySetMovable(tempPos);

        tempPos = pos;
        tempPos.y++;

        TrySetMovable(tempPos);
    }
    private void TrySetMovable(Vector2Int _pos)
    {
        GridElement _grid = _boardBehaviour.GetGrid(_pos);
        if (_grid != null)
        {
            if (_grid.IsGridAvailable())
            {
                _highlightedGrids.Add(_grid);
                _grid.SetHighlightMode(HighlightMode.Movable);
                _grid.gameObject.layer = LayerMask.NameToLayer(_movableLayer);
            }
            else if(_grid._entity._type == EntityType.Player1 || _grid._entity._type == EntityType.Player2)
            {
                Vector2Int sub = _grid.position - _currentGrid.position;
                sub *= 2;
                Vector2Int newGridPos = _currentGrid.position + sub;
                TrySetMovable(newGridPos);
            }
        }
    }
    public void StopMove()
    {
        _inputManager.enabled = false;
        foreach (var item in _highlightedGrids)
        {
            item.MakeHighlight(HighlightMode.Normal);
            item.gameObject.layer = LayerMask.NameToLayer("Default");
        }
        _highlightedGrids.Clear();
    }
    public void SetGrid(GridElement _grid)
    {
        _currentGrid = _grid;
        _currentGrid._entity = this;
        transform.SetParent(_currentGrid.transform);

        _grid.SetBusy();
    }

    public void ReleaseGrid(GridElement _grid)
    {
        _grid._entity = null;
        _grid.SetAvailable();
        transform.SetParent(null);
    }
    public override void Shocked(EntityType _srcType)
    {
        transform.DOLocalJump(transform.localPosition, 1, 1, 1);
        _turnBasedEntity.FreezeEntity();
    }
}
