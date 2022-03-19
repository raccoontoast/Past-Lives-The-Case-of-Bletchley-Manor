using UnityEngine;

public class Floor : MonoBehaviour
{
    public string Level;
    public GameObject SpawnPosition;

    private void Awake()
    {
        // Turns 'is convex' on for all floors and stairs since key just goes through floor sometimes.
        foreach (var mc in GetComponentsInChildren<MeshCollider>())
        {
            if (mc.gameObject.name.Contains("Cube") || mc.gameObject.name.Contains("Stairs"))
            {
                mc.convex = true;
            }
        }
    }
}
