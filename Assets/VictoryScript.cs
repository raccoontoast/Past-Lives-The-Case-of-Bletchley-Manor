using UnityEngine;

public class VictoryScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("You win.");
        throw new System.NotImplementedException("Implement winning stuff.");
    }
}
