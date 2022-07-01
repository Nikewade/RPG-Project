using RPG.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction
    {
        NavMeshAgent nav;
        Health health;

        private void Start()
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

        public void StartMoveAction(Vector3 destination)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination);
        }

        public void MoveTo(Vector3 destination)
        {
            nav.isStopped = false;
            nav.destination = destination;
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
    }
}
