using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public GameObject player;
    public GameObject projectile;
    [SerializeField] float speed;
    private Rigidbody enemyRb;
    [SerializeField] float health;
    [SerializeField] float damage;
    private float size;
    private int coinValue;

    public float speedScaler = 2.0f;
    public float damageScaler = 1.0f;
    public float sizeScaler = 1.0f;
    public float maxHealth = 20.0f;
    public float minHealth = 5.0f;
    public float minSize = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        health = Random.Range(minHealth, maxHealth);
        coinValue = Mathf.RoundToInt(health);
    }

    // Update is called once per frame
    void Update()
    {
        // Stay at y position
        transform.position = new Vector3(transform.position.x, size/2, transform.position.z);

        // Scale damage and speed with health
        speed = speedScaler / (health + 5);
        damage = health * damageScaler;

        // Scale size with health
        size = (health + minSize) * sizeScaler / 10;
        transform.localScale = new Vector3(size, size, size);

        // Move towards player
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        enemyRb.AddForce(lookDirection * speed);

        if (health <= 0)
        {
            FindObjectOfType<EnemySound>().playDeathCoin();
            Destroy(gameObject);
            FindObjectOfType<PlayerCoins>().UpdateCoins(coinValue);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Projectile")
        {
            health -= FindObjectOfType<PlayerController>().GetProjectileDamage();
            FindObjectOfType<EnemySound>().playHitSound();
        }    
    }

    public float GetDamage()
    {
        return damage;
    }
}
