using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public static event Action isDead;
    [SerializeField] AudioSource _beforeDeadSound;
    [SerializeField] AudioSource _pickCoinsAudio;
    [SerializeField] AudioSource _pickExtraCoinsAudio;


    private void Start()
    {
        //DontDestroyOnLoad(gameObject);
    }


    private void OnEnable()
    {
        Hp.isMustDie += Die;
    }

    private void OnDisable()
    {
        Hp.isMustDie -= Die;
    }


    public void Die()
    {
        gameObject.SetActive(false);

        _beforeDeadSound.Play();

        isDead?.Invoke();

        Debug.Log("Player dead");
        Debug.Log("Balance: " + Progress.Instance.balance);

        //GameManager.restartGame += playerAlive;
        
    }

    //public void playerAlive()
    //{
    //    gameObject.SetActive(true);
    //    Debug.Log("Player alive");
    //}

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Player trigger");
        BonusTrigger coin = other.GetComponent<BonusTrigger>();

        if (coin != null)
        {
            _pickCoinsAudio.Play();
            Debug.Log("Sound coin picked");
        }
    }
}
