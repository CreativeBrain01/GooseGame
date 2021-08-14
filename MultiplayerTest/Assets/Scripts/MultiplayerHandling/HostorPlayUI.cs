using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Dropdown))]
public class HostorPlayUI : MonoBehaviour
{
    Dropdown dropdown;

    [SerializeField]
    GameObject hostMenu;
    [SerializeField]
    GameObject joinMenu;

    private void Start()
    {
        hostMenu.SetActive(false);
        joinMenu.SetActive(false);

        dropdown = GetComponent<Dropdown>();
    }

    void Update()
    {
        if(dropdown.value == 0)
        {
            hostMenu.SetActive(true);
            joinMenu.SetActive(false);
        } else
        {
            hostMenu.SetActive(false);
            joinMenu.SetActive(true);
        }
    }
}
