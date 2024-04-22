//using System.Collections;
//using System.Collections.Generic;
//using TMPro;
//using UnityEngine;
//using UnityEngine.UI;

//public class ButtonClickListener : MonoBehaviour
//{
//    public bool isClicked;
//    //[SerializeField] TextMeshProUGUI balanceText;
//    [SerializeField] InputField betInputTextView;
//    [SerializeField] Button buttonClicked;

    


//    private void FixedUpdate()
//    {

//        isBetTextEmpty();
//    }

//    public void isButtonClicked()
//    {
//        int bet = int.Parse(betInputTextView.text.ToString());
//        Debug.Log("Button " + gameObject.name + " clicked");
 
//        FortunetWheel.setBetByColor(bet, "red");
//    }

//    private bool isBetTextEmpty()
//    {
//        if (string.IsNullOrEmpty(betInputTextView.text))
//        {
//            buttonClicked.enabled = false;
//            return true;
//        }
//        else
//        {
//            buttonClicked.enabled = true;
//            return false;
//        }
//    }


//}
