using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float shotDamage = 0;

    private float shotSpeed = 0;
    private Vector3 shotDirection = Vector3.zero;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(shotDirection * shotSpeed * Time.deltaTime);
    }

    public void stopBullet()
    {
        shotSpeed = 0f;
        anim.SetTrigger("Impact");
        GameObject.Destroy(this.gameObject, .4f);
    }

    public void setState(float speed, float damage, Vector3 direction)
    {
        shotSpeed = speed;
        shotDamage = damage;
        shotDirection = direction;
    }
}
