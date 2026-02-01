using UnityEngine;
using UnityEngine.SceneManagement;

public static class Utils
{
    public static void SwitchScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public static void SwitchScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }
}
