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

    public override void Interact(GameObject interacter)
    {
        KeyManager km = FindObjectOfType<KeyManager>();

        if (IsPickupable)
        {
            // Check if already holding a key, and if so throw the key before picking up
            if (km.CurrentKey != KeyManager.Key.None)
            {
                km.ThrowKey(interacter.transform.position, interacter.transform.Find("Camera").transform);
            }

            // Pick up the key;
            Destroy(transform.root.gameObject);
            km.CurrentKey = thisKey;
        }
    }    

    // Is only pickupable after throwing if on floor.
    private void OnCollisionEnter(Collision collision)
    {
        // TODO: Check if collision is floor.
        IsPickupable = true;
    }
}
