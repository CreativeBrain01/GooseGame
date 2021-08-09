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
    KeyCode[] left, right, jump;
    [SerializeField]
    float speed = 2.0f;
    [SerializeField]
    float jumpStrength = 1.0f;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float dt = Time.deltaTime;
        bool isLeft = false, isRight = false, isJump = false;

        #region Left Detection
        foreach (KeyCode keybind in left)
        {
            if (Input.GetKey(keybind))
            {
                isLeft = true;
            }
        }
        #endregion
        #region Right Detection
        foreach (KeyCode keybind in right)
        {
            if (Input.GetKey(keybind))
            {
                isRight = true;
            }
        }
        #endregion
        #region Jump Detection
        foreach (KeyCode keybind in jump)
        {
            if (Input.GetKeyDown(keybind))
            {
                isJump = true;
            }
        }
        #endregion

        float x = 0, y = 0;
        if (isLeft) x -= speed * 1000 * dt;
        if (isRight) x += speed * 1000 * dt;
        if (isJump) y += (jumpStrength * 1000 * dt) + rb.gravityScale;

        rb.AddForce(new Vector2(0, y), ForceMode2D.Impulse);
        rb.velocity = new Vector2(x, rb.velocity.y);
    }
}
