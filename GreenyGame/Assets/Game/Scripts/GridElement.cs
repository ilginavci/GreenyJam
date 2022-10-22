using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HighlightMode
{
    Normal,
    Movable,
    OnHover,
}
public class GridElement : ComponentBase
{
    public Vector2Int position;
    public Entity _entity;
    public BoardBehaviour _board;
    [SerializeField] private MeshRenderer _gfx;
    [SerializeField] private Material _normalHighlight;
    [SerializeField] private Material _movableHighlight;
    [SerializeField] private Material _onHoverHighlight;
    private HighlightMode _highlightMode;
   public void SetHighlightMode(HighlightMode _mode)
    {
        _highlightMode = _mode;
        SetDefaultHighlight();
    }
    public void SetDefaultHighlight()
    {
        MakeHighlight(_highlightMode);
    }
    public void MakeHighlight(HighlightMode _mode)
    {
        if (_mode == HighlightMode.Normal)
        {
            _gfx.material = _normalHighlight;
        }
        else if (_mode == HighlightMode.Movable)
        {
            _gfx.material = _movableHighlight;
        }
        else if (_mode == HighlightMode.OnHover)
        {
            _gfx.material = _onHoverHighlight;
        }

    }

    public bool IsGridAvailable()
    {
        if (_entity == null) return true;

        if (_entity._type == EntityType.Player) return false;

        return true;
    }

    #region ForCollectables
    public void SetBusy(Entity _currentEntity= null)
    {
        if(_currentEntity != null)
        {
            _entity = _currentEntity;
        }

        if(_board._emptyGrids.Contains(this))
        {
            _board._emptyGrids.Remove(this);
        }
        _board._fullGrids.Add(this);
    }

    public void SetAvailable()
    {
        if (_board._fullGrids.Contains(this))
        {
            _board._fullGrids.Remove(this);
        }

        _board._emptyGrids.Add(this);

    }
    #endregion ForCollectables
}   
