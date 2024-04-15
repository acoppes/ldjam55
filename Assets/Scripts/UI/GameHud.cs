using System.Collections.Generic;
using Components;
using Gemserk.Leopotam.Ecs;
using Gemserk.Triggers.Queries;
using UnityEngine;

namespace UI
{
    public class GameHud : MonoBehaviour
    {
        public WorldReference worldReference;
        public Query runesQuery;
        public List<RuneUI> runes;

        public bool inverted;

        private void LateUpdate()
        {
            var world = worldReference.GetReference(gameObject);
            var runeEntities = world.GetEntities(runesQuery);
            
            runeEntities.Sort((e1, e2) =>
            {
                var t1 = e1.Get<RuneStoneComponent>().activeTime;
                var t2 = e2.Get<RuneStoneComponent>().activeTime;

                if (t1 < t2)
                    return inverted ? -1 : 1;
                
                if (t1 > t2)
                    return inverted ? 1 : -1;

                return 0;
            });

            for (var i = 0; i < runeEntities.Count; i++)
            {
                var e = runeEntities[i];
                var stoneComponent = e.Get<RuneStoneComponent>();
                runes[i].activeRune = stoneComponent.rune;
                runes[i].active = stoneComponent.active;
            }
        }
    }
}