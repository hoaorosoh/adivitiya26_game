using UnityEngine;

public class NightBorne : MonoBehaviour
{
    Animator anim;
    public GameObject hitbox1, hitbox2;
    public float speed = 1.0f;
    const float attackRange = 2.0f;
    GameObject player = null;

    private enum State
    {
        IDLE, RUNNING, ATTACKING, HURT, DYING
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        hitbox2.SetActive(false);
    }

    private void Update()
    {
        //AnimationDebug();
    }

    private void FixedUpdate()
    {
        if (player != null) Behaviour(player);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("PLayer entered");
            player = collision.gameObject;
        }
    }

    void Behaviour(GameObject player)
    {
        if (player == null) { return; }
        if (
            (player.transform.position
            - this.transform.position).sqrMagnitude
            > attackRange)
        {
            runto(player);
        }
    }

    void stateTransition(State s)
    {
        return;
    }

    void runto(GameObject player)
    {
        if (player == null) { return; }
        Debug.Log("running");
        stateTransition(State.RUNNING);
        if (Mathf.Round(this.transform.position.y) > Mathf.Round(player.transform.position.y))
        {
            Debug.Log("PLayer is Under");
            Debug.Log(player.transform.position);
            this.transform.position = new Vector3(
                this.transform.position.x,
                this.transform.position.y - (speed * Time.fixedDeltaTime),
                this.transform.position.z);
        } else if (Mathf.Round(this.transform.position.y) < Mathf.Round(player.transform.position.y))
        {
            this.transform.position = new Vector3(
                this.transform.position.x,
                this.transform.position.y + (speed * Time.fixedDeltaTime),
                this.transform.position.z);
        }
        else if (this.transform.position.x > player.transform.position.x)
        {
            this.transform.position = new Vector3(
                this.transform.position.x - (speed * Time.fixedDeltaTime),
                this.transform.position.y,
                this.transform.position.z);
        }
        else if (this.transform.position.x < player.transform.position.x)
        {
            this.transform.position = new Vector3(
                this.transform.position.x + (speed * Time.fixedDeltaTime),
                this.transform.position.y,
                this.transform.position.z);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("PLayer exited");
            player = null;
        }
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
