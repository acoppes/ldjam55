using Components;
using Game.Components;
using Gemserk.Leopotam.Ecs;
using Gemserk.Utilities.Signals;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Systems
{
    public class RuneStoneActivationSystem : BaseSystem, IEcsRunSystem
    { 
        public SignalAsset onStoneActivated;
         
        readonly EcsFilterInject<Inc<StoneComponent, StoneActivateComponent>, Exc<DisabledComponent>> activateStonesFilter = default;
        
        public void Run(EcsSystems systems)
        {
            foreach (var e in activateStonesFilter.Value)
            {
                ref var stone = ref activateStonesFilter.Pools.Inc1.Get(e);
                ref var stoneActivation = ref activateStonesFilter.Pools.Inc2.Get(e);

                stone.wasActive = stone.active;
                stone.active = stoneActivation.activation;
                
                onStoneActivated.Signal(world.GetEntity(e));
                
                world.RemoveComponent<StoneActivateComponent>(e);
            }
        }
    }
}