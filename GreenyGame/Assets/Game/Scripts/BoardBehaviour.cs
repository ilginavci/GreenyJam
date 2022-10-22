using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardBehaviour : ComponentBase
{
    public Grid _block;
    [SerializeField] int _boardSize;
    [HideInInspector] public Grid[][] _board;

    //ForEntities
    public List<Grid> _emptyGrids;
    public List<Grid> _fullGrids;
    protected override void OnEnable()
    {
        base.OnEnable();
        _board = new Grid[_boardSize][];
        for (int i = 0; i < _board.Length; i++)
        {
            _board[i] = new Grid[_boardSize];
        }

        GenerateBoard();

    }
    private void GenerateBoard()
    {
        for (int y = 0; y < _board.Length; y++)
        {
            for (int x = 0; x < _board[y].Length; x++)
            {
                Vector2 _pos = new Vector2(y, x);
                Grid _instantiatedGrid= Instantiate(_block, transform);
                _instantiatedGrid.position = _pos;
                _instantiatedGrid.transform.localPosition = new Vector3(_pos.x, 0, _pos.y);
                _emptyGrids.Add(_instantiatedGrid);
                _instantiatedGrid._entity = null;
            }
        }
    }
}
