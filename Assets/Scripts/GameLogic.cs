using Components;
using Game;
using Gemserk.Leopotam.Ecs;
using Gemserk.Triggers.Queries;
using Gemserk.Utilities;
using Gemserk.Utilities.Signals;
using UnityEngine;
using Object = UnityEngine.Object;

public class GameLogic : MonoBehaviour
{
    public WorldReference worldReference;
    
    [ObjectType(typeof(IEntityDefinition), filterString = "Definition")]
    public Object definition;

    public Query runesQuery;
    public Query emptySlotsQuery;

    public SignalAsset onCreatureSpawned;

    public void Update()
    {
        var world = worldReference.GetReference(gameObject);

        ref var runeSequence = ref world.GetSingleton<RuneSequenceComponent>();
        
        runeSequence.orderedRunes = world.GetEntities(runesQuery);
        runeSequence.orderedRunes.Sort((e1, e2) =>
        {
            var t1 = e1.Get<RuneStoneComponent>().activeTime;
            var t2 = e2.Get<RuneStoneComponent>().activeTime;

            if (t1 < t2)
                return 1;
                
            if (t1 > t2)
                return -1;

            return 0;
        });

        runeSequence.summonWord = "";
        foreach (var runeEntity in runeSequence.orderedRunes)
        {
            var runeStone = runeEntity.Get<RuneStoneComponent>();
            if (runeStone.active)
            {
                runeSequence.summonWord += runeStone.runeKey;
            }
        }

    }

    public void SpawnCreature()
    {
        var world = worldReference.GetReference(gameObject);
        var emptySlots = world.GetEntities(emptySlotsQuery);

        var randomSlot = emptySlots.Random();

        var creatureEntity = world.CreateEntity(definition);
        creatureEntity.Get<PositionComponent>().value = randomSlot.Get<PositionComponent>().value;

        randomSlot.Get<SlotComponent>().owner = creatureEntity;
        
        onCreatureSpawned.Signal(creatureEntity);

        ref var runeSequence = ref world.GetSingleton<RuneSequenceComponent>();
        Debug.Log("SUMMONING SEQUENCE: " + runeSequence.summonWord);
        
        // get runes in order

        // detect creature

        // get random free slot

        // spawn creature there
    }
}
