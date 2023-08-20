using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardButton : MonoBehaviour
{
    // Start is called before the first frame update
    public  bool selected { set; get; }
    public string value { set; get; }

    void Start()
    {
        selected = false;
    }

    // Update is called once per frame
    void Update()
    {
        value = GetComponentInChildren<TextMeshProUGUI>().text;


    }
    public void OnClickCardSelected()
    {
         selected = true;
    }
}
