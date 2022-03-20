using UnityEngine;

public class WaterfallLever : Interactible
{
    public GameObject Waterfall;
    public Collider WaterfallCollider;

    public override void Interact(GameObject Interacter)
    {
        if (WaterfallCollider.enabled == false)
            return;
        
        foreach (var particleSystem in Waterfall.GetComponentsInChildren<ParticleSystem>())
        {
            particleSystem.Stop(true);
        }

        WaterfallCollider.enabled = false;

        transform.Rotate(new Vector3(0f, 180f, 0f));
        transform.localPosition = new Vector3(3.788308f, 0f, -5.0811095f); // Lol
    }
}
