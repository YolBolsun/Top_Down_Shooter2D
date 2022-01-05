using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float shotDamage = 0;
    public float timeToExplode = 3f;
    public float explosionRadius = 3f;

    //private float shotSpeed = 0;
    private Vector3 shotDirection = Vector3.zero;
    float startTime;

    private bool exploded = false;

    //private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        // anim = GetComponent<Animator>();
        startTime = Time.realtimeSinceStartup;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(shotDirection * shotSpeed * Time.deltaTime);
        if(!exploded && Time.realtimeSinceStartup > startTime + timeToExplode)
        {
            exploded = true;
            Explode();
        }
    }

    public void Explode()
    {
        Collider2D[] affectedTargets = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach(Collider2D affectedTarget in affectedTargets)
        {
            HitHandler hit = affectedTarget.GetComponent<HitHandler>();
            if(hit != null && !hit.gameObject.CompareTag("Player"))
            {
                float damage = (affectedTarget.transform.position - transform.position).magnitude / explosionRadius;
                damage = Mathf.Max(1f - damage, 0f);
                hit.TakeDamage(damage*shotDamage);
            }
        }
        GetComponent<ParticleSystem>().Play();
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        GameObject.Destroy(this.gameObject, .8f);
    }

    public void setState(float speed, float damage, Vector3 direction)
    {
        GetComponent<Rigidbody2D>().AddForce(direction * speed, ForceMode2D.Impulse);
        GetComponent<Rigidbody2D>().AddTorque(speed, ForceMode2D.Impulse);
        shotDamage = damage;
        shotDirection = direction;
    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
