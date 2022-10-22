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
    public Grid _currentGrid;
    public EntityType _type;
}
