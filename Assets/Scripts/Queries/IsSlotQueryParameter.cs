using Components;
using Gemserk.Leopotam.Ecs;
using Gemserk.Triggers.Queries;

namespace Queries
{
    public class IsSlotQueryParameter : HasComponentQueryParameter<SlotComponent>
    {
        public bool isEmpty;

        public override bool MatchQuery(Entity entity)
        {
            if (base.MatchQuery(entity))
            {
                var slot = entity.Get<SlotComponent>();
                return slot.isEmpty == isEmpty;
            }
            return false;
        }
    }
}