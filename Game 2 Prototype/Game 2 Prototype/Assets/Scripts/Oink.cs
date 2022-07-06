using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Oink : MonoBehaviour
{
    public Text money;
    private int moneyAmount;

    private void Start()
    {
        moneyAmount = 0;
    }

    private void Update()
    {
        money.text = moneyAmount.ToString();
    }

    public void AddMoney()
    {
        moneyAmount += 1;
    }
    public void PlayOink()
    {
        GetComponent<AudioSource>().Play();
    }
}


