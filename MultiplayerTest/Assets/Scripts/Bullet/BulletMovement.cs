using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class BulletMovement : MonoBehaviour
{
    //Components
    [Header("Rigid Body")]
    [SerializeField]
    Rigidbody2D rb;

    GameObject owner;
    [SerializeField]
    float speed = 2.0f;

    public GameObject Owner { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 3);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject == Owner) return;
        if(collision.collider.gameObject.layer == 6)
        {
            Debug.Log("Hit");
        }
        Destroy(gameObject);
    }

    public void Shoot(GameObject owner)
    {
        Owner = owner;
        rb.velocity = (owner.transform.rotation.y == 0) ? Vector2.left * speed : Vector2.right * speed;
    }
}
