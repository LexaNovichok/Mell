using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] Button buttonRed;
    [SerializeField] Button buttonGreen;
    [SerializeField] Button buttonBlack;
    [SerializeField] Button buttonSpin;

    [SerializeField] Text betRedText;
    [SerializeField] Text betGreenText;
    [SerializeField] Text betBlackText;


    [SerializeField] TMP_InputField betInputTextView;
    [SerializeField] Button[] buttonsBetList;


    public static event Action buttonSpinClicked;

    [SerializeField] AudioSource bet_sound;


    private void OnEnable()
    {
        FortunetWheel.wheelStopped += clearBettedText;
        FortunetWheel.wheelStopped += enableBetButtons;
    }

    private void OnDisable()
    {
        FortunetWheel.wheelStopped -= clearBettedText;
        FortunetWheel.wheelStopped -= enableBetButtons;
    }

    
    void Start()
    {
        buttonRed.onClick.AddListener(() => OnButtonClick("red"));
        buttonGreen.onClick.AddListener(() => OnButtonClick("green"));
        buttonBlack.onClick.AddListener(() => OnButtonClick("black"));
        buttonSpin.onClick.AddListener(() => OnButtonClick("spin"));
    }

    private void FixedUpdate()
    {
        isBetTextEmpty();

    }

    void OnButtonClick(string buttonName)
    {
        Debug.Log("Button clicked: " + buttonName);
        int bet = 0;

        try
        {
            bet = int.Parse(betInputTextView.text.ToString());
        } catch (Exception e)
        {
            Debug.Log(e.Message);
        }


        switch (buttonName)
        {
            case "red":
                if (bet <= Progress.Instance.balance)
                {
                    FortunetWheel.setBetByColor(bet, "red");

                    bet_sound.Play();

                    int bettedToRed = int.Parse(betRedText.text.ToString());
                    betRedText.text = (bettedToRed + bet).ToString();
                }
                break;

            case "green":
                if (bet <= Progress.Instance.balance)
                {
                    FortunetWheel.setBetByColor(bet, "green");

                    bet_sound.Play();

                    int bettedToGreen = int.Parse(betGreenText.text.ToString());
                    betGreenText.text = (bettedToGreen + bet).ToString();
                }
                break;

            case "black":
                if (bet <= Progress.Instance.balance)
                {
                    FortunetWheel.setBetByColor(bet, "black");

                    bet_sound.Play();

                    int bettedToBlack = int.Parse(betBlackText.text.ToString());
                    betBlackText.text = (bettedToBlack + bet).ToString();
                }
                break;

            case "spin":
                buttonSpinClicked?.Invoke();
                Debug.Log("Button spin");
                for (int i = 0; i < buttonsBetList.Length; i++)
                {
                    buttonsBetList[i].enabled = false;
                }

                
                break;

        }

    }

    private bool isBetTextEmpty()
    {
        if (string.IsNullOrEmpty(betInputTextView.text))
        {
            Array.ForEach(buttonsBetList, button => button.enabled = false);
            return true;
        }
        else
        {
            Array.ForEach(buttonsBetList, button => button.enabled = true);
            return false;
        }
    }

    private void clearBettedText()
    {
        betRedText.text = "0";
        betGreenText.text = "0";
        betBlackText.text = "0";
    }

    private void enableBetButtons()
    {
        for (int i = 0; i < buttonsBetList.Length; i++)
        {
            buttonsBetList[i].enabled = true;
        }
    }

  
}
