using UnityEngine;

public class BackstoryManager : MonoBehaviour
{
    public float waitTime = 20.0f;

    // Update is called once per frame
    void Update()
    {
        if(waitTime < 0)
        {
            Utils.SwitchScene("MainScene");
        }
        waitTime -= Time.deltaTime;
    }
}
