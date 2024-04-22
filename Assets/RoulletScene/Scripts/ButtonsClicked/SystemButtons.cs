using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SystemButtons : MonoBehaviour
{
    [SerializeField] Button buttonBack;

    private void Start()
    {
        buttonBack.onClick.AddListener(() => OnButtonClick("ButtonBack"));
    }

    void OnButtonClick(string buttonName)
    {
        switch (buttonName)
        {
            case "ButtonBack":
                //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
                SceneManager.LoadScene("MellstroyMain");
                break;
        }
    }
}
