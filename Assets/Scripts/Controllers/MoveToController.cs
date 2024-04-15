using Game.Components;
using Game.Components.Abilities;
using Game.Controllers;
using Gemserk.Leopotam.Ecs;
using Gemserk.Leopotam.Ecs.Controllers;
using Gemserk.Leopotam.Ecs.Events;
using UnityEngine;

namespace Controllers
{
    public class MoveToController : ControllerBase, IUpdate, IActiveController
    {
        public void OnUpdate(World world, Entity entity, float dt)
        {
            ref var movement = ref entity.Get<MovementComponent>();
            ref var abilities = ref entity.Get<AbilitiesComponent>();
            var moveToAbility = abilities.GetAbility("MoveTo");

            ref var activeController = ref entity.Get<ActiveControllerComponent>();

            if (activeController.IsControlled(this))
            {
                
                if (moveToAbility.abilityTargets.Count == 0)
                {
                    movement.movingDirection = Vector3.zero;
                    moveToAbility.Stop(Ability.StopType.Interrupted);
                    activeController.ReleaseControl(this);
                    return;
                }

                var destination = moveToAbility.abilityTargets[0].targetPosition;
                var position = entity.Get<PositionComponent>().value;

                if (Vector3.Distance(position, destination) < 0.1f)
                {
                    movement.movingDirection = Vector3.zero;
                    moveToAbility.Stop(Ability.StopType.Interrupted);
                    activeController.ReleaseControl(this);
                    return;
                }

                movement.movingDirection = (destination - position).normalized;
            }
            else
            {
                if (moveToAbility.pendingExecution)
                {
                    moveToAbility.pendingExecution = false;
                    moveToAbility.Start();
                    activeController.TakeControl(entity, this);
                    return;
                }
            }
        }

        public bool CanBeInterrupted(Entity entity, IActiveController activeController)
        {
            return true;
        }

        public void OnInterrupt(Entity entity, IActiveController activeController)
        {
            ref var input = ref entity.Get<InputComponent>();
            ref var abilities = ref entity.Get<AbilitiesComponent>();
            var moveToAbility = abilities.GetAbility("MoveTo");
            moveToAbility.Stop(Ability.StopType.Interrupted);
            input.direction().vector2 = Vector2.zero;
        }
    }
}