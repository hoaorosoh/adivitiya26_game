using UnityEngine;

public class NightBorne : MonoBehaviour
{
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        AnimationDebug();
    }

    void AnimationDebug()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            anim.SetBool("IsRunning", true);
        }
        else
        {
            anim.SetBool("IsRunning", false);
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            anim.SetBool("IsAttacking", true);
        }
        else
        {
            anim.SetBool("IsAttacking", false);
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            anim.SetBool("IsHurt", true);
        }
        else
        {
            anim.SetBool("IsHurt", false);
        }
        if (Input.GetKey(KeyCode.Alpha4))
        {
            anim.SetBool("IsDead", true);
        }
        else
        {
            anim.SetBool("IsDead", false);
        }
    }

}
