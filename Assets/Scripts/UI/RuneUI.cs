using UnityEngine;

namespace UI
{
    public class RuneUI : MonoBehaviour
    {
        public GameObject[] runes;

        public bool active;
        public int activeRune;

        public void LateUpdate()
        {
            for (var i = 0; i < runes.Length; i++)
            {
                runes[i].SetActive(active && activeRune == i);
            }
        }
    }
}
