using UnityEngine;

public class BackstoryManager : MonoBehaviour
{
    public float waitTime = 20.0f;
    public string meow = "MainScene";

    // Update is called once per frame
    void Update()
    {
        if(waitTime < 0)
        {
            Utils.SwitchScene(meow);
        }
        waitTime -= Time.deltaTime;
    }
}
