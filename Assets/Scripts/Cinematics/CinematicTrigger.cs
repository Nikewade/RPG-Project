using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using RPG.Saving;

namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour, ISaveable
    {
        bool isTriggered = false;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player") || isTriggered) return;

            GetComponent<PlayableDirector>().Play();
            isTriggered = true;
        }

        public object CaptureState()
        {
            return isTriggered;
        }

        public void RestoreState(object state)
        {
            isTriggered = (bool)state;
        }
    }
}
