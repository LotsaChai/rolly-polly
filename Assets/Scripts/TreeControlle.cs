using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeControlle : MonoBehaviour
{
    // Put the tree prefabs here. They will be chosen by random
    public List<GameObject> treePrefabs = new List<GameObject>();
    public int treeNumber = 64;
    public float treeAreaWidth = 5;
    public float treeAreaHeight = 5;
    public string interactionTag = "Player"; // Tag objects with this string, that you want to interact with the gras

    private Vector4[] treeInteractionPositions = new Vector4[4];
    private Transform ground;
    private List<GameObject> tree = new List<GameObject>();

    void Awake () {
        ground = transform;
        float groundWidthHalf = treeAreaWidth / 2;
        float groundDepthHalf = treeAreaHeight / 2;

        // Create some tree at random positions in given area
        for (int treeIndex = 0; treeIndex < treeNumber; treeIndex++) {
            Vector3 position = transform.position + new Vector3(Random.Range(-groundWidthHalf, groundWidthHalf), 0, Random.Range(-groundDepthHalf, groundDepthHalf));
            GameObject newtree = Instantiate(treePrefabs[Random.Range(0, treePrefabs.Count)], position, Quaternion.Euler(0, Random.Range(0, 360), 0), ground.transform);
            // newtree.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            tree.Add(newtree);
        }
    }
}
