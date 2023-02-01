using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopUI : MonoBehaviour
{
    int shopSelectionX;
    int shopSelectionY;

    public Image selectBottomLeft;
    int healthLevel;
    int healthCost;
    int healthTickPosX;
    public TMP_Text healthText;

    public Image selectBottomRight;
    int speedLevel;
    int speedCost;
    int speedTickPosX;
    public TMP_Text speedText;

    public Image selectTopLeft;
    int damageLevel;
    int damageCost;
    int damageTickPosX;
    public TMP_Text damageText;

    public Image selectTopRight;
    int projectileLevel; 
    int projectileCost;
    int projectileTickPosX;
    public TMP_Text projectileText;
    int offset = 40;

    public GameObject upgradeTick;

    Color32 unselectedColor = new Color32(154, 154, 154, 255);
    Color32 selectionColor = new Color32(70, 170, 25, 255);

    public GameObject player;
    private PlayerController playerScript;
    private PlayerHealth playerHealth;
    private PlayerCoins playerCoins;
    public ProjectileBehaviour projectileScript;
    int coins;

    public GameObject UISounds;
    UISounds UISoundsScript;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);

        playerCoins = player.GetComponent<PlayerCoins>();
        playerScript = player.GetComponent<PlayerController>();
        playerHealth = player.GetComponent<PlayerHealth>();

        UISoundsScript = UISounds.GetComponent<UISounds>();
        
        healthLevel = 1;
        healthCost = 50;
        speedCost = 150;
        speedLevel = 1;
        damageCost = 100;
        damageLevel = 1;
        projectileCost = 100;
        projectileLevel = 1;

        shopSelectionX = 0;
        shopSelectionY = 1;

        healthTickPosX = 0;
        damageTickPosX = 0;
        speedTickPosX = 0;
        projectileTickPosX = 0;
    }

    // Update is called once per frame
    void Update()
    {
        coins = playerCoins.numCoins;
    }

    void UpdateCosts()
    {
        healthText.text = "Health: $" + healthCost.ToString();
        speedText.text = "speed: $" + speedCost.ToString();
        damageText.text = "damage $" + damageCost.ToString();
        projectileText.text = "Bullet: $" + projectileCost.ToString();
    }

    public void ShowShopUI()
    {
        gameObject.SetActive(true);
        UpdateCosts();
        UpdateSelectionColor();
    }

    public void HideShopUI()
    {
        gameObject.SetActive(false);
    }

    public void SelectionUp()
    {
        if (shopSelectionY == 0)
        {
            shopSelectionY++;
            UISoundsScript.playBeep();
            UpdateSelectionColor();
        }
    }

    public void SelectionDown()
    {
        if (shopSelectionY == 1)
        {
            shopSelectionY--;
            UISoundsScript.playBeep();
            UpdateSelectionColor();
        }
    }

    public void SelectionLeft()
    {
        if (shopSelectionX == 1)
        {
            shopSelectionX--;
            UISoundsScript.playBeep();
            UpdateSelectionColor();
        }
    }

    public void SelectionRight()
    {
        if (shopSelectionX == 0)
        {
            shopSelectionX++;
            UISoundsScript.playBeep();
            UpdateSelectionColor();
        }
    }

    public void UpdateSelectionColor()
    {
        if (shopSelectionX == 0 && shopSelectionY == 0) {
            // Bottom left
            selectBottomLeft.color = selectionColor;
            selectBottomRight.color = unselectedColor;
            selectTopLeft.color = unselectedColor;
            selectTopRight.color = unselectedColor;
        } else if (shopSelectionX == 1 && shopSelectionY == 0) {
            // Bottom right
            selectBottomLeft.color = unselectedColor;
            selectBottomRight.color = selectionColor;
            selectTopLeft.color = unselectedColor;
            selectTopRight.color = unselectedColor;
        } else if (shopSelectionX == 0 && shopSelectionY == 1) {
            // Top left 
            selectBottomLeft.color = unselectedColor;
            selectBottomRight.color = unselectedColor;
            selectTopLeft.color = selectionColor;
            selectTopRight.color = unselectedColor;
        } else {
            // Top right
            selectBottomLeft.color = unselectedColor;
            selectBottomRight.color = unselectedColor;
            selectTopLeft.color = unselectedColor;
            selectTopRight.color = selectionColor;
        }
    }

    public void SpacePressed()
    {
        Purchase(shopSelectionX, shopSelectionY);
    }
    
    void Purchase(int x, int y)
    {
        if (x == 0 && y == 1) {
            PurchaseDamage();
        } else if (x == 1 && y == 1) {
            PurchaseProjectile();
        } else if (x == 0 && y == 0) {
            PurchaseHealth();
        } else if (x == 1 && y == 0) {
            PurchaseSpeed();
        }
    }

    void PurchaseDamage()
    {
        if (damageLevel == 5)
        {
            // Indicate max level
        } else if (coins >= damageCost)
        {
            playerCoins.numCoins -= damageCost;
            damageCost += 200;
            playerScript.projectileDamage += 1.4f;
            damageLevel++;
            UpdateCosts();
            UISoundsScript.playChaChing();
            damageTickPosX += offset;

            InstantiateTick(damageTickPosX, 4);
        } else {
            // Play no money sound effect
        }
    }

    void PurchaseProjectile()
    {
        if (projectileLevel == 5)
        {
            // Indicate max level
        } else if (coins >= projectileCost)
        {
            playerCoins.numCoins -= projectileCost;
            projectileCost += 200;
            playerScript.projectileSize += 0.1f;
            playerScript.projectilePierce += 1;
            projectileLevel++;
            UpdateCosts();
            UISoundsScript.playChaChing();
            projectileTickPosX += offset;

            InstantiateTick(projectileTickPosX, 5);
        } 
    }

    void PurchaseHealth()
    {
        if (healthLevel == 5)
        {
            // Indicate max level
        } else if (coins >= healthCost)
        {
            playerCoins.numCoins -= healthCost;
            healthCost += 200;
            playerHealth.maxHealth += 100;
            playerHealth.health = playerHealth.maxHealth;
            healthLevel++;
            UpdateCosts();
            UISoundsScript.playChaChing();
            healthTickPosX += offset;

            InstantiateTick(healthTickPosX, 6);
        }
    }

    void PurchaseSpeed()
    {
        if (speedLevel == 5)
        {
            // Indicate max level
        } else if (coins >= speedCost)
        {
            playerCoins.numCoins -= speedCost;
            speedCost += 200;
            playerScript.moveSpeed -= 0.075f;
            playerScript.reloadSpeed -= 0.03f;
            speedLevel++;
            UpdateCosts();
            UISoundsScript.playChaChing();
            speedTickPosX += offset;

            InstantiateTick(speedTickPosX, 7);
        }
    }

    void InstantiateTick(float pos, int child)
    {
        var tick = Instantiate(upgradeTick, transform.position, transform.rotation);
        tick.transform.SetParent(gameObject.transform.GetChild(child));
        tick.transform.localScale = new Vector3(1, 1, 1);
        tick.transform.position = tick.transform.parent.GetChild(3).position + new Vector3(pos, 0, 0);
    }
}
