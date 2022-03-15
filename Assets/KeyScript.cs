using UnityEngine;

[RequireComponent(typeof(Collider))]
public class KeyScript : Interactible
{
    public KeyManager.Key thisKey;
    public bool IsPickupable = true;

    private Vector3 origPosition;
    private Quaternion origRotation;

    private void Awake()
    {
        origPosition = transform.position;
        origRotation = transform.rotation;
    }

    public override void Interact()
    {
        if (IsPickupable)
        {
            // Pick up the key;
            Destroy(gameObject);
            FindObjectOfType<KeyManager>().CurrentKey = thisKey;
        }
    }    

    // Is only pickupable after throwing if on floor.
    private void OnCollisionEnter(Collision collision)
    {
        // TODO: Check if collision is floor.
        IsPickupable = true;
    }
}
