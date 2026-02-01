//using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5;
    public int facingDirection = 1;

    public Rigidbody2D rb;
    public Animator anim;

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if ((horizontal > 0 && transform.localScale.x < 0) || (horizontal < 0 && transform.localScale.x > 0))
        {
            Flip();
        }

        anim.SetFloat("horizontal", Mathf.Abs(horizontal));
        anim.SetFloat("vertical", Mathf.Abs(vertical));

        Vector2 movement = new Vector2(horizontal, vertical);
        rb.linearVelocity = movement * speed;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.transform.position = this.transform.position + new Vector3(facingDirection * 10.0f, 0, 0);
        }
    }

    void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
}