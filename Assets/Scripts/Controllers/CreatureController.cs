using Game.Components;
using Gemserk.Leopotam.Ecs;
using Gemserk.Leopotam.Ecs.Controllers;
using Gemserk.Leopotam.Ecs.Events;

namespace Controllers
{
    public class CreatureController : ControllerBase, IUpdate
    {
        public void OnUpdate(World world, Entity entity, float dt)
        {
            // var input = entity.Get<InputComponent>();
            
            ref var movement = ref entity.Get<MovementComponent>();
            ref var lookingDirection = ref entity.Get<LookingDirection>();

            // ref var animations = ref entity.Get<AnimationComponent>();

            // movement.movingDirection = input.direction3d();
            
            if (movement.movingDirection.sqrMagnitude > 0.1f)
            {
                lookingDirection.value = movement.movingDirection.normalized;
                
                // if (!animations.IsPlaying("Walk"))
                // {
                //     animations.Play("Walk");
                // }
            }
            else
            {
                // if (!animations.IsPlaying("Idle"))
                // {
                //     animations.Play("Idle");
                // }
            }
        }
    }
}