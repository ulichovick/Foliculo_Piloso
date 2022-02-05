using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int movementSpeed;
    [SerializeField] Rigidbody2D playerRigidBody;
    private Vector2 movementInput;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movementInput.x = Input.GetAxisRaw("Horizontal");
        movementInput.y = Input.GetAxisRaw("Vertical");

        //transform.position += new Vector3(movementInput.x, movementInput.y, 0f)* movementSpeed * Time.deltaTime;

        playerRigidBody.velocity = movementInput * movementSpeed;

    }
}
