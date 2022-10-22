using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : Entity
{
    [SerializeField] private Vector2Int _firstPos;
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private string _movableLayer;
    List<GridElement> _highlightedGrids = new List<GridElement>();
    private bool _isTurn = false;
    public bool IsTurn{

        get{ return _isTurn; }

        set{
            _isTurn = false;
            GetTurn();
        }
        
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        var grid = _boardBehaviour.GetGrid(_firstPos);
        transform.position = new Vector3(_firstPos.x, transform.position.y, _firstPos.y);
        _currentGrid = grid;
        _currentGrid.SetBusy(this);

    }
    public void GetTurn()
    {
        FindMovableGrids();
        _inputManager.enabled = true;
        _inputManager.InputInitialize(OnChoose);
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
            else if(_grid._entity._type == EntityType.Player)
            {
                Vector2Int sub = _grid.position - _currentGrid.position;
                sub *= 2;
                Vector2Int newGridPos = _currentGrid.position + sub;
                TrySetMovable(newGridPos);
            }
        }
    }
    private void OnChoose(GridElement _choosedGrid)
    {
        foreach (var item in _highlightedGrids)
        {
            item.MakeHighlight(HighlightMode.Normal);
            item.gameObject.layer = LayerMask.NameToLayer("Default");
        }
        _highlightedGrids.Clear();
        _inputManager.enabled = false;
        //GetTurn();
    }
}
