using UnityEngine;

public class AudioManager : GenericSingletonClass<AudioManager>
{
    public AK.Wwise.Bank MusicSoundBank;
    public AK.Wwise.Event MusicStartEvent;
    public AK.Wwise.Event KeyPickupEvent;
    public AK.Wwise.Event ChangeLifeEvent;
    public AK.Wwise.Event ExploringEvent;

    public override void Awake()
    {
        base.Awake();

        MusicSoundBank.Load();
    }

    private void Start()
    {
        MusicStartEvent.Post(gameObject);
    }
}
