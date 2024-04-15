using Components;
using Game.Components;
using Gemserk.Leopotam.Ecs;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Systems
{
     public class StonesRuneSystem : BaseSystem, IEcsRunSystem
    {
        readonly EcsFilterInject<Inc<ModelComponent, StoneComponent>, Exc<DisabledComponent>> filter = default;
        
        public void Run(EcsSystems systems)
        {
            foreach (var e in filter.Value)
            {
                ref var model = ref filter.Pools.Inc1.Get(e);
                ref var stone = ref filter.Pools.Inc2.Get(e);

                var runeRenderer = model.instance.transform.Find("Rune").GetComponent<SpriteRenderer>();
                runeRenderer.sprite = stone.runeSprites[stone.rune];
            }
        }
    }
}