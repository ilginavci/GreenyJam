using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GridContainer
{
    public GridElement[] columns;

    public GridContainer(int size)
    {
        columns = new GridElement[size];
    }
}
public class BoardBehaviour : ComponentBase
{
    public GridElement[] _blocks;
    [SerializeField] int _boardSize;
    public GridContainer[] _board;

    //ForEntities
    public List<GridElement> _emptyGrids;
    public List<GridElement> _fullGrids;
    public bool _createMap;
    public bool _clearMap;
    public bool _mapCreated = false;
#if UNITY_EDITOR
    private void OnValidate()
    {
        if (_createMap)
        {
            _createMap = false;
            ClearBoard();

            _board = new GridContainer[_boardSize];
            for (int i = 0; i < _board.Length; i++)
            {
                _board[i] = new GridContainer(_boardSize);
            }
            GenerateBoard();
        }
        else if (_clearMap)
        {
            ClearBoard();
        }
    }
    private void ClearBoard()
    {
        if (_mapCreated)
        {
            for (int x = 0; x < _board.Length; x++)
            {
                for (int y = 0; y < _board[x].columns.Length; y++)
                {
                    Vector2Int _pos = new Vector2Int(x, y);
                    StartCoroutine(Destroy(_board[_pos.x].columns[y].gameObject));
                }
            }
            _emptyGrids.Clear();
            _fullGrids.Clear();
            _mapCreated = false;
            _clearMap = false;
        }
    }
    IEnumerator Destroy(GameObject go)
    {
        yield return new WaitForEndOfFrame();
        DestroyImmediate(go);
    }
    private void GenerateBoard()
    {
        for (int x = 0; x < _board.Length; x++)
        {
            for (int y = 0; y < _board[x].columns.Length; y++)
            {
                Vector2Int _pos = new Vector2Int(x, y);
                GridElement _instantiatedGrid = Instantiate(_blocks[Random.Range(0, _blocks.Length)], transform);
                
                _instantiatedGrid.position = _pos;
                _instantiatedGrid.transform.localPosition = new Vector3(_pos.x, 0, _pos.y);

                Vector3 rotation = Vector3.zero;
                rotation.y = 90 * Random.Range(0, 4);
                _instantiatedGrid.transform.localRotation = Quaternion.Euler(rotation);

                _board[_pos.x].columns[_pos.y] = _instantiatedGrid;
                _emptyGrids.Add(_instantiatedGrid);
                _instantiatedGrid._entity = null;
                _instantiatedGrid._board = this;
            }
        }
        _mapCreated = true;
    }
#endif
    protected override void OnEnable()
    {
        base.OnEnable();


    }
    public GridElement GetGrid(Vector2Int _pos)
    {
        if ((_pos.x >= 0 && _pos.x < _boardSize) && (_pos.y >= 0 && _pos.y < _boardSize))
        {
            return _board[_pos.x].columns[_pos.y];
        }
        return null;
    }

}
