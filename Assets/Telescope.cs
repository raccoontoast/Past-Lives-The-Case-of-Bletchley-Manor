using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Telescope : Interactible
{
    public bool PlayerIsUsingTelescope = false;

    public GameObject HiddenPath;
    public GameObject TelescopeCameraRig;

    private void Awake()
    {
        TelescopeCameraRig.SetActive(false);
    }

    public override void Interact(GameObject Interacter)
    {
        Interacter.GetComponent<FirstPersonController>().enabled = PlayerIsUsingTelescope;
        Interacter.GetComponentInChildren<Camera>().enabled = PlayerIsUsingTelescope;
        TelescopeCameraRig.SetActive(!PlayerIsUsingTelescope);

        PlayerIsUsingTelescope = !PlayerIsUsingTelescope;
    }

    private void Update()
    {
        if (PlayerIsUsingTelescope)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Camera>().enabled = true;
                GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>().enabled = true;
                TelescopeCameraRig.SetActive(false);
                PlayerIsUsingTelescope = false;
            }
        }
    }
}
