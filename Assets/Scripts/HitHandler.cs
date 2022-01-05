using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HitHandler : MonoBehaviour
{
    [SerializeField] private float prebuffHealth;
    private float health;
    [SerializeField] private float flashTime;
    [SerializeField] private GameObject healthBarObject;

    private float maxHealth = 0;


    private SharedSceneState sharedSceneState;

    private bool fading = false;
    private bool returning = false;
    private float alphaChangeSpeed = 0;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator anim;

    private HealthBar healthBar;

    private ApplyBoosts boosts;

    public bool isDead = false;

    private bool isPlayer;

    [SerializeField] private GameObject playerHealthBarObject;

    //private float oldHealthBuff;

    private void Awake()
    {
        health = prebuffHealth;
        isPlayer = this.gameObject.CompareTag("Player");
        if(isPlayer)
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        if (!isPlayer)
        {
            health *= (1+(GameObject.FindGameObjectWithTag("Player").GetComponent<ApplyBoosts>().powerLevel()/100f));

        }
        maxHealth = health;
        if (anim == null)
        {
            anim = GetComponentInChildren<Animator>();
        }
        
        sharedSceneState = GameObject.FindGameObjectWithTag("SharedState").GetComponent<SharedSceneState>();
        
    }

    // Start is called before the first frame update
    void Start()
    {
        boosts = GetComponent<ApplyBoosts>();
        alphaChangeSpeed = 2 / flashTime;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if(isPlayer)
        {
            healthBarObject = playerHealthBarObject;//GameObject.Instantiate(healthBarObject, playerHealthBarLocation);
            healthBar = healthBarObject.GetComponentInChildren<HealthBar>();
            healthBar.SetMaxHealth(maxHealth);
        }
    }

    // Update is called once per frame
    void Update()
    {
        HandleFlashAnimation();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isPlayer)
        {
            Bullet bulletScript;
            if (collision.gameObject.TryGetComponent<Bullet>(out bulletScript))
            {
                
                TakeDamage(bulletScript.shotDamage);
                bulletScript.stopBullet();
            }
        }
    }

    public void TakeDamage(float damage)
    {
        if(health == maxHealth && !isPlayer && health > damage && healthBar == null)
        {
            healthBarObject = GameObject.Instantiate(healthBarObject, transform.position + Vector3.up + Vector3.left*.2f, Quaternion.identity, transform);
            healthBarObject.SetActive(true);
            healthBar = healthBarObject.GetComponentInChildren<HealthBar>();
            healthBar.SetMaxHealth(maxHealth);
        }
        if(isPlayer)
        {
            damage /= (100+boosts.getArmorBuff())/100;
        }
        health -= damage;
        health = health <= 0 ? 0 : health;
        if (healthBar != null)
        {
            healthBar.SetHealth(health);
        }
        if (!fading && !returning)
        {
            fading = true;
        }
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die(bool gameEnded = false)
    {
        if (!isDead)
        {
            isDead = true;
            anim.SetTrigger("isDead");

            AudioSource audioSource = GetComponent<AudioSource>();
            if(audioSource != null)
            {
                StartCoroutine(FadeAudioSource.StartFade(audioSource, 1, 0));
            }

            this.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            SetOpaque();
            healthBarObject.SetActive(false);

            if (!isPlayer)
            {
                GetComponent<EnemyAI>().DisableEnemy();
                spriteRenderer.sortingOrder = 10;
                if (!gameEnded)
                {
                    sharedSceneState.zombieKilledCounter++;
                }
            }
            else
            {
                //todo wait for death animation to finish and end game
                Debug.Log("player is dead");
                SceneManager.LoadScene("LossScene");

            }
            DeathBehaviour actionOnDeath = GetComponent<DeathBehaviour>();
            if(actionOnDeath != null)
            {
                actionOnDeath.Died();
            }
        }
    }

    void HandleFlashAnimation()
    {
        if (fading)
        {
            Color color = spriteRenderer.color;
            color.a -= alphaChangeSpeed * Time.deltaTime;
            spriteRenderer.color = color;
            if (color.a <= 0)
            {
                fading = false;
                returning = true;
            }
        }
        if (returning)
        {
            Color color = spriteRenderer.color;
            color.a += alphaChangeSpeed * Time.deltaTime;
            spriteRenderer.color = color;
            if (color.a >= 1)
            {
                returning = false;
            }
        }
    }

    void SetOpaque()
    {
        Color color = spriteRenderer.color;
        color.a = 1;
        spriteRenderer.color = color;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (isPlayer)
        {
            if (scene.name == "Town")
            {
                Debug.Log("health = max health");
                health = maxHealth;
                if(healthBar)
                {
                    healthBar.SetHealth(health);
                }
            }
        }
    }
}
