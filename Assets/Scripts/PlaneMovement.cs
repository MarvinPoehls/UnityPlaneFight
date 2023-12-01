using Unity.VisualScripting;
using UnityEngine;

public class PlaneMovement : MonoBehaviour
{
    protected Rigidbody2D rb;

    public float MaxSpeed = 8;
    public float Acceleration = 8;
    public float RotationControl = 5;
    public float Health = 100;
    public float Stamina = 100;
    protected float MaxStamina;
    protected float movY = 1;
    protected float lift;

    public GameObject shootObject;
    public Transform bullletSpawn;

    protected float timeStamp;
    public float coolDownInSeconds;

    protected bool isDead = false;
    protected bool isBoosting = false;

    protected float upsideDown;

    protected void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        MaxStamina = Stamina;
    }

    protected virtual void Update()
    {
        movY = Input.GetAxis("Vertical");
        lift = 9.81f * rb.mass;

        if (Health <= 0) {
            isDead = true;
        }

        if (isBoosting) {
            Stamina -= 0.05f;
        } else {
            Stamina += 0.01f;
        }

        if (Stamina <= 0) {
            EndBoost();
        }

        if (Stamina > MaxStamina) {
            Stamina = MaxStamina;
        }

        if (isDead)
        {
            Destroy(gameObject, 2);
        }

        if (IsPlaneUpsideDown())
        {
            FlipPlane();
        }
    }

    protected void Shoot()
    {
        if (timeStamp <= Time.time) {
            Instantiate(shootObject, bullletSpawn.position, transform.rotation);
            timeStamp = Time.time + coolDownInSeconds;
        }
    }

    protected void Brake()
    {
        if (rb.velocity.x > 0) {
            rb.AddForce(transform.right * -10);
        }
    }

    protected void Boost()
    {
        if (!isBoosting) {
            MaxSpeed *= 2f;
            Acceleration *= 2f;
            isBoosting = true;
        }
    }

    protected void EndBoost()
    {
        if (isBoosting) {
            MaxSpeed *= 0.5f;
            Acceleration *= 0.5f;
            isBoosting = false;
        }
    }

    protected void SetRotation()
    {
        float Dir = Vector2.Dot(rb.velocity, rb.GetRelativeVector(Vector2.right));

        if(Acceleration > 0) {
            if(Dir > 0) {
                rb.rotation += movY * RotationControl * (rb.velocity.magnitude / MaxSpeed); 
            } else {
                rb.rotation -= movY * RotationControl * (rb.velocity.magnitude / MaxSpeed); 
            }
        }
    }

    protected void SpeedLimit()
    {
        if(rb.velocity.magnitude > MaxSpeed) {
            rb.velocity = rb.velocity.normalized * MaxSpeed;
        }
    }

    protected bool IsPlaneUpsideDown()
    {
        return Vector3.Dot(transform.up, Vector3.down) > 0;
    }

    protected void FlipPlane()
    {
        transform.Rotate(new Vector3(180, 0, 0));
    }

    public bool IsDead()
    {
        return isDead;
    }
}