using UnityEngine;

namespace RPG.Combat
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

            if(health ==0 && !isDead)
            {
                isDead = true;
                GetComponent<Animator>().SetTrigger("die");
            }
        }

        public bool IsDead()
        {
            return isDead;
        }
    }
}
