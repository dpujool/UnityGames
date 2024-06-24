using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalGame : MonoBehaviour
{
    [SerializeField] private GameManagerSO gameManager;
    [SerializeField] private GameObject first, final;
    [SerializeField] private GameObject robotToAppear;
    // Start is called before the first frame update
    void Start()
    {
        if (gameManager.IsEnded)
        {
            final.SetActive(true);
            first.SetActive(false);
            robotToAppear.SetActive(true);
        }
        else
        {
            final.SetActive(false);
            first.SetActive(true);
            robotToAppear.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
