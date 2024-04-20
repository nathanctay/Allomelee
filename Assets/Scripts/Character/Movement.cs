using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public int playerNumber = 1;
    private Rigidbody2D body;
    private SpriteRenderer sr;
    public float speed = 10;
    public float jumpPower = 15;
    private bool onGround;
    public bool doubleJump;

    // private GameManager.ControllerState controllerState;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        // controllerState = new GameManager.ControllerState;
    }

    // Update is called once per frame
    void Update()
    {
        float movementX = Input.GetAxis("Horizontal");
        float movementY = Input.GetAxisRaw("Vertical");
        // controllerState.joystick = new GameManager.Joystick(0, 0);
        body.velocity = new Vector2(movementX * speed, body.velocity.y);
        if (onGround)
        {
            body.velocity = new Vector2(movementX * speed, movementY * jumpPower);
        }
        else if (doubleJump)
        {
            if (Input.GetButtonDown("Vertical"))
            {
                body.velocity = new Vector2(movementX * speed, movementY * jumpPower);
                doubleJump = false;
            }
        }

        if (movementX < 0)
        {
            sr.flipX = true;
        }
        else if (movementX > 0)
        {
            sr.flipX = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = true;
            doubleJump = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = false;
        }
    }
}
