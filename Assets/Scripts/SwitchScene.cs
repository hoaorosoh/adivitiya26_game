using UnityEngine;

public class SwitchScene : MonoBehaviour
{
    public string sceneName;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Utils.SwitchScene(sceneName);
        }
    }
}
