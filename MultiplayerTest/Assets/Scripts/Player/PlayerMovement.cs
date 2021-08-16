using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

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

    [SerializeField]
    bool canJump = false;
    EdgeCollider2D bottomCollider;

    [SerializeField]
    TMP_Text NameUI;

    float dt { get => Time.deltaTime; }

    private PhotonView photonView;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bottomCollider = GetComponent<EdgeCollider2D>();
        photonView = GetComponent<PhotonView>();
    }

    void Update() 
    { 
        #region Left Detection
        if (Input.GetKey(left))
        {
            isLeft = true;
            transform.rotation = Quaternion.AngleAxis(0, Vector2.up);
            NameUI.transform.rotation = new Quaternion(0, 0, 0, 0);
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
            NameUI.transform.rotation = new Quaternion(0, 180, 0, 0);
        }
        else
        {
            isRight = false;
        }
        #endregion
        #region Jump Detection
        if (Input.GetKeyDown(jump) && canJump)
        {
            isJump = true;
        }
        #endregion

        if (bottomCollider.IsTouchingLayers(LayerMask.GetMask("Terrain")))
        {
            canJump = true;
        }
        else
        {
            canJump = false;
        }
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
