using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{

    public void ClickRetryButton()
    {
        SceneManager.LoadScene(0);
    }
}
