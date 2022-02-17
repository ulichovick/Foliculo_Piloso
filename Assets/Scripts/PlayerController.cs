using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] int movementSpeed;
    [SerializeField] Rigidbody2D playerRigidBody;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform firePoint;
    private Vector2 movementInput;
    [SerializeField] float timeBetweenShots;
    private float shotCounter;
    public float boost = 10f;
    public float actionCooldown = 0.31f;
    bool canPress = true;
    float timeSinceAction = 0.0f;
    //[SerializeField] Transform WeaponsArm;
    [SerializeField] Transform Player;
    //private Camera mainCamera;
    private float torpedo;
    private Animator playerAnimator;
    // Start is called before the first frame update
    void Start()
    {
        //mainCamera = Camera.main;
        playerAnimator = GetComponent<Animator>();
        shotCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        movementInput.x = Input.GetAxisRaw("Horizontal");
        movementInput.y = Input.GetAxisRaw("Vertical");
        if (canPress)
        {
            torpedo = Input.GetAxisRaw("Fire2");
            StartCoroutine(CoolDown());
        }
        movementInput.Normalize();
        playerRigidBody.velocity = movementInput * movementSpeed;

        //Vector3 mousePosition = Input.mousePosition;
        //Vector3 screenPoint = mainCamera.WorldToScreenPoint(Player.localPosition);
        //Vector2 offset = new Vector2(mousePosition.x - screenPoint.x, mousePosition.y - screenPoint.y);
        //float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        //WeaponsArm.rotation = Quaternion.Euler(0, 0, angle);

        if (movementInput.x < 0)
        {
            //Player.localScale = new Vector3(-1f, 1f, 1f);
            Player.rotation = Quaternion.Euler(0, 180, 0);
            //WeaponsArm.localScale = new Vector3(-1f, -1f, 1f);
        }
        else if (movementInput.x > 0)
        {
            //Player.localScale = new Vector3(1f, 1f, 0);
            //Player.localScale = Vector3.one;
            Player.rotation = Quaternion.Euler(0, 0, 0);
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
        Debug.Log(timeSinceAction);
        if (movementInput != Vector2.zero && torpedo == 1)
        {
            if (timeSinceAction < actionCooldown)
            {
                //Player.position += new Vector3(torpedo, 0f, 0f);
                //Player.position += new Vector3(torpedo*(movementSpeed*2), 0f, 0f);
                //playerRigidBody.velocity = new Vector2(movementInput.x, 0) * (movementSpeed * 3);
                playerRigidBody.AddForce(new Vector3(movementInput.x, 0, 0) * boost, ForceMode2D.Impulse);
                playerAnimator.SetBool("isFlipping", true);
                timeSinceAction += Time.deltaTime;
                canPress = false;
                if(timeSinceAction > actionCooldown)
                {
                    timeSinceAction = actionCooldown;
                }
            }
            else
            {
                playerAnimator.SetBool("isFlipping", false);
            }
        }
        else
        {
            if(timeSinceAction > 0)
            {
                timeSinceAction -= Time.deltaTime;
                if(timeSinceAction < 0)
                {
                    timeSinceAction = 0;
                }
            }
            playerAnimator.SetBool("isFlipping", false);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            shotCounter -= Time.deltaTime;
            if (shotCounter <= 0)
            {
                playerAnimator.SetBool("isFiring", true);
                Instantiate(bullet,firePoint.position,firePoint.rotation);
                shotCounter = timeBetweenShots;
            }
        }
        else
        {
            playerAnimator.SetBool("isFiring", false);
        }
    }
    private IEnumerator CoolDown()
    {
        canPress = false;
        yield return new WaitForSeconds(0.32f);
        canPress = true;
    }
}
