using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CodeMonkey.Utils;

public class ShootingController : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float shotSpeed;
    [SerializeField] private float shotDamage;
    [SerializeField] private float shotCooldown;
    [SerializeField] private float instantiationDistance;
    [SerializeField] private float bulletLifetime;

    [SerializeField] private float slowdownMultiplyerWhileShooting;
    private MoveVelocity moveVelocity;

    private Animator anim;
    private float timeOfLastShot;
    private ApplyBoosts boosts;

    [SerializeField] private GameObject secondary;
    [SerializeField] private float secondaryShotSpeed;
    [SerializeField] private float secondaryShotDamage;
    [SerializeField] private float secondaryShotCooldown;
    [SerializeField] private float secondaryInstantiationDistance;
    private float secondaryTimeOfLastShot;

    // Start is called before the first frame update
    void Start()
    {
        timeOfLastShot = Time.realtimeSinceStartup;
        secondaryTimeOfLastShot = Time.realtimeSinceStartup;
        anim = GetComponentInChildren<Animator>();
        moveVelocity = GetComponent<MoveVelocity>();
        boosts = GetComponent<ApplyBoosts>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SharedSceneState.inputEnabled)
        {
            float shotCooldownWithAttackSpeed = 1 / (1 / shotCooldown * ((boosts.getAttackSpeedBuff() + 100) / 100));
            Boost.ShotBehaviour shotBehaviour = boosts.getShootingType();
            switch (shotBehaviour)
            {
                case Boost.ShotBehaviour.Pistol:
                    shotCooldownWithAttackSpeed *= 2;
                    break;
                case Boost.ShotBehaviour.Shotgun:
                    shotCooldownWithAttackSpeed *= 3;
                    break;
                case Boost.ShotBehaviour.Rifle:
                default:
                    break;
            }
            if (Input.GetMouseButton(0) && timeOfLastShot + shotCooldownWithAttackSpeed < Time.realtimeSinceStartup)
            {
                Vector3 direction = (UtilsClass.GetMouseWorldPosition() - transform.position).normalized;
                direction.z = 0;
                timeOfLastShot = Time.realtimeSinceStartup;
                anim.SetTrigger("Shoot");
                switch (shotBehaviour)
                {
                    case Boost.ShotBehaviour.Pistol:
                        ShootPistol(direction);
                        break;
                    case Boost.ShotBehaviour.Shotgun:
                        ShootShotgun(direction);
                        break;
                    case Boost.ShotBehaviour.Rifle:
                    default:
                        ShootRifle(direction);
                        break;
                }
                moveVelocity.ApplyVelocityMultiplyer(slowdownMultiplyerWhileShooting, shotCooldownWithAttackSpeed);
            }
            if(Input.GetMouseButton(1) && secondaryTimeOfLastShot + secondaryShotCooldown < Time.realtimeSinceStartup)
            {
                Vector3 direction = (UtilsClass.GetMouseWorldPosition() - transform.position).normalized;
                direction.z = 0;
                secondaryTimeOfLastShot = Time.realtimeSinceStartup;
                ShootSecondary(direction);
            }
        }
    }

    void ShootSecondary(Vector3 direction)
    {
        Debug.Log("throwing grenade");
        GameObject grenade = GameObject.Instantiate(secondary, transform.position + direction * secondaryInstantiationDistance, Quaternion.identity);
        grenade.GetComponent<Grenade>().setState(secondaryShotSpeed, secondaryShotDamage, direction);
    }

    void ShootRifle(Vector3 direction)
    {
        GameObject bullet = GameObject.Instantiate(bulletPrefab, transform.position + direction * instantiationDistance, Quaternion.identity);
        bullet.GetComponent<Bullet>().setState(shotSpeed, shotDamage + boosts.getDamageBuff(), direction);
        GameObject.Destroy(bullet, bulletLifetime);
    }

    void ShootShotgun(Vector3 direction)
    {
        GameObject bullet = GameObject.Instantiate(bulletPrefab, transform.position + direction * instantiationDistance, Quaternion.identity);
        bullet.GetComponent<Bullet>().setState(shotSpeed, shotDamage + boosts.getDamageBuff(), direction);
        GameObject.Destroy(bullet, bulletLifetime);

        Vector3 shiftedDirection = Quaternion.Euler(0, 0, 10) * direction;
        GameObject bullet2 = GameObject.Instantiate(bulletPrefab, transform.position + shiftedDirection * instantiationDistance, Quaternion.identity);
        bullet2.GetComponent<Bullet>().setState(shotSpeed, shotDamage + boosts.getDamageBuff(), shiftedDirection);
        GameObject.Destroy(bullet2, bulletLifetime);

        Vector3 shiftedDirection2 = Quaternion.Euler(0, 0, -10) * direction;
        GameObject bullet3 = GameObject.Instantiate(bulletPrefab, transform.position + shiftedDirection2 * instantiationDistance, Quaternion.identity);
        bullet3.GetComponent<Bullet>().setState(shotSpeed, shotDamage + boosts.getDamageBuff(), shiftedDirection2);
        GameObject.Destroy(bullet3, bulletLifetime);
    }
    void ShootPistol(Vector3 direction)
    {
        GameObject bullet = GameObject.Instantiate(bulletPrefab, transform.position + direction * instantiationDistance, Quaternion.identity);
        bullet.GetComponent<Bullet>().setState(shotSpeed, shotDamage + boosts.getDamageBuff(), direction);
        GameObject.Destroy(bullet, bulletLifetime);

        GameObject bullet2 = GameObject.Instantiate(bulletPrefab, transform.position + direction * instantiationDistance + Vector3.up *.3f + direction*.2f, Quaternion.identity);
        bullet2.GetComponent<Bullet>().setState(shotSpeed, shotDamage + boosts.getDamageBuff(), direction);
        GameObject.Destroy(bullet2, bulletLifetime);
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Boost"))
        {
            Boost boost = collision.gameObject.GetComponent<Boost>();
            shotCooldown *= boost.attackSpeedModifier;
            shotDamage *= boost.attackDamageModifier;
            if (boost.attackSpeedModifier != 1 || boost.attackDamageModifier != 1)
            {
                GameObject.Destroy(collision.gameObject);
            }
        }
    }*/
}
