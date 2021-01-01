using RPG.Combat;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour
    {
        private NavMeshAgent nav;
        private Animator animator;

        private void Start()
        {
            nav = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
        }

        void Update()
        {
            UpdateAnimator();
        }

        private void UpdateAnimator()
        {
            Vector3 velocity = nav.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;

            animator.SetFloat("forwardSpeed", speed);
        }

        public void StartMoveAction(Vector3 destination)
        {

            GetComponent<Fighter>().CancelAttack();
            MoveTo(destination);
        }

        public void MoveTo(Vector3 destination)
        {
            nav.isStopped = false;
            nav.destination = destination;
            
        }

        public void Stop()
        {
            nav.isStopped = true;
        }
    }
}
