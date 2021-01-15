using System;
using UnityEngine;

namespace RPG.Core
{
    public class Health : MonoBehaviour
    {
        [SerializeField]
        private float health = 100;

        private bool isDead = false;
        public void TakeDamage(float damage)
        {
            health = Mathf.Max((health - damage), 0);
            Debug.Log(health);

            if(health ==0)
            {
                Die();
            }
        }

        private void Die()
        {
            if(isDead)
            {
                return;
            }

            isDead = true;
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        public bool IsDead()
        {
            return isDead;
        }
    }
}
