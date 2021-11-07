using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnClick : MonoBehaviour
{
    private void OnMouseUpAsButton()
    {
        Destroy(gameObject);
    }
}
