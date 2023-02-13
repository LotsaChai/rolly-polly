using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitlePlayerController : MonoBehaviour
{
    int playerMoveDistance = 1;
    public float moveSpeed = 0.5f;
    int xBoundary = 8;
    int yBoundary = 5;

    float t;
    Vector3 startPosition;
    Vector3 target;
    Vector3 direction;
    float timeOut;
    int playerX;
    int playerY;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = target = transform.position;
    }

    // Update is called once per frame
    void Update()
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

    void CheckPlayerMove()
    {
        if (Input.GetKey(KeyCode.W))
        {
            Debug.Log("y"+playerY);
            Debug.Log("b"+yBoundary);
            // Player on edge of map
            if (playerY + 1 > yBoundary)
            {
                // Indicate cannot move
                Debug.Log("happening");
            } 
            else 
            {
                Moving(transform.position + new Vector3(0, 0, playerMoveDistance), transform.rotation * Quaternion.Euler(90, 0, 0));
                playerY++;
                direction = Vector3.right;
                FindObjectOfType<TitlePlayerSound>().playRollSound();
            }
            
        } 
        else if (Input.GetKey(KeyCode.A))
        {
            // Player on edge of map
            if (playerX - 1 < -xBoundary)
            {
                // Indicate cannot move
            } 
            else 
            {
                Moving(transform.position + new Vector3(-playerMoveDistance, 0, 0), transform.rotation * Quaternion.Euler(0, 0, 90));
                playerX--;
                direction = Vector3.forward;
                FindObjectOfType<TitlePlayerSound>().playRollSound();
            }
        } 
        else if (Input.GetKey(KeyCode.S))
        {
            // Player on edge of map
            if (playerY - 1 < -yBoundary)
            {
                // Indicate cannot move
            } 
            else 
            {
                Moving(transform.position + new Vector3(0, 0, -playerMoveDistance), transform.rotation * Quaternion.Euler(-90, 0, 0));
                playerY--;
                direction = Vector3.left;
                FindObjectOfType<TitlePlayerSound>().playRollSound();
            }
        } 
        else if (Input.GetKey(KeyCode.D))
        {
            // Player on edge of map
            if (playerX + 1 > xBoundary)
            {
                // Indicate cannot move
            } 
            else 
            {
                Moving(transform.position + new Vector3(playerMoveDistance, 0, 0), transform.rotation * Quaternion.Euler(0, 0, -90));
                playerX++;
                direction = Vector3.back;
                FindObjectOfType<TitlePlayerSound>().playRollSound();
            }
        }
    }
}
