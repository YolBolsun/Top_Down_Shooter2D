using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float ambleSpeed = 1f;
    [SerializeField] private float ambleRangeModifier = 1f;
    [SerializeField] private Transform target;
    [SerializeField] private float aggroDistance = 500f;

    [SerializeField] private float attackRange;
    [SerializeField] private float attackDamage;
    [SerializeField] private float attackCooldown;

    private float timeOfLastAttack = 0;
    private SpriteRenderer spriteRenderer;
    private Vector3 ambleDirection;

    private Vector3 spawnLocation;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        spawnLocation = transform.position;
        ambleDirection = UtilsClass.GetRandomDir() * ambleRangeModifier + (spawnLocation - transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 distanceToTarget = (target.position - transform.position);
        if(distanceToTarget.magnitude <= attackRange)
        {
            if (Time.realtimeSinceStartup > timeOfLastAttack + attackCooldown)
            {
                timeOfLastAttack = Time.realtimeSinceStartup;
                //todo play zombie attack animation
                target.gameObject.GetComponent<HitHandler>().TakeDamage(attackDamage);
            }
        }
        else
        {
            Vector3 direction = distanceToTarget.normalized;
            if(direction.x <= 0)
            {
                spriteRenderer.flipX = true;
            }
            else
            {
                spriteRenderer.flipX = false;
            }
            if (distanceToTarget.magnitude < aggroDistance)
            {
                transform.Translate(direction * movementSpeed * Time.deltaTime);
            }
            else
            {
                if ((transform.position-spawnLocation).magnitude > ambleRangeModifier)
                {
                    ambleDirection = UtilsClass.GetRandomDir() * ambleRangeModifier + (spawnLocation - transform.position);
                }
                transform.Translate(ambleDirection.normalized * ambleSpeed * Time.deltaTime);
            }
        }
        
        this.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ambleDirection = UtilsClass.GetRandomDir() * ambleRangeModifier + (spawnLocation - transform.position);
    }

    public void DisableEnemy()
    {
        this.GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }
}
