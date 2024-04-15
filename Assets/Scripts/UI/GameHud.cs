using System.Collections.Generic;
using Components;
using Game;
using Gemserk.Leopotam.Ecs;
using UnityEngine;

namespace UI
{
    public class GameHud : MonoBehaviour
    {
        public WorldReference worldReference;
        public List<RuneUI> runes;

        public bool inverted;

        private void LateUpdate()
        {
            var world = worldReference.GetReference(gameObject);
            var runeSequence = world.GetSingleton<RuneSequenceComponent>();

            if (runeSequence.orderedRunes.Count == 0)
            {
                return;
            }

            for (var i = 0; i < runeSequence.orderedRunes.Count; i++)
            {
                var e = runeSequence.orderedRunes[i];
                var stoneComponent = e.Get<RuneStoneComponent>();

                var runeIndex = inverted ? runes.Count - i - 1: i;
                
                runes[runeIndex].activeRune = stoneComponent.rune;
                runes[runeIndex].active = stoneComponent.active;
            }
        }
    }
}