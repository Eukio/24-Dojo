using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Netcode;
using UnityEngine;

public class CardMovement : NetworkBehaviour
{
    // Start is called before the first frame update
 public Vector3 targetPos { get; set; }
   public float speed { get; set; }
    public float arcHeight = .6f;
    float timeCount;
    Vector3 startPos;
     MathDeck mathDeck;

    public int value { get; set; }

    void Start()
    {
        mathDeck = GameObject.Find("MathDeck").GetComponent<MathDeck>();

        //if()
        startPos = transform.position;
        transform.rotation = new Quaternion(180, 0, 0, 0);
        string name = gameObject.name;
        if (name.Contains("01_"))
        {
            value = 1;
        }
       else if (name.Contains("02_"))
        {
            value = 2;
        }
        else if (name.Contains("03_"))
        {
            value = 3;
        }
        else if(name.Contains("04_"))
        {
            value = 4;
        }
        else if (name.Contains("05_"))
        {
            value = 5;
        }
        else if (name.Contains("06_"))
        {
            value = 6;
        }
        else if (name.Contains("07_"))
        {
            value = 7;
        }
        else if (name.Contains("08_"))
        {
            value = 8;
        }
        else if (name.Contains("09_"))
        {
            value = 9;
        }
        else        {
            value = 10;
        }
        mathDeck.AddCardValue(value); //0
       // Debug.Log("val: "+ value+" "+gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {
        float x0 = startPos.x;
        float x1 = targetPos.x;
        float z1 = targetPos.z;
        float dist = x1 - x0;
        float nextX = Mathf.MoveTowards(transform.position.x, x1, speed * Time.deltaTime);
        float nextZ = Mathf.MoveTowards(transform.position.z, z1, speed * Time.deltaTime);
        //transform.rotation = Quaternion.Lerp(new Quaternion(181,0,0,0) , new Quaternion(0, 0, 0, 0), timeCount * .5f);
        // timeCount += Time.deltaTime;
       
        float baseY = Mathf.Lerp(startPos.y, targetPos.y, (nextX - x0) / dist);
        float arc = arcHeight * (nextX - x0) * (nextX - x1) / (-0.25f * dist * dist);
        Vector3 nextPos = new Vector3(nextX, baseY + arc, nextZ);
       
        if (nextPos.x < targetPos.x )
        {
            transform.position = nextPos;
            if(transform.position.x >x0)
            transform.Rotate(new Vector3(0, 0, 20) * Time.deltaTime * 10);

        }
        else
        {
            transform.position = targetPos;

            transform.rotation = new Quaternion(0, 0, 0, 0);

        }

    }
}
