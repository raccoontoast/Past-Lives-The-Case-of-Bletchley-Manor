using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    public void TryInteract(Transform cameraTransform)
    {
        // do a raycast
        // check if hit object is interactible
        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
        RaycastHit hitInfo;

        Physics.Raycast(ray, out hitInfo, 2f, 3);

        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.TryGetComponent(out Interactible interactible))
            {
                Debug.Log("Viewed object is interactible.");
                interactible.Interact();
            }
        }
    }
}
