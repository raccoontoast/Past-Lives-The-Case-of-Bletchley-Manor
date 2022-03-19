using UnityEngine;

public class Door : Interactible
{
    public KeyManager.Key CorrespondingKey;

    public override void Interact(GameObject Interacter)
    {
        KeyManager km = Interacter.GetComponent<KeyManager>();

        if (km.CurrentKey == CorrespondingKey)
        {
            gameObject.SetActive(false); // TODO: Animation.
            km.CurrentKey = KeyManager.Key.None;
            km.KeyFPSViewGO.SetActive(false);
        }
    }
}
