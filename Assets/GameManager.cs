using UnityEngine;

public class GameManager : GenericSingletonClass<GameManager>
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
}
