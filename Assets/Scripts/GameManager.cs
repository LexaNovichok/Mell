using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject _startMenu;
    [SerializeField] GameObject _restartMenu;
    [SerializeField] GameObject _spawner;
    [SerializeField] PlayerController _playerController;

    private BonusMove bonusMove;

    public static event Action restartGame;

    void Start()
    {
        var bonusObjects = FindObjectsOfType<BonusMove>();
        foreach (var bonusObject in bonusObjects)
        {
            Destroy(bonusObject.gameObject);
        }

        menuStart();

    }


    private void OnEnable()
    {
        PlayerBehaviour.isDead += restartMenu;
    }

    public void menuStart()
    {
        _startMenu.SetActive(true);
        _spawner.SetActive(false);
        _playerController.enabled = false;
    }

    public void restartMenu()
    {
        _restartMenu.SetActive(true);
        _spawner.SetActive(false);
        _playerController.enabled = false;

        var bonusObjects = FindObjectsOfType<BonusMove>();
        foreach (var bonusObject in bonusObjects)
        {
            Destroy(bonusObject.gameObject);
        }
    }

    public void onButtonStartClicked()
    {
        _startMenu.SetActive(false);
        _restartMenu.SetActive(false);
        _spawner.SetActive(true);
        _playerController.enabled = true;

        Debug.Log("On button start clicked");

        Progress.Instance.hp = 3;
        restartGame?.Invoke();
    }

    public void onButtonSlotsClicked()
    {
        SceneManager.LoadScene("slotsPref");
    }

    public void onButtonRouletteClicked()
    {
        SceneManager.LoadScene("Roullet");
    }




}
