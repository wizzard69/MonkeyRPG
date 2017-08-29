using UnityEngine;

public class Interactable : MonoBehaviour {

    public float radius = 3f;
    public Transform interactableTransform;

    bool isFocus = false;
    Transform player;
    bool hasInteracted = false;

    public virtual void Interact()
    {
        print("INTERACT!! with " + name);
    }

    private void Update()
    {
        if (isFocus && !hasInteracted)
        {
            float distance = Vector3.Distance(player.position, interactableTransform.position);

            if (distance <= radius)
            {
                Interact();
                hasInteracted = true;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactableTransform.position,radius);
    }

    public void OnFocused(Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;
        hasInteracted = false;
    }

    public void OnDefocused()
    {
        isFocus = false;
        player = null;
        hasInteracted = false;
    }
}
