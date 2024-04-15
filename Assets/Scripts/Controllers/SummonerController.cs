using Game.Components;
using Game.Controllers;
using Gemserk.Leopotam.Ecs;
using Gemserk.Leopotam.Ecs.Controllers;
using Gemserk.Leopotam.Ecs.Events;
using Gemserk.Utilities;
using UnityEngine;

namespace Controllers
{
    public class SummonerController : ControllerBase, IUpdate
    {
        [ObjectType(typeof(IEntityDefinition), filterString = "Definition")]
        public Object stompImpulseDefinition; 
        
        public void OnUpdate(World world, Entity entity, float dt)
        {
            var input = entity.Get<InputComponent>();
            ref var movement = ref entity.Get<MovementComponent>();
            ref var lookingDirection = ref entity.Get<LookingDirection>();

            ref var animations = ref entity.Get<AnimationComponent>();

            movement.movingDirection = Vector3.zero;
            
            if (animations.IsPlaying("StompEnd"))
            {
                // do something here
                
                // 

                if (!animations.isCompleted)
                {
                    return;
                }
            }
            
            if (animations.IsPlaying("StompCharge"))
            {
                if (!input.button1().isPressed)
                {
                    animations.Play("StompEnd", 1);
                    world.CreateEntity(stompImpulseDefinition);
                    return;
                }
                return;
            }
            
            if (animations.IsPlaying("StompStart"))
            {
                if (animations.isCompleted)
                {
                    animations.Play("StompCharge");
                }
                return;
            }

            if (input.button1().isPressed)
            {
                animations.Play("StompStart", 1);
                return;
            }
            
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
    }
}