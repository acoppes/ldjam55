using Gemserk.Leopotam.Ecs;
using UnityEngine;

namespace Components
{
    public struct RuneStoneComponent : IEntityComponent
    {
        public int rune;
        public string runeKey;

        public bool wasActive;
        public bool active;
        
        public float activeTime;
        public Sprite[] runeSprites;
    }
    
    public struct RuneStoneActivateComponent : IEntityComponent
    {
        public bool activation;
    }
    
    public class RuneStoneComponentDefinition : ComponentDefinitionBase
    {
        public int rune;
        public string runeKey;

        public Sprite[] runeSprites;
        
        public override string GetComponentName()
        {
            return nameof(RuneStoneComponent);
        }

        public override void Apply(World world, Entity entity)
        {
            if (!world.HasComponent<RuneStoneComponent>(entity))
            {
                world.AddComponent(entity, new RuneStoneComponent()
                {
                    rune = rune,
                    runeKey = runeKey,
                    runeSprites = runeSprites
                });
            }
            else
            {
                ref var stone = ref entity.Get<RuneStoneComponent>();
                stone.rune = rune;
                stone.runeKey = runeKey;
            }
        }
    }
}