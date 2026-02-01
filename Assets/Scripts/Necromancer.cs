using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;
public class Necromancer : MonoBehaviour
{
    Animator anim;
    public float speed = 1.0f;
    public float dashDistance = 6.0f;
    public float maxDashTime = 1.0f;
    public float bulletSpeed = 1.0f;
    public float attackCooldown = 0.5f;
    public MageBullet mageBullet;
    bool isAttacking = false;
    GameObject player = null;
    float dashCooldown = 1.0f;
    Coroutine AttackCoroutine;

    State currentState = State.IDLE;

    private enum State
    {
        IDLE, RUNNING, ATTACKING, HURT, DYING
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        //AnimationDebug();
    }

    private void FixedUpdate()
    {
        if (player != null) Behaviour(player);
    }

    IEnumerator Attack()
    {
        isAttacking = true;
        MageBullet m = Instantiate(mageBullet);
        m.SetVelocity(bulletSpeed * (player.transform.position - this.transform.position).normalized);
        yield return new WaitForSeconds(0.5f);
        stateTransition(State.IDLE);
        yield return new WaitForSeconds(attackCooldown);
        isAttacking = false;
    }

    void Behaviour(GameObject player)
    {
        Assert.IsNotNull(player);

        if (dashCooldown < 0)
        {
            dashCooldown = UnityEngine.Random.Range(3.0f, 5.0f);
            runto(player);
        }
        else
        {
            dashCooldown -= Time.fixedDeltaTime;
            if (!isAttacking)
            {
                stateTransition(State.ATTACKING);
                AttackCoroutine = StartCoroutine(Attack());
            }

        }
    }

    void GoToIdle(State s)
    {
        switch (s)
        {
            case State.IDLE:
                return;
            //break;
            case State.RUNNING:
                anim.SetBool("IsRunning", false);
                break;
            case State.ATTACKING:
                anim.SetBool("IsAttacking", false);
                break;
            case State.HURT:
                anim.SetBool("IsHurt", false);
                break;
            case State.DYING:
                anim.SetBool("IsDead", false);
                break;

        }
    }

    void stateTransition(State s)
    {
        if (s == currentState) return;
        GoToIdle(currentState);
        //yield return new WaitForEndOfFrame();
        EnterState(s);
        currentState = s;
    }

    void EnterState(State s)
    {
        switch (s)
        {
            case State.IDLE:
                return;
            //break;
            case State.RUNNING:
                anim.SetBool("IsRunning", true);
                break;
            case State.ATTACKING:
                anim.SetBool("IsAttacking", true);
                break;
            case State.HURT:
                anim.SetBool("IsHurt", true);
                break;
            case State.DYING:
                anim.SetBool("IsDead", true);
                break;

        }
    }

    IEnumerator Dash(Vector2 dir)
    {
        //float maxDashTime = 1.0f;
        float dashTime = maxDashTime;
        Vector3 d = new Vector3(dir.x * dashDistance, dir.y * dashDistance, this.transform.position.z);
        while(this.transform.position != d && dashTime > 0)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, d, (dashDistance / maxDashTime) * Time.fixedDeltaTime);
            dashTime -= Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        stateTransition(State.IDLE);
    }

    void runto(GameObject player)
    {
        if (isAttacking)
        {
            StopCoroutine(AttackCoroutine);
        }
        stateTransition(State.RUNNING);
        Vector2 dir = (player.transform.position - this.transform.position).normalized;
        dir = new Vector2(
            dir.x > 0 ? 1 : -1,
            dir.y > 0 ? 1 : -1);
        StartCoroutine(Dash(dir));

    }

    public void FreakyOnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Debug.Log("PLayer exited");
            player = null;
            stateTransition(State.IDLE);
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
