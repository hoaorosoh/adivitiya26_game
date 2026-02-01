using UnityEngine;

public class SpiderAggroScript : MonoBehaviour
{

    public Spider parent;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        parent.FreakyOnTriggerEnter2D(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        parent.FreakyOnTriggerExit2D(collision);
    }

}
