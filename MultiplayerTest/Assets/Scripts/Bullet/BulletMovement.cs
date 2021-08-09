using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class BulletMovement : MonoBehaviour
{
    //Components
    Rigidbody2D rb;

    GameObject owner;
    [SerializeField]
    float speed = 2.0f;

    public GameObject Owner { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
