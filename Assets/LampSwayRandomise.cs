using UnityEngine;

public class LampSwayRandomise : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Animator>().speed = Random.Range(1f, 2f);
    }
}