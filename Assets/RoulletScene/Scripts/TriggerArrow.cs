using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerArrow : MonoBehaviour
{
    private static string lastColor;
    private void OnTriggerEnter2D(Collider2D other)
    {
        lastColor = other.gameObject.name;
    }

    public static string getWinColor()
    {
        return lastColor;
    }
}
