using UnityEngine;
using UnityEngine.SceneManagement;

public static class Utility
{
    static void SwitchScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    static void SwitchScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }
}
