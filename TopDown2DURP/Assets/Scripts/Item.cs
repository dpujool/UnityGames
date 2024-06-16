using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, Interactive
{
    [SerializeField] private ItemSO myDates;
    [SerializeField] private GameManagerSO gameManager;

    public void Interact()
    {
        gameManager.Inventory.NewItem(myDates);
        Destroy(gameObject);
    }
}
