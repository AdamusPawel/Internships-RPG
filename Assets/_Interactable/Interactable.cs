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

        public void OnFocused(Transform playerTransform)
        {
            isFocus = true;
            player = playerTransform;
            hasInteracted = false;
        }

        public void OnDefcused()
        {
            isFocus = false;
            player = null;
            hasInteracted = false;
        }


        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(intteractionTransform.position, radius);
        }
    }
}