using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusMove : MonoBehaviour
{
    [SerializeField] float _speed;
    //[SerializeField] GameObject _coin;
    //CoinManager coinManager; 

    private void Start()
    {
        //coinManager = FindObjectOfType<CoinManager>();    
    }

    void Update()
    { 
        coinMove();

        coinDie();
    }

    public void coinMove()
    {
        transform.position -= transform.up * Time.deltaTime * _speed;
    }

    private void coinDie()
    {
        if (transform.position.y < -4)
        {
            Destroy(gameObject);
            Progress.Instance.hp--;
        }
    }
}
