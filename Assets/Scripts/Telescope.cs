using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Telescope : Interactible
{
    public bool PlayerIsUsingTelescope = false;

    public GameObject HiddenPath;
    public GameObject TelescopeCameraRig;
    public GameObject TelescopeOverlay;
    public GameObject TelescopeCylinderModel;

    private void Awake()
    {
        TelescopeCameraRig.SetActive(false);
    }

    public override void Interact(GameObject Interacter)
    {
        Interacter.GetComponent<FirstPersonController>().enabled = PlayerIsUsingTelescope;
        Interacter.GetComponentInChildren<Camera>().enabled = PlayerIsUsingTelescope;
        TelescopeCameraRig.SetActive(!PlayerIsUsingTelescope);
        TelescopeOverlay.SetActive(!PlayerIsUsingTelescope);
        TelescopeCylinderModel.SetActive(PlayerIsUsingTelescope);

        foreach (var pathSegmentRenderer in HiddenPath.GetComponentsInChildren<Renderer>())
        {
            pathSegmentRenderer.enabled = true;
        }

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
                TelescopeOverlay.SetActive(false);
                PlayerIsUsingTelescope = false;
                TelescopeCylinderModel.SetActive(true);

                foreach (var pathSegmentRenderer in HiddenPath.GetComponentsInChildren<Renderer>())
                {
                    pathSegmentRenderer.enabled = false;
                }
            }
        }
    }
}
