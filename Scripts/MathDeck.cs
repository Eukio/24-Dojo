using JetBrains.Annotations;
using OpenCover.Framework.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static System.Net.Mime.MediaTypeNames;

public class MathDeck : NetworkBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public List<int> cardValues;
    [SerializeField] public List<CardButton> cardButtons;
    CardButton button1;
    CardButton button2;
    CardButton button3;
     CardButton button4;
    CardButton firstCardSelected;
    String operation;
    CardButton secondCardSelected;
    TextMeshProUGUI total;



    //assign value of card
    //when card selected (change color) wait for operation
    //card selected twice now, remove first card button and apply operation to that card
    //if there is one card left, and that card is = 24, then submit response success to client and redraw
    void Start()
    {

        cardValues = new List<int>();
        button1 = GameObject.Find("Number1").GetComponent<CardButton>();
        button2 = GameObject.Find("Number2").GetComponent<CardButton>();
        button3 = GameObject.Find("Number3").GetComponent<CardButton>();
        button4 = GameObject.Find("Number4").GetComponent<CardButton>();
        cardButtons.Add(button1);
        cardButtons.Add(button2);
        cardButtons.Add(button3);
        cardButtons.Add(button4);

        for(int i = 0; i < cardButtons.Count; i++)
        {
            cardButtons[i].gameObject.SetActive(true);
        }
        firstCardSelected = null;
        secondCardSelected = null;
        operation = "";


    }

    // Update is called once per frame
    void Update()
    {
        //  if(answerField != null) 
        //  answerField.text = answerText;
        if (cardValues.Count > 0)
        {
            for (int i = 0; i < cardButtons.Count; i++)
            {
                if (firstCardSelected == null && operation.Equals("")&& cardButtons[i].selected)
                {
               
                    firstCardSelected = cardButtons[i];
                    firstCardSelected.GetComponentInChildren<TextMeshProUGUI>().color = Color.red;
                    
                 //   Debug.Log("Pressed First Value: " + firstCardSelected.value);
                }
                else if(firstCardSelected != null && operation.Equals("") && cardButtons[i].selected)
                {
                        firstCardSelected.GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
                        firstCardSelected.selected = false;
                    firstCardSelected = cardButtons[i];
                    firstCardSelected.GetComponentInChildren<TextMeshProUGUI>().color = Color.red;

                 //   Debug.Log("Changed and Pressed First Value: " + firstCardSelected.value);
                }
                else if (firstCardSelected != null && !firstCardSelected.Equals(cardButtons[i]) && cardButtons[i].selected && operation != "")
                {
                    secondCardSelected = cardButtons[i];
               //     Debug.Log("Pressed Second Value: " + secondCardSelected.value);
                    firstCardSelected.GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
                    // secondCardSelected "" + Calculate(operation, firstCardSelected, secondCardSelected);
                    double calculatedAnswer = Calculate(operation, firstCardSelected, secondCardSelected);
                    Debug.Log("Answer " + calculatedAnswer + "text: ");
                    //Correct answer but
                    secondCardSelected.gameObject.GetComponentInChildren<TextMeshProUGUI>().text =""+calculatedAnswer;
                   
                    firstCardSelected.gameObject.SetActive(false);
                    firstCardSelected.selected = false;
                    secondCardSelected.selected = false;
                    firstCardSelected = null;
                    secondCardSelected = null;
                    operation = "";
                }
            }

        }
    }
   
    public void OnClickSubmit()
    {
        Debug.Log("Card Values total: "+cardValues.Count);

        for (int i = 0; i < cardValues.Count; i++)
        {
            Debug.Log(cardValues[i]);
        }

    }
    
    public void AssignValueToButton()
    {
        button1.GetComponentInChildren<TextMeshProUGUI>().text = ""+cardValues[0];
        button2.GetComponentInChildren<TextMeshProUGUI>().text = "" + cardValues[1];
        button3.GetComponentInChildren<TextMeshProUGUI>().text = "" + cardValues[2];
        button4.GetComponentInChildren<TextMeshProUGUI>().text = "" + cardValues[3];

    }
    public double Calculate(String operand, CardButton firstCard, CardButton secondCard) { 
        double firstValue = Int32.Parse(firstCard.value);
        double secondValue = Int32.Parse(secondCard.value);
        Debug.Log("firstVal: " + firstValue);
        Debug.Log("secondVal: " + secondValue);

        double finalValue;
        if (operand.Equals("+"))
        {
            finalValue = firstValue + secondValue;
            Debug.Log("Cards Equals: " + finalValue);
            return finalValue;
        }
        else if (operand.Equals("-"))
        {
            finalValue = firstValue - secondValue;
            Debug.Log("Cards Equals: " + finalValue);
            return finalValue;
        }
       else if (operand.Equals("*"))
        {
            finalValue = firstValue * secondValue;
            Debug.Log("Cards Equals: " + finalValue);
            return finalValue;
        }
        else if (operand.Equals("/"))
        {
            finalValue = firstValue / secondValue;
            Debug.Log("Cards Equals: " + finalValue);
            return finalValue;
        }
      else if (operand.Equals("log"))
        {
            finalValue = Math.Log(firstValue, secondValue); ;
            Debug.Log("Cards Equals: " + finalValue);
            return finalValue;
        }
        else if (operand.Equals("!"))
        {

            if (firstValue % 1 == 0)
            {
                finalValue = 1;
                for (int i = (int)firstValue; i > 0; i--)
                {
                    finalValue *= firstValue;
                }
                Debug.Log("Cards Equals: " + finalValue);
                firstCardSelected.selected = false;
                firstCardSelected = null;
            }
            else
            {
                Debug.Log("Cards Equals: Error");

                return -1;

            }
        }
        else if (operand.Equals("^"))
        {
            finalValue = Math.Pow(firstValue, secondValue); ;
            Debug.Log("Cards Equals: " + finalValue);
            return finalValue;
        }
        else if (operand.Equals("rt"))
        {
            finalValue = Math.Pow(firstValue, secondValue); ;
            Debug.Log("Cards Equals: " + finalValue);
            return finalValue;
        }
        operation = "";
        return -1;
    }
    public void OnClickSkip()
    {
    }
    public void OnClickClear()
    {
        for (int i = 0; i < cardButtons.Count; i++)
        {
            cardButtons[i].value = cardValues[i] +"";
            cardButtons[i].gameObject.SetActive(true);
        }
    }
    public void OnClickAdd()
    {
        if(firstCardSelected)
        operation = "+";
    }
    public void OnClickSubtract()
    {
        if (firstCardSelected)

            operation = "-";

    }
    public void OnClickMultiply()
    {
        if (firstCardSelected)

            operation = "*";

    }
    public void OnClickDivide()
    {
        if (firstCardSelected)

            operation = "/";

    }

    public void OnClickLog()
    {
        if (firstCardSelected)

            operation = "log";

    }
    public void OnClickFactorial()
    {
        if (firstCardSelected)

            operation = "!";

    }
    public void OnClickExponent()
    {
        if (firstCardSelected)

            operation = "^";

    }
    public void OnClickSqrt()
    {
        if (firstCardSelected)

            operation = "sqrt";

    }
   
    public void AddCardValue(int value)
    {
        cardValues.Add(value);
    }

}
