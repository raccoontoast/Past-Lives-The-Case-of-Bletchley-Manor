using UnityEngine;

public class TestScript : MonoBehaviour
{
    // Public variables for method 1
    public AK.Wwise.Event AudioEvent;
    public AK.Wwise.Switch Switch;

    private void Start() // This will be called when the script loads.
    {
        // Method 1 - Involves setting the event and switch in the Inspector.
        AudioEvent.Post(gameObject); // 'gameObject' is shorthand for the GameObject that this script is attached to.
        Switch.SetValue(gameObject);

        // Method 2 - Hard coded - no setting in inspector but prone to typos.
        AkSoundEngine.PostEvent("AudioEvent", gameObject);
        AkSoundEngine.SetSwitch("SwitchGroupName", "Switch_State", gameObject);
    }
}
