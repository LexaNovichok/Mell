using System;
using UnityEngine;
using UnityEngine.UI;

public class Hp : MonoBehaviour
{

    [SerializeField] SpriteRenderer[] hp;
    public static event Action isMustDie;

    public static event Action minusHp;

    private void Start()
    {
 
    }
    void OnEnable()
    {
        GameManager.restartGame += makeEnableHp;
        //GameManager.backToScene += makeEnableHp;
    }

    private void OnDisable()
    {
        GameManager.restartGame -= makeEnableHp;
    }

    void Update()
    {
        if (Progress.Instance.hp == 2)
        {
            hp[2].enabled = false;
            minusHp?.Invoke();
            Debug.Log("Hp == 2");
        } 
        else if (Progress.Instance.hp == 1)
        {
            hp[1].enabled = false;
            minusHp?.Invoke();
            Debug.Log("Hp == 1");
        }
        else if (Progress.Instance.hp == 0)
        {
            hp[0].enabled = false;
            minusHp?.Invoke();
            Debug.Log("Hp == 0");
            isMustDie?.Invoke();
        }
    }

    public void makeEnableHp()
    {
        hp[0].enabled = true;
        hp[1].enabled = true;
        hp[2].enabled = true;
    }

  
}
