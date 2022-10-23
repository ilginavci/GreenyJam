using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EntityType
{
    Player1,
    Player2,
    Player1Item,
    Player2Item,
    EarthquakePower,
}

public class Entity : ComponentBase
{
    public GridElement _currentGrid;
    public BoardBehaviour _boardBehaviour;
    public EntityType _type;

    protected override void OnEnable()
    {
        transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), .3f);
        transform.DOScale(new Vector3(1f, 1f, 1f), .3f).SetDelay(.3f);
    }
    public void DestroyWithAnimation()
    {
        transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), .3f);
        transform.DOScale(new Vector3(0f, 0f, 0f), .3f).SetDelay(.3f)
            .OnComplete(()=> 
        {
            SetEmptyGrid();
            Destroy(gameObject);
        });
    }

    public virtual void Shocked(EntityType _sourceType)
    {
        CollectableController.Instance.RemoveCollectable(this);
        switch (_type)
        {
            case EntityType.Player1Item:
                CheckScore(EntityType.Player1, _sourceType);
                SetEmptyGrid();
                Destroy(gameObject);
                break;
            case EntityType.Player2Item:
                CheckScore(EntityType.Player2, _sourceType);
                SetEmptyGrid();
                Destroy(gameObject);
                break;
            case EntityType.EarthquakePower:
                SetEmptyGrid();
                Destroy(gameObject);
                break;
            default:
                break;
        }
    }
    void SetEmptyGrid()
    {
        if(_currentGrid !=null)
        {
            _currentGrid._entity = null;
            _boardBehaviour._fullGrids.Remove(_currentGrid);
            _boardBehaviour._emptyGrids.Add(_currentGrid);
        }
    }
    void CheckScore(EntityType entityPlayer , EntityType source)
    {
        TurnBasedEntity source_TurnBasedEntity;
        PlayerInventory _playerInventory;
        if (source == EntityType.EarthquakePower)
        {
            source_TurnBasedEntity = TurnBasedController.Instance.GetPlayer(entityPlayer);
            _playerInventory = source_TurnBasedEntity._inventory;
            _playerInventory.AddScore(-10);
            return;
        }
        source_TurnBasedEntity = TurnBasedController.Instance.GetPlayer(source);
        _playerInventory = source_TurnBasedEntity._inventory;
        if (entityPlayer== source)
        {
            _playerInventory.AddScore(-10);
        }
        else
        {
            _playerInventory.AddScore(10);
        }
    }
    public virtual void Collected(PlayerInventory _inventory)
    {
        Vector3 _pos = _inventory.transform.position;
        transform.DOJump(_pos, 2, 1, .5f).OnComplete(() =>
        {
            Destroy(gameObject);
            _inventory.AddInventory(this);
            CollectableController.Instance.RemoveCollectable(this);
        }
        );
    }
    private void OnDestroy()
    {
        SetEmptyGrid();
    }
}
