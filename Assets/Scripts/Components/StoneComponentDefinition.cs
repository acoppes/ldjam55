using Gemserk.Leopotam.Ecs;
using UnityEngine;

namespace Components
{
    public struct StoneComponent : IEntityComponent
    {
        public int rune;

        public bool wasActive;
        public bool active;
        
        public float activeTime;
        public Sprite[] runeSprites;
    }
    
    public struct StoneActivateComponent : IEntityComponent
    {
        public bool activation;
    }
    
    public class StoneComponentDefinition : ComponentDefinitionBase
    {
        public int rune;

        public Sprite[] runeSprites;
        
        public override string GetComponentName()
        {
            return nameof(StoneComponent);
        }

        public override void Apply(World world, Entity entity)
        {
            if (!world.HasComponent<StoneComponent>(entity))
            {
                world.AddComponent(entity, new StoneComponent()
                {
                    rune = rune,
                    runeSprites = runeSprites
                });
            }
            else
            {
                ref var stone = ref entity.Get<StoneComponent>();
                stone.rune = rune;
            }
        }
    }
}