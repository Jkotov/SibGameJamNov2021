using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSampleSceneOnClick : MonoBehaviour
{
    private void OnMouseUpAsButton()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
