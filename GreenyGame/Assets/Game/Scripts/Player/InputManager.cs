using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : ComponentBase
{
    [SerializeField] private Camera _cam;
    [SerializeField] private string _clickableLayer;
    [SerializeField] private PlayerBehaviour_Movement _movement;
    private event Action<GridElement> _chooseAction;
    GridElement _onHoverGrid;
    public void InputInitialize(Action<GridElement> _onChoose)
    {
        _chooseAction = _onChoose;
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        _chooseAction?.Invoke(_onHoverGrid);

        if (_onHoverGrid !=null)
        {
            _onHoverGrid.SetDefaultHighlight();
        }
    }
    void Update()
    {
        GetGrid();
    }

    void GetGrid()
    {
        RaycastHit _hit;
        Vector3 _mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);
        Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out _hit, 100f, LayerMask.GetMask(_clickableLayer)))
        {
            if (_onHoverGrid == null)
            {
                _onHoverGrid = _hit.transform.gameObject.GetComponent<GridElement>();
                _onHoverGrid.MakeHighlight(HighlightMode.OnHover);
            }

            if (_onHoverGrid.transform != _hit.transform)
            {
                _onHoverGrid.SetDefaultHighlight();
                _onHoverGrid = _hit.transform.gameObject.GetComponent<GridElement>();
                _onHoverGrid.MakeHighlight(HighlightMode.OnHover);
            }

            if (Input.GetMouseButtonDown(0))
            {
                GridElement _clickedGrid = _onHoverGrid;
                ChooseGrid(_clickedGrid);
            }
        }
        else if (_onHoverGrid != null)
        {
            _onHoverGrid.SetDefaultHighlight();
            _onHoverGrid = null;
        }
    }
    private void ChooseGrid(GridElement _clickedGrid)
    {
        if (_clickedGrid != null)
        {
            _clickedGrid.SetHighlightMode(HighlightMode.Normal);
            _movement.GoToGrid(_clickedGrid);
            _chooseAction?.Invoke(_clickedGrid);
        }
    }
}
