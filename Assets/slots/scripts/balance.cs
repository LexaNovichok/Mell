using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class balance : MonoBehaviour
{
    public int Balance;
    [SerializeField] TextMeshProUGUI _balanceText;
    public int Bet = -1;

    void Start()
    {
        
        UpdateBalance(0);
    }

    public void UpdateBalance(int value)
    {
        Progress.Instance.balance += value;
        _balanceText.text = Progress.Instance.balance.ToString();
    }

    public void ChangeBet(string bet) 
    {
        bool isInt = int.TryParse(bet, out Bet);
        if (isInt)
        {
            if (Bet < 0) Bet *= -1;
            if (Bet > Balance) Bet = -1;
            if (Bet == 0) Bet = -1;
        }
        else
        {
            Bet = -1;
        }
        Debug.Log(Bet);
    }


}
