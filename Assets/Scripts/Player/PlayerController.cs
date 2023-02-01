using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public int playerMoveDistance = 1;
    public float moveSpeed = 0.3f;
    public int xBoundary = 24;
    public int yBoundary = 24;

    public bool gameOver;

    public GameObject projectile; 
    public Vector3 projectileDirection;

    public float reloadSpeed = 1.0f;

    private float projectileCooldown;
    public float projectileDamage;
    public float projectileSize;
    public int projectilePierce;

    public GameObject shopUI;
    private ShopUI shopUIScript;

    // Inaccessable locations
    public int[][] inaccessable = new int[][]
    {
        // Shop building
        new int[] {9, 3},
        new int[] {9, 4},
        new int[] {9, 5},
        new int[] {9, 6},
        new int[] {10, 3},
        new int[] {10, 4},
        new int[] {10, 5},
        new int[] {10, 6}
    };

    public int[][] shop = new int[][]
    {
        // Shop purchase locations
        new int[] {8, 3},
        new int[] {8, 4},
        new int[] {8, 5},
        new int[] {8, 6}
    };

    float t;
    Vector3 startPosition;
    Vector3 target;
    Quaternion startRotation;
    Quaternion rollTarget;
    float timeOut;
    Vector3 direction;
    public int playerX;
    public int playerY;

    bool inShop;

    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;
        inShop = false;
        startPosition = target = transform.position;
        projectileDamage = 3.0f;
        projectileSize = 0.3f;
        projectilePierce = 1;
        shopUIScript = shopUI.GetComponent<ShopUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver)
        {
            // If player is already moving, rotate cube.
            // Otherwise, move player towards destination
            if (timeOut > 0)
            {
                timeOut -= Time.deltaTime;
                RollOnSide(direction);
            }
            else
            {
                CheckPlayerMove();
                FixPlayerRotation();
            }

            if (projectileCooldown > 0)
            {
                projectileCooldown -= Time.deltaTime;
            }
            else if (timeOut > 0)
            {
                // Cannot fire while rolling
            } 
            else if (inShop)
            {
                // In Shop Stuff
                CheckPlayerSelection();
            }
            else
            {
                CheckPlayerAttack();
            }
        }

        if (inShop && Input.GetKeyDown(KeyCode.Space))
        {
            shopUIScript.SpacePressed();
        }

        // Move player towards destination over time
        t += Time.deltaTime/moveSpeed;
        transform.position = Vector3.Lerp(startPosition, target, t);
    }

    // Set destination to for player to move towards
    void Moving(Vector3 destination, Quaternion rotation)
    {
        t = 0;
        startPosition = transform.position;
        target = destination; 
        timeOut = moveSpeed;
    }

    // Rotate player while moving
    void RollOnSide(Vector3 d)
    {
        transform.Rotate(d * 90 * Time.deltaTime/moveSpeed, Space.World);
    }

    void FixPlayerRotation()
    {
        int xRot = RoundToTen(transform.localRotation.x);
        int yRot = RoundToTen(transform.localRotation.y);
        int zRot = RoundToTen(transform.localRotation.z);

        transform.rotation = Quaternion.Euler(xRot, yRot, zRot);
    }

    int RoundToTen(float num)
    {
        return ((int)(Mathf.Round(num / 10)) * 10);
    }

    bool InArray(int[] playerLocation, int[][] place)
    {
        foreach (int[] l in place)
        {
            if (l[0] == playerLocation[0] && l[1] == playerLocation[1])
            {
                return true;
            }
        }
        return false;
    }

    void CheckPlayerMove()
    {
        if (Input.GetKey(KeyCode.W))
        {
            // Player on edge of map
            if (playerY + 1 > yBoundary || InArray(new int[]{playerX, playerY + 1}, inaccessable))
            {
                // Indicate cannot move
            } 
            else 
            {
                Moving(transform.position + new Vector3(0, 0, 1), transform.rotation * Quaternion.Euler(90, 0, 0));
                playerY++;
                direction = Vector3.right;
                FindObjectOfType<PlayerSound>().playRollSound();
                if (InArray(new int[]{playerX, playerY}, shop))
                {
                    shopUIScript.ShowShopUI();
                    inShop = true;
                } else {
                    shopUIScript.HideShopUI();
                    inShop = false;
                }
            }
            
        } 
        else if (Input.GetKey(KeyCode.A))
        {
            // Player on edge of map
            if (playerX - 1 < -(xBoundary + 1) || InArray(new int[]{playerX - 1, playerY}, inaccessable))
            {
                // Indicate cannot move
            } 
            else 
            {
                Moving(transform.position + new Vector3(-1, 0, 0), transform.rotation * Quaternion.Euler(0, 0, 90));
                playerX--;
                direction = Vector3.forward;
                FindObjectOfType<PlayerSound>().playRollSound();
                if (InArray(new int[]{playerX, playerY}, shop))
                {
                    shopUIScript.ShowShopUI();
                    inShop = true;
                } else {
                    shopUIScript.HideShopUI();
                    inShop = false;
                }
            }
        } 
        else if (Input.GetKey(KeyCode.S))
        {
            // Player on edge of map
            if (playerY - 1 < -(yBoundary + 1) || InArray(new int[]{playerX, playerY - 1}, inaccessable))
            {
                // Indicate cannot move
            } 
            else 
            {
                Moving(transform.position + new Vector3(0, 0, -1), transform.rotation * Quaternion.Euler(-90, 0, 0));
                playerY--;
                direction = Vector3.left;
                FindObjectOfType<PlayerSound>().playRollSound();
                if (InArray(new int[]{playerX, playerY}, shop))
                {
                    shopUIScript.ShowShopUI();
                    inShop = true;
                } else {
                    shopUIScript.HideShopUI();
                    inShop = false;
                }
            }
        } 
        else if (Input.GetKey(KeyCode.D))
        {
            // Player on edge of map
            if (playerX + 1 > xBoundary || InArray(new int[]{playerX + 1, playerY}, inaccessable))
            {
                // Indicate cannot move
            } 
            else 
            {
                Moving(transform.position + new Vector3(1, 0, 0), transform.rotation * Quaternion.Euler(0, 0, -90));
                playerX++;
                direction = Vector3.back;
                FindObjectOfType<PlayerSound>().playRollSound();
                if (InArray(new int[]{playerX, playerY}, shop))
                {
                    shopUIScript.ShowShopUI();
                    inShop = true;
                } else {
                    shopUIScript.HideShopUI();
                    inShop = false;
                }
            }
        }
    }

    void CheckPlayerAttack()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            projectileDirection = Vector3.forward;
            Instantiate(projectile, transform.position, transform.rotation);
            projectileCooldown = reloadSpeed;
            FindObjectOfType<PlayerSound>().playShootSound();
        } 
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            projectileDirection = Vector3.left;
            Instantiate(projectile, transform.position, transform.rotation);
            projectileCooldown = reloadSpeed;
            FindObjectOfType<PlayerSound>().playShootSound();
        } 
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            projectileDirection = Vector3.back;
            Instantiate(projectile, transform.position, transform.rotation);
            projectileCooldown = reloadSpeed;
            FindObjectOfType<PlayerSound>().playShootSound();
        } 
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            projectileDirection = Vector3.right;
            Instantiate(projectile, transform.position, transform.rotation);
            projectileCooldown = reloadSpeed;
            FindObjectOfType<PlayerSound>().playShootSound();
        }
    }

    void CheckPlayerSelection()
    {
        if (Input.GetKey(KeyCode.UpArrow)) {
            shopUIScript.SelectionUp();
        } else if (Input.GetKey(KeyCode.LeftArrow)) {
            shopUIScript.SelectionLeft();
        } else if (Input.GetKey(KeyCode.DownArrow)) {
            shopUIScript.SelectionDown();
        } else if (Input.GetKey(KeyCode.RightArrow)) {
            shopUIScript.SelectionRight();
        }
    }

    public Vector3 GetProjectileDirection()
    {
        return projectileDirection;
    }

    public float GetProjectileDamage()
    {
        return projectileDamage;
    }
}
