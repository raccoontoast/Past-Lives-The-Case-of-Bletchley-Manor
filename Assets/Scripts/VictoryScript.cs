using System.Collections;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class VictoryScript : MonoBehaviour
{
    public GameObject WinText;
    public AK.Wwise.Event WinMusicTriggerEvent;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("You win.");
        StartCoroutine(Win());
    }

    IEnumerator Win()
    {
        AudioManager.Instance.MusicStartEvent.Stop(AudioManager.Instance.gameObject, 500);
        WinMusicTriggerEvent.Post(AudioManager.Instance.gameObject);

        WinText.SetActive(true);
        FindObjectOfType<FirstPersonController>().enabled = false;

        yield return new WaitForSeconds(15f);

        Application.Quit();
    }
}
