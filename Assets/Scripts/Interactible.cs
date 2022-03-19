using UnityEngine;

public abstract class Interactible : MonoBehaviour
{
    public bool ResetOnLifeSwap;
    public abstract void Interact(GameObject Interacter);
}
