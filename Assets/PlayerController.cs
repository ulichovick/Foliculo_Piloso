using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int movementSpeed;
    [SerializeField] Rigidbody2D playerRigidBody;
    private Vector2 movementInput;

    [SerializeField] Transform WeaponsArm;

    [SerializeField] Transform Player;

    private Camera mainCamera;
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        movementInput.x = Input.GetAxisRaw("Horizontal");
        movementInput.y = Input.GetAxisRaw("Vertical");

        playerRigidBody.velocity = movementInput * movementSpeed;

        Vector3 mousePosition = Input.mousePosition;
        Vector3 screenPoint = mainCamera.WorldToScreenPoint(Player.localPosition);

        Vector2 offset = new Vector2(mousePosition.x - screenPoint.x, mousePosition.y - screenPoint.y);

        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;

        WeaponsArm.rotation = Quaternion.Euler(0, 0, angle);

        if (mousePosition.x < screenPoint.x)
        {
            Player.localScale = new Vector3(-1f, 1f, 1f);
            WeaponsArm.localScale = new Vector3(-1f, -1f, 1f);
        }
        else if(mousePosition.x > screenPoint.y)
        {
            //Player.localScale = new Vector3(1f, 1f, 0);
            Player.localScale = Vector3.one;
            //WeaponsArm.localScale = new Vector3(1f, 1f, 0);
            WeaponsArm.localScale = Vector3.one;

        }

    }
}
