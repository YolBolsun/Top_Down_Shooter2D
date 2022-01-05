using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyThrowerScript : MonoBehaviour
{
    [SerializeField] private GameObject thrownObjectPrefab;
    [SerializeField] private float aggroDistance;

    [SerializeField] private float attackDamage;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float instantiationDistance;

    [SerializeField] private float thrownObjectSpeed;
    [SerializeField] private float thrownObjectLifetime;

    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource aggroSound;

    private bool isAggroed = false;
    private float lastAttack = 0;
    private float timeBetweenAttacks;

    private bool flipDirection = false;


    private GameObject playerTarget;

    public bool FlipDirection { get => flipDirection;
        set
        {
            if(flipDirection != value)
            {
                Debug.Log("setting flip direction to " + value);
                //SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
                //spriteRenderer.flipX = value;
                transform.localScale = new Vector3(flipDirection ? 1 : -1, 1, 1);
            }
            flipDirection = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerTarget = GameObject.FindGameObjectWithTag("Player");
        timeBetweenAttacks = 1 / attackSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if((playerTarget.transform.position - this.transform.position).magnitude < aggroDistance)
        {
            if(!isAggroed)
            {
                aggroSound.Play();
            }
            isAggroed = true;
            if((playerTarget.transform.position - this.transform.position).x > 0)
            {
                FlipDirection = true;
            }
            else
            {
                FlipDirection = false;
            }
        }
        if(isAggroed && Time.realtimeSinceStartup > lastAttack + timeBetweenAttacks)
        {
            lastAttack = Time.realtimeSinceStartup;
            animator.SetTrigger("Throw");
            StartCoroutine(ThrowAfterAnimation(.4f));
        }
    }
    IEnumerator ThrowAfterAnimation(float time)
    {
        yield return new WaitForSeconds(time);

        Vector3 direction = (playerTarget.transform.position - transform.position).normalized;
        GameObject thrownObject = GameObject.Instantiate(thrownObjectPrefab, transform.position + direction * instantiationDistance, Quaternion.identity);
        thrownObject.GetComponent<EnemyProjectile>().setState(thrownObjectSpeed, attackDamage, direction);
        GameObject.Destroy(thrownObject, thrownObjectLifetime);
    }


}
