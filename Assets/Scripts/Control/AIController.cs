using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField]
        private float chaseDistance = 5f;
        [SerializeField]
        private float suspecionTime = 3f;
        [SerializeField]
        private float waypointTolerance = 1f;
        [SerializeField]
        private PatrolPath patrolPath;
        [SerializeField]
        private float waypointDwellTime = 3f;

        private int currentWaypointIndex = 0;
        private float timeSinceLastSawPlayer = Mathf.Infinity;
        private float timeSinceArrivedAtWaypoint = Mathf.Infinity; 

        private Health health;
        private GameObject player;
        private Fighter fighter;


        private Vector3 guardPosition;

        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
            guardPosition = transform.position;
        }


        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }

        private void Update()
        {
            if (health.IsDead())
            {
                return;
            }

            if (InAttackRangeOfPlayer() && fighter.CanAttack(player))
            {

                AttackBehaviour();
            }

            else if (timeSinceLastSawPlayer < suspecionTime)
            {
                SuspecionBehaviour();
            }

            else
            {
                Patrolbehaviour();
            }

            UpdateTimers();
        }

        private void UpdateTimers()
        {
            timeSinceLastSawPlayer += Time.deltaTime;
            timeSinceArrivedAtWaypoint += Time.deltaTime;
        }

        private void Patrolbehaviour()
        {
            Vector3 nextPosition = guardPosition;

            if(patrolPath != null)
            {
               
                if(isAtWaypoint())
                {
                    timeSinceArrivedAtWaypoint = 0f;
                    CycleWaypoint();
                }
                nextPosition = GetCurrentWaypoint();
            }

            if(timeSinceArrivedAtWaypoint >waypointDwellTime)
            {
                GetComponent<Mover>().StartMoveAction(nextPosition);
            }
            
        }

        private Vector3 GetCurrentWaypoint()
        {
           
            return patrolPath.GetWayPoint(currentWaypointIndex);
        }

        private void CycleWaypoint()
        {
            currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
        }

        private bool isAtWaypoint()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
            return distanceToWaypoint < waypointTolerance;
        }

        private void SuspecionBehaviour()
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private void AttackBehaviour()
        {
            timeSinceLastSawPlayer = 0;
            fighter.Attack(player);
        }

        private bool InAttackRangeOfPlayer()
        {
            return Vector3.Distance(player.transform.position, transform.position) < chaseDistance;
        }

        
    }
}
