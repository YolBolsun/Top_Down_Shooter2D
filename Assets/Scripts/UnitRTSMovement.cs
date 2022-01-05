using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;


public class UnitRTSMovement : MonoBehaviour
{
    [SerializeField] private float closeEnoughDistance;
    private Vector3 destination;
    private MoveVelocity moveVelocity;

    private Animator anim;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        destination = this.transform.position;
        moveVelocity = GetComponent<MoveVelocity>();
        anim = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void SetDestination(Vector3 targetDest)
    {
        destination = targetDest;  
    }

    private void Update()
    {
        Vector3 directionVector = Vector3.zero;
        if (SharedSceneState.inputEnabled)
        {
            if (Input.GetKey(KeyCode.W))
            {
                directionVector += Vector3.up;
            }
            if (Input.GetKey(KeyCode.A))
            {
                directionVector += Vector3.left;
            }
            if (Input.GetKey(KeyCode.S))
            {
                directionVector += Vector3.down;
            }
            if (Input.GetKey(KeyCode.D))
            {
                directionVector += Vector3.right;
            }
        }
       /* if(directionVector != Vector3.zero)
        {*/
            destination = this.transform.position + directionVector;
       // }
        if (destination.x > transform.position.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
        Vector3 direction = (destination - this.transform.position);
        if (direction.magnitude > closeEnoughDistance)
        {
            moveVelocity.SetVelocity(direction.normalized);
            anim.SetTrigger("isRunning");
        }
        else
        {
            moveVelocity.SetVelocity(Vector3.zero);
            anim.SetTrigger("isIdle");
        }
    }
}
