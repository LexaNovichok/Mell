using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinTrigger : MonoBehaviour
{
    CoinManager coinManager;

    void Start()
    {
        coinManager = FindObjectOfType<CoinManager>();
        Debug.Log(coinManager + "найден");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMove player = FindObjectOfType<PlayerMove>();
        
        if (player != null)
        {
            coinManager.addCoins(1);
            gameObject.SetActive(false);
            Debug.Log("Coin trigger");
        }
    }
}
