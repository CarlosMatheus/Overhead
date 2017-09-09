using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneVerifier : MonoBehaviour
{
    public static bool IsInMainSceneOrTutorial()
    {
        if (SceneManager.GetActiveScene().name == "Main" || SceneManager.GetActiveScene().name == "Tutorial")
            return true;
        else
            return false;
    }
}
