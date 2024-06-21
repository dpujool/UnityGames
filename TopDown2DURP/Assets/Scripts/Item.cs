using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, Interactive
{
    [SerializeField] private int id;
    [SerializeField] private ItemSO myDates;
    [SerializeField] private GameManagerSO gameManager;

    public void Interact()
    {
        gameManager.AddItemInInventory(myDates, id);
        gameManager.Items[id] = false;
        gameManager.ItemsRecolected.Add(myDates);
        Destroy(gameObject);
    }

    private void Start()
    {
        if (gameManager.Items.ContainsKey(id))
        {
            if (!gameManager.Items[id])
            {
                Destroy(gameObject);
            }
        }
        else
        {
            gameManager.Items.Add(id, true);
        }
    }

    private void Update()
    {
        
    }
}
