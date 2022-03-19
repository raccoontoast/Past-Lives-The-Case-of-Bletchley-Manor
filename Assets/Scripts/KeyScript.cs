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
        KeyManager km = interacter.GetComponent<KeyManager>();

        if (IsPickupable)
        {
            // Check if already holding a key, and if so throw the key before picking up
            if (km.CurrentKey != KeyManager.Key.None)
            {
                km.ThrowKey(interacter.transform.position, interacter.transform.Find("Camera").transform);
            }

            // Pick up the key.
            km.KeyFPSViewGO.SetActive(true);
            km.KeyFPSViewGO.GetComponentInChildren<Renderer>().material.color = GetComponent<Renderer>().material.color;
            foreach (var light in km.KeyFPSViewGO.GetComponentsInChildren<Light>())
            {
                light.color = GetComponent<Renderer>().material.color;
            }
            
            Destroy(transform.root.gameObject);
            km.CurrentKey = thisKey;

            // Audio
            //AudioManager.Instance.KeyPickupEvent.Post(AudioManager.Instance.gameObject);
        }
    }    

    // Is only pickupable after throwing if on floor.
    private void OnCollisionEnter(Collision collision)
    {
        // TODO: Check if collision is floor.
        IsPickupable = true;
    }
}
