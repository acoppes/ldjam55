using System.Collections.Generic;
using Components;
using Game.Triggers;
using Gemserk.Leopotam.Ecs;
using Gemserk.Triggers;

namespace Triggers
{
    public class StoneActivationTriggerAction : WorldTriggerAction
    {
        public bool activate;
        public TriggerTarget target;
        
        public override ITrigger.ExecutionResult Execute(object activator = null)
        {
            var targets = new List<Entity>();
            if (target.Get(targets, world, activator))
            {
                foreach (var target in targets)
                {
                    target.Add(new StoneActivateComponent()
                    {
                        activation = activate
                    });
                }
            }
            return ITrigger.ExecutionResult.Completed;
        }
    }
}