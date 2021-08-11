using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static event Action<int> OnMove = delegate { };

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (Input.mousePosition.x > Screen.width / 2)
            {
                OnMove.Invoke(0);
            }
            else if(Input.mousePosition.x < Screen.width / 2)
            {
                OnMove.Invoke(1);
            }
        }
    }
}
