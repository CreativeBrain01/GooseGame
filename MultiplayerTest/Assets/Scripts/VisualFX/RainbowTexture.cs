using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class RainbowTexture : MonoBehaviour
{
    [SerializeField]
    float smoothness;

    SpriteRenderer sprite;
    [SerializeField]
    float r = 0, g = 0, b = 0;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        Color color = sprite.color;
        sprite.color = new Color(r, g, b, sprite.color.a);
    }

    int colorVal = 0;
    //b += (1 / smoothness) * Time.deltaTime;
    bool rUp = true, gUp = true, bUp = true;
    bool rOn = true, gOn = false, bOn = false;
    void Update()
    {
        rControl();
        gControl();
        bControl();

        rCheck();
        gCheck();
        bCheck();

        sprite.color = new Color(r, g, b, sprite.color.a);
    }

    // a ^ -> b ^ -> a \/ -> c ^ -> b \/ -> a ^ -> c \/

    #region Controls
    void rControl()
    {
        if (rOn)
        {
            if (rUp)
            {
                r += (1 / smoothness) * Time.deltaTime;
            }
            else
            {
                r -= (1 / smoothness) * Time.deltaTime;
            }
        }
    }

    void gControl()
    {
        if (gOn)
        {
            if (gUp)
            {
                g += (1 / smoothness) * Time.deltaTime;
            }
            else
            {
                g -= (1 / smoothness) * Time.deltaTime;
            }
        }
    }

    void bControl()
    {
        if (bOn)
        {
            if (bUp)
            {
                b += (1 / smoothness) * Time.deltaTime;
            }
            else
            {
                b -= (1 / smoothness) * Time.deltaTime;
            }
        }
    }
    #endregion

    #region Checks
    void rCheck()
    {
        if (r > 1 || r < 0)
        {
            gOn = true;
            rOn = false;

            if (r > 1)
            {
                rUp = false;
            } else
            {
                rUp = true;
            }
        }
    }

    void gCheck()
    {
        if (g > 1 || g < 0)
        {
            bOn = true;
            gOn = false;

            if (g > 1)
            {
                gUp = false;
            }
            else
            {
                gUp = true;
            }
        }
    }

    void bCheck()
    {
        if (b > 1 || b < 0)
        {
            rOn = true;
            bOn = false;

            if (b > 1)
            {
                bUp = false;
            }
            else
            {
                bUp = true;
            }
        }
    }
    #endregion
}
