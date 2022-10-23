using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableController : Singleton<CollectableController>
{
    private List<Entity> player1Collectables = new List<Entity>();
    private List<Entity> player2Collectables = new List<Entity>();
    private Entity _earthquake;
    [SerializeField] int collectablePerPlayer = 0;
    [SerializeField] Spawner _spawner;

    private void OnEnable()
    {
        TurnBasedController.Instance.OnRoundStart += RoundStarted;
        TurnBasedController.Instance.OnRoundFinished += RoundFinished;
    }


    private void RoundStarted()
    {
        if (TurnBasedController.Instance.Round == 1)
        {
            MixCollectables();
        }
        if (TurnBasedController.Instance.Round % 10 == 0)
        {
            if (_earthquake == null)
            {
                _spawner.SpawnCollectable(EntityType.EarthquakePower, 1);
            }
        }
    }

    private void RoundFinished()
    {

    }

    public void RemoveCollectable(Entity _entity)
    {
        if(_entity._type == EntityType.Player1Item)
        {
            player1Collectables.Remove(_entity);
        }
        else if(_entity._type == EntityType.Player2Item)
        {
            player2Collectables.Remove(_entity);
        }
        else if (_entity._type == EntityType.EarthquakePower)
        {
            _earthquake=null;
        }
    }

    public void AddCollectable(Entity _entity)
    {
        if (_entity._type== EntityType.Player1)
        {
            player1Collectables.Add(_entity);
        }
        else if (_entity._type == EntityType.Player2)
        {
            player2Collectables.Add(_entity);
        }
        else if(_entity._type == EntityType.EarthquakePower)
        {
            _earthquake = _entity;
            TurnBasedController.Instance._earthquakeObject = _earthquake as Earthquake_Power;
            TurnBasedController.Instance._autoActivateRound = Random.Range(TurnBasedController.Instance.Round + 5 , TurnBasedController.Instance.Round + 10);

        }
    }

    public void MixCollectables()
    {
        ClearCollactables(player1Collectables);
        ClearCollactables(player2Collectables);
        _spawner.SpawnCollectable(EntityType.Player1Item, collectablePerPlayer);
        _spawner.SpawnCollectable(EntityType.Player2Item, collectablePerPlayer);
    }

    void ClearCollactables(List<Entity> _list)
    {
        foreach (var item in _list)
        {
           item.DestroyWithAnimation();
        }
        _list.Clear();
    }
}
