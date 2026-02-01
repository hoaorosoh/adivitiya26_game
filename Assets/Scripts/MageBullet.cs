using UnityEngine;

public class MageBullet : EnemyCombat
{
    private Vector3 velocity = Vector3.zero;
    public float lifeTime = 60.0f;
    private float remainingTime;

    private void Start()
    {
        remainingTime = lifeTime;
    }

    public void SetVelocity(Vector3 v)
    {
        velocity = v;
    }

    public Vector3 GetVelocity()
    {
        return velocity;
    }

    private void FixedUpdate()
    {
        if (remainingTime > 0)
        {
            this.transform.position = new Vector3(
                this.transform.position.x + (velocity.x * Time.fixedDeltaTime),
                this.transform.position.y + (velocity.y * Time.fixedDeltaTime),
                this.transform.position.z + (velocity.z * Time.fixedDeltaTime));
            remainingTime -= Time.fixedDeltaTime;
        }
        else
        {
            Destroy(this);
        }
    }
}
