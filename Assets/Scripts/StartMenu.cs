using UnityEngine;

public class StartMenu : MonoBehaviour
{

    public void Begin()
    {
        Utils.SwitchScene(1);
    }

    public void End()
    {
        Application.Quit();
    }
}
