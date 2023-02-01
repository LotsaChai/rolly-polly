using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100.0f;
    public float health;

    public float damage = 3.0f;
    public float damageReceived;

    private PlayerController playerControllerScript;
    
    public Image blackScreen;
    private FadeToBlack blackScreenScript;

    public GameObject spawnManager;
    private SpawnManager spawnManagerScript;

    public TMP_Text gameOverText;
    private FadeToBlack gameOverTextScript;

    public Button restartButton;
    private RestartButton restartButtonScript;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        playerControllerScript = GetComponent<PlayerController>();

        blackScreenScript = blackScreen.GetComponent<FadeToBlack>();
        spawnManagerScript = spawnManager.GetComponent<SpawnManager>();
        gameOverTextScript = gameOverText.GetComponent<FadeToBlack>();
        restartButtonScript = restartButton.GetComponent<RestartButton>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (!playerControllerScript.gameOver && other.gameObject.tag == "Enemy")
        {
            damageReceived = other.GetComponent<EnemyBehaviour>().GetDamage();
            health -= damageReceived;
            FindObjectOfType<PlayerSound>().playCollisionSound();
            Destroy(other.gameObject);
            if (health < 0)
            {
                FindObjectOfType<PlayerSound>().playDeathSound();
                GameOver();
                blackScreenScript.FadeInImage(0, 0, 0);
                gameOverTextScript.FadeInText(255, 0, 0);
                spawnManagerScript.CancelInvoke("SpawnEnemy");
                restartButtonScript.GameOver();
            }
        }
    }

    public float GetHealth()
    {
        return health;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    public void GameOver()
    {
        playerControllerScript.gameOver = true;
    }
}
