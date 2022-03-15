using UnityEngine;
using System.Collections.Generic;

public class InteractibleObjectResetManager : GenericSingletonClass<InteractibleObjectResetManager>
{
    public List<InteractibleObjectSpawnLocation> InteractibleObjectSpawnLocations;

    public override void Awake()
    {
        base.Awake();

        foreach (var interactibleObjectSpawnLocation in InteractibleObjectSpawnLocations)
        {
            Instantiate(interactibleObjectSpawnLocation.GameObject, interactibleObjectSpawnLocation.transform.position, Quaternion.identity);
        }
    }

    public void Reset()
    {
        foreach (var spawnedInteractible in FindObjectsOfType<Interactible>())
        {
            Destroy(spawnedInteractible.gameObject);
        }

        foreach (var interactibleObjectSpawnLocation in InteractibleObjectSpawnLocations)
        {
            Instantiate(interactibleObjectSpawnLocation.GameObject, interactibleObjectSpawnLocation.transform.position, Quaternion.identity);
        }
    }
}
