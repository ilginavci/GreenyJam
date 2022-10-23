using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earthquake_Power : Entity
{
    public override void Shocked(EntityType _sourceType)
    {
        base.Shocked(_sourceType);
        Destroy(gameObject);
    }
    public override void Collected(PlayerInventory _inventory)
    {
        base.Collected(_inventory);
    }
}
