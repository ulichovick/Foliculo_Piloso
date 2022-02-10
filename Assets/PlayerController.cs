using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] int movementSpeed;
    [SerializeField] Rigidbody2D playerRigidBody;
    private Vector2 movementInput;
    //[SerializeField] Transform WeaponsArm;
    [SerializeField] Transform Player;
    private Camera mainCamera;
    private float torpedo;
    private Animator playerAnimator;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;

        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        movementInput.x = Input.GetAxisRaw("Horizontal");
        movementInput.y = Input.GetAxisRaw("Vertical");
        torpedo = Input.GetAxisRaw("Fire2");

        playerRigidBody.velocity = movementInput * movementSpeed;

        Vector3 mousePosition = Input.mousePosition;
        Vector3 screenPoint = mainCamera.WorldToScreenPoint(Player.localPosition);

        Vector2 offset = new Vector2(mousePosition.x - screenPoint.x, mousePosition.y - screenPoint.y);

        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;

        //WeaponsArm.rotation = Quaternion.Euler(0, 0, angle);

        if (movementInput.x < 0)
        {
            Player.localScale = new Vector3(-1f, 1f, 1f);
            //WeaponsArm.localScale = new Vector3(-1f, -1f, 1f);
        }
        else if (movementInput.x > 0)
        {
            //Player.localScale = new Vector3(1f, 1f, 0);
            Player.localScale = Vector3.one;
            //WeaponsArm.localScale = new Vector3(1f, 1f, 0);
            //WeaponsArm.localScale = Vector3.one;

        }


        if (movementInput != Vector2.zero)
        {
            playerAnimator.SetBool("isWalking", true);
        }
        else
        {
            playerAnimator.SetBool("isWalking", false);
        }


        if (movementInput != Vector2.zero & torpedo == 1)
        {
            //Player.position += new Vector3(torpedo, 0f, 0f);
            //Player.position += new Vector3(torpedo*(movementSpeed*2), 0f, 0f);
            playerRigidBody.velocity = new Vector2(torpedo * movementInput.x, 0) * (movementSpeed * 3);
            Debug.Log(playerRigidBody.velocity);
            playerAnimator.SetBool("isFlipping", true);

        }
        else
        {
            playerAnimator.SetBool("isFlipping", false);

        }

    }
}
