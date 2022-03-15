using UnityEngine;

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

    void Start()
    {
        CurrentKey = Key.None;
    }

    public void ThrowKey(Vector3 currentPosition, Transform CameraTransform)
    {
        if (CurrentKey == Key.None)
            return;

        // Instantiate Key.
        // The spawn location should be in front of the player (the direction they are looking) and at head height.
        Vector3 KeySpawnLocation = currentPosition + Vector3.up + CameraTransform.forward.normalized * 1.5f;
        GameObject thrownKey = Instantiate(BlankKeyPrefab, KeySpawnLocation, Quaternion.identity);

        // Set up the thrown key's properties.
        thrownKey.GetComponent<KeyScript>().thisKey = CurrentKey;
        thrownKey.GetComponent<KeyScript>().IsPickupable = false;
        thrownKey.GetComponent<Renderer>().material.color = Color.red; // TODO: Change to work for all colours.
        thrownKey.GetComponent<Collider>().isTrigger = false;

        // Shove the key a bit using physics;
        Vector3 throwDirection = CameraTransform.forward * 200;
        thrownKey.GetComponent<Rigidbody>().AddForce(throwDirection);

        // Manage the data on this script.
        CurrentKey = Key.None;
    }
}
