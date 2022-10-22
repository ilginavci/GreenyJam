using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EntityType
{
    Player1,
    Player2,
    Player1Item,
    Player2Item,
    Bomb,
}

public class Entity : ComponentBase
{
    public GridElement _currentGrid;
    public BoardBehaviour _boardBehaviour;
    public EntityType _type;

    public virtual void Shocked(EntityType _sourceType)
    {

    }
}
