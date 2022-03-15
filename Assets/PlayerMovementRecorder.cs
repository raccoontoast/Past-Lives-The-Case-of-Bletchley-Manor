using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerMovementRecorder : MonoBehaviour
{
    // Recording
    private bool isRecording;
    public bool IsRecording
    {
        get
        {
            return isRecording;
        }
        set
        {
            isRecording = value;
            GetComponent<FirstPersonController>().enabled = isRecording;
        }
    }    
    [HideInInspector] public RecordInfo RecInfo;
    public RecordInfo.Interaction CurrentInteraction = RecordInfo.Interaction.None;

    // Clone
    public bool IsPlayer;
    public GameObject PlayerClonePrefab;
    public string CurrentLevel;

    // Other
    private FirstPersonController FPSController;

    void Start()
    {
        if (!IsPlayer) // if this is a clone
        {
            Destroy(GetComponentInChildren<Camera>());
            Destroy(GetComponentInChildren<AudioListener>());
            IsRecording = false;
        }
        else
        {
            RecInfo = new RecordInfo();
            IsRecording = true;
            CurrentLevel = "lower";
            FPSController = GetComponent<FirstPersonController>();
        }
    }

    private void FixedUpdate()
    {
        if (IsRecording)
        {
            RecInfo.PositionList.Add(transform.position);
            RecInfo.ActionList.Add(CurrentInteraction);
            RecInfo.CameraYRotationList.Add(transform.rotation);
            RecInfo.CameraXRotationList.Add(GetComponentInChildren<Camera>().transform.rotation);
        }

        // Init Interaction for recording
        CurrentInteraction = RecordInfo.Interaction.None;
    }

    private void Update()
    {
        // Testing
        if (Input.GetKeyDown(KeyCode.F))
        {
            InstantiatePlayerClone();

            // Reset Interactible Objects
            InteractibleObjectResetManager.Instance.Reset();
        }
    }

    void InstantiatePlayerClone()   
    {
        // Find the spawn point in the room which the player currently isn't in
        Transform PlayerSpawnTransform = transform; // Has to be instantiated?

        foreach (var room in GameObject.FindGameObjectsWithTag("Level"))
        {
            Floor roomScript = room.GetComponent<Floor>();

            if (roomScript.Level != CurrentLevel) // If the room being examined is not the room the player is currently in
            {
                CurrentLevel = roomScript.Level;
                PlayerSpawnTransform = roomScript.SpawnPosition.transform;
                break;
            }
        }
        
        GameObject clone = Instantiate(PlayerClonePrefab, RecInfo.PositionList[0], Quaternion.identity);
        clone.GetComponent<CloneMovementPlayback>().RecInfo = new RecordInfo();
        clone.GetComponent<CloneMovementPlayback>().RecInfo.CopyInfoFrom(RecInfo); // Copies the players recinfo to the clone's recinfo
        RecInfo.Clear();

        // Coroutine for teleporting as we have to wait a frame
        IEnumerator TeleportPlayer()
        {
            FPSController.enabled = false;
            transform.position = PlayerSpawnTransform.position;
            transform.rotation = PlayerSpawnTransform.rotation;
            yield return new WaitForFixedUpdate();
            FPSController.ResetMouseLook();
            FPSController.enabled = true;
        }

        // Now teleport player to second level
        StartCoroutine(TeleportPlayer());
    }
}
