using UnityEngine;
using System.Collections.Generic;

public class KeyManager : MonoBehaviour
{
    public enum Key
    {
        Red,
        Blue,
        Yellow,
        None
    }

    public Key CurrentKey;
    public GameObject BlankKeyPrefab;
    public List<GameObject> KeyGOList;

    void Start()
    {
        CurrentKey = Key.None;

        // Link Prefabs to Enums TODO: Think of cleverer way to do this.

        KeyGOList = new List<GameObject>();
        KeyGOList.Add(Resources.Load<GameObject>("Interactibles/Red"));
        KeyGOList.Add(Resources.Load<GameObject>("Interactibles/Blue"));
        KeyGOList.Add(Resources.Load<GameObject>("Interactibles/Yellow"));
    }

    public void ThrowKey(Vector3 currentPosition, Transform CameraTransform)
    {
        if (CurrentKey == Key.None)
            return;

        // Instantiate Key.
        // The spawn location should be in front of the player (the direction they are looking) and at head height.
        Vector3 KeySpawnLocation = currentPosition + Vector3.up + CameraTransform.forward.normalized * 1.5f;
        GameObject thrownKey = Instantiate(KeyGOList[(int)CurrentKey], KeySpawnLocation, Quaternion.identity);

        // Set up the thrown key's properties.
        thrownKey.GetComponentInChildren<KeyScript>().thisKey = CurrentKey;
        thrownKey.GetComponentInChildren<KeyScript>().IsPickupable = true;
        thrownKey.GetComponentInChildren<Animator>().enabled = false;

        // Shove the key a bit using physics;
        Rigidbody rb = thrownKey.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        Vector3 throwDirection = CameraTransform.forward * 400;
        rb.AddForce(throwDirection);
        rb.AddTorque(new Vector3(0f, 1f, 0f) * 100, ForceMode.Impulse);

        // Manage the data on this script.
        CurrentKey = Key.None;
    }
}
