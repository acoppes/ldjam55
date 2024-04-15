using System;
using System.Collections;
using System.Collections.Generic;
using Components;
using Game;
using Game.Components;
using Gemserk.Leopotam.Ecs;
using Gemserk.Triggers.Queries;
using Gemserk.Utilities;
using Gemserk.Utilities.Signals;
using UnityEngine;
using Object = UnityEngine.Object;

public class GameLogic : MonoBehaviour
{
    [Serializable]
    public class CreatureSequence
    {
        public string sequence;
        // public Object definitionOverride;
        public string anim;
    }
    
    public WorldReference worldReference;
    
    [ObjectType(typeof(IEntityDefinition), filterString = "Definition")]
    public Object definition;

    public Query runesQuery;
    public Query emptySlotsQuery;

    public SignalAsset onCreatureSpawned;

    public List<CreatureSequence> sequences;

    public ParticleSystem spawnPointA, spawnPointB;

    public float spawnTime1, spawnTime2, spawnTime3 ;

    private void Awake()
    {
        spawnPointA.Stop();
        spawnPointB.Stop();
    }

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
        StartCoroutine(SpawnCreatureRoutine());
    }

    public IEnumerator SpawnCreatureRoutine()
    {
        var world = worldReference.GetReference(gameObject);
        var emptySlots = world.GetEntities(emptySlotsQuery);

        if (emptySlots.Count == 0)
        {
            // CANT SPAWN MORE
            yield break;
        }
        
        var runeSequence = world.GetSingleton<RuneSequenceComponent>();
        Debug.Log("SUMMONING SEQUENCE: " + runeSequence.summonWord);

        var specificCreature = sequences.Find(s => s.sequence.Equals(runeSequence.summonWord, StringComparison.OrdinalIgnoreCase));
        var randomSlot = emptySlots.Random();

        spawnPointA.Play();
        spawnPointB.transform.position = randomSlot.Get<PositionComponent>().value;
        
        yield return new WaitForSeconds(spawnTime1);
        
        spawnPointB.Play();
        
        yield return new WaitForSeconds(spawnTime2);

        var creatureEntity = world.CreateEntity(definition);
        creatureEntity.Get<PositionComponent>().value = randomSlot.Get<PositionComponent>().value;

        randomSlot.Get<SlotComponent>().owner = creatureEntity;
        
        if (specificCreature != null)
        {
            // spawn specific creature...
            creatureEntity.Add(new StartingAnimationComponent()
            {
                startingAnimationType = StartingAnimationComponent.StartingAnimationType.Name,
                name = specificCreature.anim,
                loop = true,
            });
        }
        
        onCreatureSpawned.Signal(creatureEntity);
        
        yield return new WaitForSeconds(spawnTime3);
        
        spawnPointA.Stop();
        spawnPointB.Stop();
    }
}
