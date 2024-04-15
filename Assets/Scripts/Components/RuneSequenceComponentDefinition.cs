using System.Collections.Generic;
using Gemserk.Leopotam.Ecs;

namespace Components
{
    public struct RuneSequenceComponent : IEntityComponent
    {
        public List<Entity> orderedRunes;
        public string summonWord;
    }
    
    public class RuneSequenceComponentDefinition : ComponentDefinitionBase
    {
        public override string GetComponentName()
        {
            return nameof(RuneSequenceComponent);
        }

        public override void Apply(World world, Entity entity)
        {
            world.AddComponent(entity, new RuneSequenceComponent()
            {
                orderedRunes = new List<Entity>()
            });
        }
    }
}