using System;
using RPG.Combat;
using RPG.Movement;
using UnityEngine;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        private void Update()
        {
            if(InteractWithCombat())
            {
                return;
            }

            if(InterractWithMovement())
            {
                return;
            }
            
        }

        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            
            foreach(RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();

                if(!GetComponent<Fighter>().CanAttack(target))
                {
                    continue;
                }

                if(Input.GetMouseButtonDown(0))
                {
                
                    GetComponent<Fighter>().Attack(target);
                        
                }
                return true;
                
            }
            return false;
            
        }

        private bool InterractWithMovement()
        {
            RaycastHit hit;

            if (Physics.Raycast(GetMouseRay(), out hit))
            {
                if (Input.GetMouseButton(0))
                {
                    GetComponent<Mover>().StartMoveAction(hit.point);
                    //nav.SetDestination(hit.point);
                }
                return true;

            }

            return false;

        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}
