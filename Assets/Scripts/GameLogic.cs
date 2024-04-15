using System.Collections;
using System.Collections.Generic;
using Components;
using Gemserk.Leopotam.Ecs;
using Gemserk.Triggers.Queries;
using Gemserk.Utilities;
using Gemserk.Utilities.Signals;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public WorldReference worldReference;
    
    [ObjectType(typeof(IEntityDefinition), filterString = "Definition")]
    public Object definition;

    public Query emptySlotsQuery;

    public SignalAsset onCreatureSpawned;
    
    public void SpawnCreature()
    {
        var world = worldReference.GetReference(gameObject);
        var emptySlots = world.GetEntities(emptySlotsQuery);

        var randomSlot = emptySlots.Random();

        var creatureEntity = world.CreateEntity(definition);
        creatureEntity.Get<PositionComponent>().value = randomSlot.Get<PositionComponent>().value;

        randomSlot.Get<SlotComponent>().owner = creatureEntity;
        
        onCreatureSpawned.Signal(creatureEntity);

        // get runes in order

        // detect creature

        // get random free slot

        // spawn creature there
    }
}
