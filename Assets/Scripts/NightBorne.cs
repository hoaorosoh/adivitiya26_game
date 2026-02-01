//using UnityEditor.Tilemaps;
using System;
using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class NightBorne : MonoBehaviour
{
    Animator anim;
    public GameObject hitbox1, hitbox2;
    public Sprite attackLanded;
    SpriteRenderer spreet;
    public float speed = 1.0f;
    public float attackCooldown = 1.0f;
    bool isAttacking = false;
    public float attackRange = 3.0f;
    GameObject player = null;
    int facingDirection = 1;

    State currentState = State.IDLE;

    private enum State
    {
        IDLE, RUNNING, ATTACKING, HURT, DYING
    }

    private void Start()
    {
        spreet = this.gameObject.GetComponent<SpriteRenderer>();
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

    bool IsCurrentSpriteEqualToAttackLanded()
    {
        return spreet.sprite == attackLanded;
    }

    IEnumerator Attack()
    {
        isAttacking = true;
        yield return new WaitUntil(IsCurrentSpriteEqualToAttackLanded);
        Debug.Log("meow");
        hitbox2.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        hitbox2.SetActive(false);
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
                hitbox2.SetActive(false);
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

    void runto(GameObject player)
    {
        if (player == null) { return; }
        //Debug.Log("running");
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
