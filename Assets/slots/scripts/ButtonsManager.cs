using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonsManager : MonoBehaviour
{
    [SerializeField] SlotsMachine _slotsMachine;
    balance balance;
    [SerializeField] AudioSource _startSound;
    [SerializeField] AudioSource _failSound;

    void Start()
    {
        balance = FindAnyObjectByType<balance>();
    }

    public void StartButton()
    {
        if (balance.Bet != -1 && balance.Balance >= balance.Bet)
        {
            FindAnyObjectByType<InputField>().interactable = false;
            if (_slotsMachine.isEnd)
            {
                _startSound.Play();
                balance.UpdateBalance(-balance.Bet);
                _slotsMachine.StartSpin = true;
                _slotsMachine.StopSpin = false;
            }
        }
        else
        {
            _failSound.Play();
        }
    }

    public void StopButton()
    {
        if (_slotsMachine.StartSpin)
        {
            _startSound.pitch = -10;
            _slotsMachine.StartSpin = false;
            _slotsMachine.StopSpin = true;
            _slotsMachine.isEnd = false;
        }
    }

    public void ExitButton()
    {
        SceneManager.LoadScene("MellstroyMain");
    }
}
