using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerCoins : MonoBehaviour
{
    public TMP_Text numCoinsText;
    public int numCoins;

    // Start is called before the first frame update
    void Start()
    {
        numCoins = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // Update coin counter
        numCoinsText.text = "MONEY: $" + numCoins.ToString();
    }

    public void UpdateCoins(int value)
    {
        numCoins += value;
    }
}
