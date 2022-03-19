using UnityEngine;
using System.Collections.Generic;

public class InteractibleObjectResetManager : GenericSingletonClass<InteractibleObjectResetManager>
{
    public List<InteractibleObjectSpawnLocation> InteractibleObjectSpawnLocations;

    Interactible[] InteractibleObjects;
    List<Vector3> InteractibleObjectPositons = new List<Vector3>();
    List<string> InteractibleObjectNames = new List<string>();
    List<Quaternion> InteractibleObjectRotations = new List<Quaternion>();

    public override void Awake()
    {
        base.Awake();

        InteractibleObjects = FindObjectsOfType<Interactible>();

        foreach (var interactible in InteractibleObjects)
        {
            if (interactible.ResetOnLifeSwap)
            {
                InteractibleObjectPositons.Add(interactible.transform.position);
                InteractibleObjectNames.Add(interactible.transform.root.name);
                InteractibleObjectRotations.Add(interactible.transform.rotation);
            }
        }
    }

    public void Reset()
    {
        foreach (var keyManager in FindObjectsOfType<KeyManager>())
        {
            keyManager.CurrentKey = KeyManager.Key.None;
            keyManager.KeyFPSViewGO.SetActive(false);
        }

        foreach (var spawnedInteractible in FindObjectsOfType<Interactible>())
        {
            if (spawnedInteractible.ResetOnLifeSwap)
            {
                Destroy(spawnedInteractible.transform.root.gameObject);
            }
        }

        for (int i = 0; i < InteractibleObjectPositons.Count; i++)
        {
            Instantiate(Resources.Load("Interactibles/" + InteractibleObjectNames[i]), InteractibleObjectPositons[i], InteractibleObjectRotations[i]);
        }
    }
}
