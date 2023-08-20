using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Player : NetworkBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float movementSpeed = 7f;
    [SerializeField] private float rotationSpeed = 500f;
    [SerializeField] private float positionrange = 3f;
    void Start()
    {
        
    }
    public override void OnNetworkSpawn()
    {
        transform.position = new Vector3(Random.RandomRange(positionrange, -positionrange), 0, Random.RandomRange(positionrange, -positionrange));
        transform.rotation = new Quaternion(0, 180, 0, 0);
    }
    // Update is called once per frame
    void Update()
    {
        if(!IsOwner) return;
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontalInput,0, verticalInput);
        movementDirection.Normalize();
        transform.Translate(movementDirection*movementSpeed*Time.deltaTime,Space.World);
        if(movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation,toRotation,rotationSpeed*Time.deltaTime);
        }
    }
}
