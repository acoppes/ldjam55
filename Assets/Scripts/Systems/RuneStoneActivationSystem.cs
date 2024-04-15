using Components;
using Gemserk.Leopotam.Ecs;
using Gemserk.Utilities.Signals;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Systems
{
    public class RuneStoneActivationSystem : BaseSystem, IEcsRunSystem
    { 
        public SignalAsset onStoneActivated;
         
        readonly EcsFilterInject<Inc<RuneStoneComponent, RuneStoneActivateComponent>, Exc<DisabledComponent>> activateStonesFilter = default;
        
        public void Run(EcsSystems systems)
        {
            foreach (var e in activateStonesFilter.Value)
            {
                ref var stone = ref activateStonesFilter.Pools.Inc1.Get(e);
                ref var stoneActivation = ref activateStonesFilter.Pools.Inc2.Get(e);

                stone.wasActive = stone.active;
                stone.active = stoneActivation.activation;
                
                if (!stone.wasActive && stone.active)
                {
                    onStoneActivated.Signal(world.GetEntity(e));
                } else if (stone.wasActive && !stone.active)
                {
                    onStoneActivated.Signal(world.GetEntity(e));
                }
                
                world.RemoveComponent<RuneStoneActivateComponent>(e);
            }
        }
    }
}