using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    public float speed = 10.0f;
    private Vector3 direction;
    public float yBoundary = 30.0f;
    public float xBoundary = 30.0f;
    public Vector3 size = new Vector3(0.4f, 0.4f, 0.4f);

    int pierce;

    private PlayerController playerScript;

    // Start is called before the first frame update
    void Start()
    {
        direction = FindObjectOfType<PlayerController>().GetProjectileDirection();

        playerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        speed = 5 / playerScript.moveSpeed;
        pierce = playerScript.projectilePierce;
        size = new Vector3(playerScript.projectileSize, playerScript.projectileSize, playerScript.projectileSize);

        transform.localScale = size;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * Time.deltaTime * speed, Space.World);

        if (transform.position.x > xBoundary || 
        transform.position.x < -xBoundary ||
        transform.position.z > yBoundary ||
        transform.position.z < -yBoundary)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            pierce--; 
            transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
            if (pierce < 1)
            {
                Destroy(gameObject);
            }
        }
    }
}
