using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;



public class FortunetWheel : MonoBehaviour
{
    private int totalAngle;
    [SerializeField] private string[] prizeText;
    [SerializeField] private int section;
    [SerializeField] private int angleSpeed;
    [SerializeField] private int minTime;
    [SerializeField] private int maxTime;

    private static int balance;
    [SerializeField] TextMeshProUGUI balanceText; 
    private static int[] betList = { 0, 0, 0 }; //reg-green-black

    private bool isWheelTurning = false; //может ли вращать колесо
    //public static bool buttonTurnClicked = false; 

    public static event Action wheelStopped;

    private string winColor;

    //Audio
    [SerializeField] AudioSource spin_wheel_sound;
    [SerializeField] AudioSource lose_sound;
    [SerializeField] AudioSource win_sound;
    [SerializeField] AudioSource bg_sound;
 


    private void OnDestroy()
    {
        Debug.Log("Fortunate wheel destroyed");
    }

    private void Awake()
    {
        //DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        //balance = Progress.Instance.balance;
        balance = Progress.Instance.balance;
        balanceText.text = balance.ToString();

        ButtonManager.buttonSpinClicked += spinWheel;


        totalAngle = 360 / section;

        bg_sound.Play();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        updateBalanceText();


    }


  

    public static void setBetByColor(int bet, string color)
    {
        switch (color)
        {
            case "red":
                //Progress.Instance.balance -= bet;
                Progress.Instance.balance -= bet;

                betList[0] += bet;
                break;


            case "green":
                //Progress.Instance.balance -= bet;
                Progress.Instance.balance -= bet;

                betList[1] += bet;
                break;


            case "black":
                //Progress.Instance.balance -= bet;
                Progress.Instance.balance -= bet;

                betList[2] += bet;
                break;
        }
        Debug.Log(" betted to " + betList[0] + " " + betList[1] + " " + betList[2]);
    }


    private void spinWheel() //прокрут колеса с проверкой на то поставлено ли что-то
    {
        Debug.Log("spin wheel");
        if (betList.Any(bet => bet != 0) && !isWheelTurning)
        {
            //Spin
            //StartCoroutine(TurnTheWheel());
            Debug.Log("can spin wheel");
        }
    }

    private IEnumerator TurnTheWheel()
    {
        //Audio
        spin_wheel_sound.Play();

        int randTime = UnityEngine.Random.Range(minTime, maxTime);
        isWheelTurning = true;
        float angle = UnityEngine.Random.Range(angleSpeed, angleSpeed * 2);
        for (int i = 0; i < randTime; i++)
        {
            transform.Rotate(0, 0, totalAngle / angle);
            yield return new WaitForSeconds(0.01f);
        }

        winColor = TriggerArrow.getWinColor();
        Debug.Log(winColor);


        //получение выигрыша
        gameResult();


        //обнуление ставок
        for (int i = 0; i < betList.Length; i++)
        {
            betList[i] = 0;
        }
        //Audio
        spin_wheel_sound.Stop();

        wheelStopped?.Invoke();
        isWheelTurning = false;
    }

    private void updateBalanceText()
    {
        //balanceText.text = (Progress.Instance.balance).ToString();

        balanceText.text = Progress.Instance.balance.ToString();
    }

    private void gameResult()
    {
        int moneyWon = 0;
        switch(winColor)
        {
            case "red":
                moneyWon += betList[0] * 2;
                break;
            case "green":
                moneyWon += betList[1] * 36;
                break;
            case "black":
                moneyWon += betList[2] * 2;
                break;
        }
        
        if (moneyWon == 0)
        {
            lose_sound.Play();
        } else
        {
            win_sound.Play();
        }

        Progress.Instance.balance += moneyWon;
    }

}
