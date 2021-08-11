using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    //Components
    Rigidbody2D rb;

    //Keybind
    [SerializeField]
    KeyCode left, right, jump;
    [SerializeField]
    float speed = 2.0f;
    [SerializeField]
    float jumpStrength = 1.0f;

    bool isLeft = false, isRight = false, isJump = false;

    float dt { get => Time.deltaTime; }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() 
    { 
        #region Left Detection
        if (Input.GetKey(left))
        {
            isLeft = true;
            transform.rotation = Quaternion.AngleAxis(0, Vector2.up);
        }
        else
        {
            isLeft = false;
        }
        #endregion
        #region Right Detection
        if (Input.GetKey(right))
        {
            isRight = true;
            transform.rotation = Quaternion.AngleAxis(180, Vector2.up);
        }
        else
        {
            isRight = false;
        }
        #endregion
        #region Jump Detection
        if (Input.GetKeyDown(jump))
        {
            isJump = true;
        }
        #endregion
    }

    private void FixedUpdate()
    {
        float x = 0;
        if (isLeft) x -= speed;
        if (isRight) x += speed;
        if (isJump) 
        {
            rb.AddForce(jumpStrength * Vector2.up, ForceMode2D.Impulse);
            isJump = false;
        }

        rb.velocity = new Vector2 ( x, rb.velocity.y );
    }
}
