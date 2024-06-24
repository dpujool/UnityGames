using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private GameManagerSO gameManager;
    public void ClickRetryButton()
    {
        gameManager.IsEnded = false;
        gameManager.HasMando = false;
        SceneManager.LoadScene(0);
        
    }
}
