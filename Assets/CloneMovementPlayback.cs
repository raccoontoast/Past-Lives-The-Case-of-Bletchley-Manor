using UnityEngine;
using System.Collections.Generic;

public class CloneMovementPlayback : MonoBehaviour
{
    [HideInInspector] public RecordInfo RecInfo;

    private int currentReplayIndex = 0;

    private void FixedUpdate()
    {
        // TODO: Refactor to get rid of GetComponent calls.
        Debug.Log("CRI: " + currentReplayIndex + "| Count: " + RecInfo.PositionList.Count);

        // Movement and rotation
        transform.position = RecInfo.PositionList[currentReplayIndex];
        transform.Find("Camera").rotation = RecInfo.CameraXRotationList[currentReplayIndex]; // X rotation is child
        transform.rotation = RecInfo.CameraYRotationList[currentReplayIndex]; // Y rotation is parent

        // Actions
        switch (RecInfo.ActionList[currentReplayIndex])
        {
            case RecordInfo.Interaction.Action:
                GetComponent<PlayerActions>().TryInteract(transform.Find("Camera"));
                break;
            case RecordInfo.Interaction.Fire:
                GetComponent<KeyManager>().ThrowKey(transform.position, transform.Find("Camera"));
                break;
            case RecordInfo.Interaction.None:
                break;
            default:
                break;
        }

        currentReplayIndex++;

        if (currentReplayIndex >= RecInfo.PositionList.Count)
        {
            Destroy(gameObject);
        }
    }
}
