using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EntityType
{
    Player,
    Bomb,
    CollectableItem,
}

public class Entity : ComponentBase
{
    public GridElement _currentGrid;
    public BoardBehaviour _boardBehaviour;
    public EntityType _type;
}
