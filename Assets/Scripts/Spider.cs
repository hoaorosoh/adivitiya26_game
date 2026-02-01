//using UnityEditor.Tilemaps;
using System;
using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Spider : MonoBehaviour
{
    Animator anim;
    public float speed = 10.0f;
    public float attackCooldown = 1.0f;
    bool isAttacking = false;
    public float attackRange = 1.0f;
    GameObject player = null;
    int facingDirection = 1;

    State currentState = State.IDLE;

    private enum State
    {
        IDLE, RUNNING, ATTACKING, DYING
    }

    private void Start()
    {
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

    public void FreakyOnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Debug.Log("PLayer entered");
            player = collision.gameObject;
        }
    }

    void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(
            transform.localScale.x * -1,
            transform.localScale.y,
            transform.localScale.z);
    }

    IEnumerator Attack()
    {
        isAttacking = true;
        yield return new WaitForSeconds(0.1f);
        stateTransition(State.IDLE);
        yield return new WaitForSeconds(attackCooldown);
        isAttacking = false;
    }

    void Behaviour(GameObject player)
    {
        if (player == null) { return; }

        if (this.transform.position.x < player.transform.position.x && facingDirection == -1
            || this.transform.position.x > player.transform.position.x && facingDirection == 1) { Flip(); }

        if (
            (player.transform.position
            - this.transform.position).sqrMagnitude
            > attackRange)
        {
            runto(player);
        }
        else
        {
            if (!isAttacking)
            {
                stateTransition(State.ATTACKING);
                StartCoroutine(Attack());
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
            case State.DYING:
                anim.SetBool("IsDead", true);
                break;

        }
    }

    void runto(GameObject player)
    {
        if (player == null) { return; }
        stateTransition(State.RUNNING);
        Vector2 dir = (-this.transform.position + player.transform.position).normalized;
        this.transform.position = new Vector3(
            this.transform.position.x + (dir.x * speed * Time.fixedDeltaTime),
            this.transform.position.y + (dir.y * speed * Time.fixedDeltaTime),
            this.transform.position.z);
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
