using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadScene(string sceneToLoadString)
    {
        //LoadSceneParameters loadSceneParameters = new LoadSceneParameters(LoadSceneMode.Single, LocalPhysicsMode.Physics3D);
        SceneManager.LoadScene(sceneToLoadString/*, loadSceneParameters*/);
    }
}
