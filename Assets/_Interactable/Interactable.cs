using UnityEngine;

namespace RPG.Characters
{
    public class Interactable : MonoBehaviour
    {

        [SerializeField] public float radius = 3f;
        public Transform intteractionTransform;

        bool isFocus = false;
        bool hasInteracted = false;

        Transform player;

        public virtual void Interact()
        {
            // this method is meant to be overwritten
            Debug.Log("Interacting with " + transform.name);
        }

        void Update()
        {
            if (isFocus && !hasInteracted)
            {
                float distance = Vector3.Distance(player.position, intteractionTransform.position);
                if (distance <= radius)
                {
                    Interact();
                    hasInteracted = true;
                }
            }
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(intteractionTransform.position, radius);
        }
    }
}