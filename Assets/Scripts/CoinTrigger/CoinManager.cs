using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _text;
    [SerializeField] TextMeshProUGUI _textResultPerRound;
    [SerializeField] TextMeshProUGUI _textBalance;
    public bool isMultipleMellPicked { get; set; }
    public bool isHighSpeedPicked { get; set; }



    public void Start()
    {
     
        _text.text = Progress.Instance.coinsCounter.ToString();
    }

    private void OnEnable()
    {
        PlayerBehaviour.isDead += resultPerRound;

        GameManager.restartGame += updateTextCoinCounter;
    }

    public void addCoins(int numCoins)
    {
  
        Progress.Instance.coinsCounter += numCoins;
        _text.text = Progress.Instance.coinsCounter.ToString();

        Progress.Instance.balance += numCoins;
    }



    //public void spendCoins(int value)
    //{
    //    NumberOfCoins -= value;
    //    _text.text = NumberOfCoins.ToString();
    //}


    private void resultPerRound()
    {
        _textResultPerRound.text = Progress.Instance.coinsCounter.ToString();
        Progress.Instance.coinsCounter = 0;
       

        _textBalance.text = Progress.Instance.balance.ToString();
    }

    private void updateTextCoinCounter()
    {
        _text.text = Progress.Instance.coinsCounter.ToString();
    }
}
