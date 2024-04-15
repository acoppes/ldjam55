using Gemserk.Leopotam.Ecs;
using UnityEngine;

namespace Components
{
    public struct SlotComponent : IEntityComponent
    {
        public Entity owner;
    }
    
    public class SlotComponentDefinition : ComponentDefinitionBase
    {
        public override string GetComponentName()
        {
            return nameof(SlotComponent);
        }

        public override void Apply(World world, Entity entity)
        {
            world.AddComponent(entity, new SlotComponent()
            {
                owner = Entity.NullEntity
            });
        }
    }
}