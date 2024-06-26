﻿using Components;
using Game.Components;
using Gemserk.Leopotam.Ecs;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Systems
{
    public class RuneStoneModelSystem : BaseSystem, IEcsRunSystem
    { 
        readonly EcsFilterInject<Inc<ModelComponent, RuneStoneComponent>, Exc<DisabledComponent>> filter = default;
        
        public void Run(EcsSystems systems)
        {
            foreach (var e in filter.Value)
            {
                ref var model = ref filter.Pools.Inc1.Get(e);
                ref var stone = ref filter.Pools.Inc2.Get(e);

                var runeRenderer = model.instance.transform.Find("Rune").GetComponent<SpriteRenderer>();
                runeRenderer.sprite = stone.runeSprites[stone.rune];

                var onParticles = model.instance.transform.Find("Stone_Particles").GetComponent<ParticleSystem>();

                if (!stone.active)
                {
                    stone.activeTime = 0;
                }
                else
                {
                    stone.activeTime += dt;
                }
                
                if (!onParticles.isPlaying && stone.active)
                {
                    onParticles.Play();
                } else if (onParticles.isPlaying && !stone.active)
                {
                    onParticles.Stop();
                } 
            }
        }
    }
}