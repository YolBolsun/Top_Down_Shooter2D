using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveVelocity : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private Vector3 velocityVector;
    private Rigidbody2D rigidBody2DComponent;
    private float velocityMultiplyer = 1f;
    private float endTimeOfVelocityMultiplyer = 0f;

    private ApplyBoosts boosts;

    private void Awake()
    {
        rigidBody2DComponent = GetComponent<Rigidbody2D>();
        boosts = GetComponent<ApplyBoosts>();
    }

    public void SetVelocity(Vector3 velocityVector)
    {
        this.velocityVector = velocityVector;
    }

    private void FixedUpdate()
    {
        if(Time.realtimeSinceStartup > endTimeOfVelocityMultiplyer)
        {
            velocityMultiplyer = 1f;
        }
        //rigidBody2DComponent.velocity = velocityVector * (moveSpeed * (100+boosts.getMovementSpeedBuff())/100) * velocityMultiplyer;
        transform.Translate(velocityVector * (moveSpeed * (100 + boosts.getMovementSpeedBuff()) / 100) * velocityMultiplyer * Time.deltaTime);
    }

    public void ApplyVelocityMultiplyer(float multiplyer, float time)
    {
        endTimeOfVelocityMultiplyer = Time.realtimeSinceStartup + time;
        velocityMultiplyer = multiplyer;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Boost"))
        {
            Boost boost = collision.gameObject.GetComponent<Boost>();
            moveSpeed *= boost.movementSpeedModifier;
            if (boost.movementSpeedModifier != 1)
            {
                GameObject.Destroy(collision.gameObject);
            }
        }
    }
}
