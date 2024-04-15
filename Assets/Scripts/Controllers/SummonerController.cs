using Game;
using Game.Components;
using Gemserk.Leopotam.Ecs;
using Gemserk.Leopotam.Ecs.Components;
using Gemserk.Leopotam.Ecs.Controllers;
using Gemserk.Leopotam.Ecs.Events;

namespace Controllers
{
    public class SummonerController : ControllerBase, IUpdate, IInit, IStateChanged
    {
        public void OnInit(World world, Entity entity)
        {
           
        }
        
        public void OnUpdate(World world, Entity entity, float dt)
        {
            var input = entity.Get<InputComponent>();
            ref var movement = ref entity.Get<MovementComponent>();
            ref var lookingDirection = ref entity.Get<LookingDirection>();

            ref var animations = ref entity.Get<AnimationComponent>();

            movement.movingDirection = input.direction3d();

            if (movement.movingDirection.sqrMagnitude > 0.1f)
            {
                lookingDirection.value = movement.movingDirection.normalized;
                
                if (!animations.IsPlaying("Walk-0"))
                {
                    animations.Play("Walk-0");
                }
            }
            else
            {
                if (!animations.IsPlaying("Idle-0"))
                {
                    animations.Play("Idle-0");
                }
            }
        }


        public void OnEnterState(World world, Entity entity)
        {
           
        }

        public void OnExitState(World world, Entity entity)
        {
          
        }
    }
}