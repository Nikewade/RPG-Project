using RPG.Core;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Saving;
using RPG.Attributes;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction, ISaveable
    {
        [SerializeField] float maxSpeed = 6;

        NavMeshAgent nav;
        Health health;

        private void Awake()
        {
            nav = GetComponent<NavMeshAgent>();
            health = GetComponent<Health>();
        }

        // Update is called once per frame
        void Update()
        {
            nav.enabled = !health.IsDead();

            UpdateAnimator();
        }

        public void StartMoveAction(Vector3 destination, float speedFraction)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination, speedFraction);
        }

        public void MoveTo(Vector3 destination, float speedFraction)
        {
            nav.destination = destination;
            nav.speed = maxSpeed * Mathf.Clamp01(speedFraction); //between 0 and 1
            nav.isStopped = false;
        }

        public void Cancel()
        {
            nav.isStopped = true;
        }

        private void UpdateAnimator()
        {
            Vector3 velocity = nav.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity); // Converted from global to local
            float speed = localVelocity.z;
            GetComponent<Animator>().SetFloat("forwardSpeed", speed);
        }

        public object CaptureState()
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data["position"] = new SerializableVector3(transform.position);
            data["rotation"] = new SerializableVector3(transform.eulerAngles);
            return data;
        }

        public void RestoreState(object state)
        {
            Dictionary<string, object> data = (Dictionary<string, object>)state;
            GetComponent<NavMeshAgent>().enabled = false;
            transform.position = ((SerializableVector3)data["position"]).ToVector();
            transform.eulerAngles = ((SerializableVector3)data["rotation"]).ToVector();
            GetComponent<NavMeshAgent>().enabled = true;
        }
    }
}
