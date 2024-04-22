using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusTrigger : MonoBehaviour
{
    [SerializeField] string bonusName;
    CoinManager coinManager;


    void Start()
    {
        coinManager = FindObjectOfType<CoinManager>();
        Debug.Log("coin manager найден");
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            switch (bonusName)
            {
                case "coin": 
                    coinManager.addCoins(1);
                    Debug.Log("Coin picked");
                    Destroy(gameObject);
                    break;

                case "coin1":
                    coinManager.addCoins(3);
                    Destroy(gameObject);
                    break;

                case "coin2":
                    coinManager.addCoins(5);
                    Destroy(gameObject);
                    break;

                case "coin3":
                    coinManager.addCoins(10);
                    Destroy(gameObject);
                    break;

                case "coin4":
                    coinManager.addCoins(15);
                    Destroy(gameObject);
                    break;

                case "coin5":
                    coinManager.addCoins(30);
                    Destroy(gameObject);
                    break;

                case "coin6":
                    coinManager.addCoins(100);
                    Destroy(gameObject);
                    break;
            }
        }
    }
}
