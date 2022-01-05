using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float shotDamage = 0;

    private float shotSpeed = 0;
    private Vector3 shotDirection = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(shotDirection * shotSpeed * Time.deltaTime);
    }

    public void stopBullet()
    {
        GameObject.Destroy(this.gameObject);
    }

    public void setState(float speed, float damage, Vector3 direction)
    {
        shotSpeed = speed;
        shotDamage = damage;
        shotDirection = direction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<HitHandler>().TakeDamage(shotDamage);
            Destroy(this);
        }
    }
}
