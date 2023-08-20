using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
//using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.UI.Image;

public class Game : NetworkBehaviour
{
    // Start is called before the first frame update
    [SerializeField] List<GameObject> allCards;
    public List<GameObject> centerCards = new List<GameObject>();
    bool moveCards;
    long startTime;
    long elapsed;
    int indexTranslate;
    Vector3 positionB;
    public event EventHandler spawnCards;
    bool space;

    GameObject mathDeck;


    public List<Player> player;

    void Start()
    {
        indexTranslate = -1;
        startTime = DateTime.Now.Ticks;
         mathDeck = GameObject.Find("MathDeck");


    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner) return;
        if (Input.GetKeyDown("space"))
        {
            space = true;
        }
        if (Input.GetKeyUp("space"))
        {
            space = false;
        }
        if (moveCards)
        {
            elapsed = (DateTime.Now.Ticks - startTime) / 10000;
            FlipCardsClientRpc(positionB, indexTranslate);
            mathDeck.GetComponent<MathDeck>().AssignValueToButton(); //Move this somewhere else

        }




    }
    private void FixedUpdate()
    {
        if (space && NetworkManager.Singleton.IsServer)
        {

            RandomizeDeckServerRpc();
            space = false;
        }

    }
    private void CardsToMathDeck()
    {
        
    }
    [ClientRpc]
    private void FlipCardsClientRpc(Vector3 positionTarget, int index)
    {
        if (elapsed < 3000) //Transform
        {
            // Debug.Log("Moving: "+indexTranslate + " pos:" + positionB);
            centerCards[index].GetComponent<CardMovement>().targetPos = positionTarget;
            centerCards[index].GetComponent<CardMovement>().speed = .4f;

        }
        if (elapsed >= 3000 && indexTranslate < 3)//Create
        {
            Debug.Log("Moving: " + gameObject.name + " Speed: " + centerCards[index].GetComponent<CardMovement>().speed + " targetPos: " + centerCards[index].GetComponent<CardMovement>().targetPos);

            indexTranslate++;
            positionB = CreateMiddle(indexTranslate);
            startTime = DateTime.Now.Ticks;
            elapsed = 0;
        }
        else if (indexTranslate >= 3)
        {
            moveCards = false;
            indexTranslate = -1;
            startTime = DateTime.Now.Ticks;
            elapsed = 0;
        }



    }
    [ClientRpc]
    private void SpawnCardsClientRpc(int index)
    {
        GameObject newCard = Instantiate(allCards[index], new Vector3(-1.95799994f, 0.147451788f, -0.0209999997f), new Quaternion(0, 0, 0, 0));
        newCard.AddComponent<CardMovement>();
        centerCards.Add(newCard);
    
        allCards.Remove(allCards[index]);

    }

   
    [ServerRpc(RequireOwnership = true)]
    public void RandomizeDeckServerRpc()
    {
        for (int i = 0; i < 4; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, allCards.Count);
            SpawnCardsClientRpc(randomIndex);
        }
        indexTranslate = 0;
        positionB = CreateMiddle(indexTranslate);
        startTime = DateTime.Now.Ticks;
        moveCards = true;
    }
 
    private Vector3 CreateMiddle(int i)
    {

        Vector3 positionB = Vector3.zero;

        switch (i)
        {
            case 0:
                positionB = new Vector3(-1.717f, 0.1474519f, 0.091f);
                break;
            case 1:
                positionB = new Vector3(-1.555f, 0.1474519f, 0.091f);
                break;
            case 2:
                positionB = new Vector3(-1.717f, 0.1474519f, -0.065f);
                break;
            case 3:
                positionB = new Vector3(-1.556f, 0.1474519f, -0.065f);
                break;
            default:
                Debug.Log("Error " + i);
                positionB = Vector3.zero;
                break;
        }
       // Debug.Log(positionB);
        return positionB;
    }


}
