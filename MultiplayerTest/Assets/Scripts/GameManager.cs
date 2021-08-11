using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    float deathCooldown = 10.0f;

    void Start()
    {
        deathCooldown = 10.0f;
    }

    void Update()
    {
        float dt = Time.deltaTime;
        if (deathCooldown > 0) deathCooldown -= dt;
    }
}