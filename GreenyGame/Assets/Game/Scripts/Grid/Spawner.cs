using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private BoardBehaviour _board;
    [SerializeField] private Earthquake_Power _earthquakePrefab;
    [SerializeField] private Entity _player1Collectable;
    [SerializeField] private Entity _player2Collectable;
    [SerializeField] private int _collectableNumber;
    private List<Entity> _player1CollectableList = new List<Entity>();
    private List<Entity> _player2CollectableList = new List<Entity>();

    public void SpawnCollectable(EntityType _entityType, int number)
    {
        Entity _entity = null;
        switch (_entityType)
        {
            case EntityType.Player1Item:
                _entity = _player1Collectable;
                break;
            case EntityType.Player2Item:
                _entity = _player2Collectable;
                break;
            case EntityType.EarthquakePower:
                _entity = _earthquakePrefab;
                break;
            default:
                break;
        }

        for (int i = 0; i < number; i++)
        {
            GridElement emptyGrid = _board.GetRandomEmptyGrid();
            if (emptyGrid == null)
            {
                return;
            }
            _board._emptyGrids.Remove(emptyGrid);
            _board._fullGrids.Add(emptyGrid);
            CollectableController.Instance.AddCollectable(_entity);

            Entity _collectable = Instantiate(_entity, emptyGrid.transform);
            _collectable._boardBehaviour = _board;
            Vector3 pos = Vector3.zero;
            pos.y = 1;
            _collectable.transform.localPosition = pos;
            emptyGrid._entity = _collectable;
            _collectable._currentGrid = emptyGrid;
        }
    }
}
