using Game;
using Game.Components;
using Gemserk.Leopotam.Ecs;
using Gemserk.Leopotam.Ecs.Components;
using Gemserk.Leopotam.Ecs.Controllers;
using Gemserk.Leopotam.Ecs.Events;
using Gemserk.Utilities;
using MyBox;
using UnityEngine;

namespace Controllers
{
    public class AcolyteController : ControllerBase, IUpdate, IInit, IStateChanged
    {
        public MinMaxFloat randomStompTime;
        public MinMaxFloat randomPrepareTime;

        [ObjectType(typeof(IEntityDefinition), filterString = "Definition")]
        public Object stompImpulseDefinition; 

        public void OnInit(World world, Entity entity)
        {
            ref var states = ref entity.Get<StatesComponent>();
            states.EnterState("NextStomp", randomStompTime.RandomInRange());
            
            ref var animations = ref entity.Get<AnimationComponent>();
            var lookingDirection = entity.Get<LookingDirection>();
            
            var idleAnimation = animations.animationsAsset.GetDirectionalAnimation("Idle", lookingDirection.value);
            animations.Play(idleAnimation);
        }
        
        public void OnUpdate(World world, Entity entity, float dt)
        {
            ref var states = ref entity.Get<StatesComponent>();
            ref var animations = ref entity.Get<AnimationComponent>();

            var lookingDirection = entity.Get<LookingDirection>();

            var idleAnimation = animations.animationsAsset.GetDirectionalAnimation("Idle", lookingDirection.value);
            // var prepareAnimation = animations.animationsAsset.GetDirectionalAnimation("Prepare", lookingDirection.value);
            var stompAnimation = animations.animationsAsset.GetDirectionalAnimation("Stomp", lookingDirection.value);

            if (animations.IsPlaying(stompAnimation) && animations.isCompleted)
            {
                // play stomp
                animations.Play(idleAnimation);
                states.EnterState("NextStomp", randomStompTime.RandomInRange());
                return;
            }
            
            // if (animations.IsPlaying(prepareAnimation) && animations.isCompleted)
            // {
            //     // play stomp
            //     animations.Play(stompAnimation, 1);
            //     return;
            // }
        }


        public void OnEnterState(World world, Entity entity)
        {
           
        }

        public void OnExitState(World world, Entity entity)
        {
            ref var states = ref entity.Get<StatesComponent>();
            var lookingDirection = entity.Get<LookingDirection>();
            ref var animations = ref entity.Get<AnimationComponent>();
        
            if (states.HasExitState("NextStomp"))
            {
                var prepareAnimation = animations.animationsAsset.GetDirectionalAnimation("Prepare", lookingDirection.value);
                animations.Play(prepareAnimation, 1);
                
                states.EnterState("Preparing", randomPrepareTime.RandomInRange());
            }
            
            if (states.HasExitState("Preparing"))
            {
                var stompAnimation = animations.animationsAsset.GetDirectionalAnimation("Stomp", lookingDirection.value);
                animations.Play(stompAnimation, 1);

                world.CreateEntity(stompImpulseDefinition);
            }
        }
    }
}
