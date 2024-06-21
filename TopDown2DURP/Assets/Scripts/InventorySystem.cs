using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class InventorySystem : MonoBehaviour
{
    [SerializeField] private GameManagerSO gameManager;
    [SerializeField] private GameObject inventoryFrame;
    [SerializeField] private Button[] buttons;
    [SerializeField] private Bomb bomb;

    private Dictionary<int, ItemSO> items = new();

    private int enabledItems = 0;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < buttons.Length; i++)
        {
            int buttonIndex = i;
            buttons[i].onClick.AddListener(() => ButtonClicked(buttonIndex));
        }
        gameManager.RefreshInventory();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            
            LaunchInventory();
        }
    }

    private void ButtonClicked(int buttonIndex)
    {
        ItemSO itemPressed = items[buttonIndex];
        if(itemPressed.itenName == "Bomb")
        {
            if (gameManager.CanActivateBomb())
            {
                bomb.gameObject.SetActive(true);
                buttons[buttonIndex].gameObject.SetActive(false);
                enabledItems--;
                items.Remove(buttonIndex);
            }
        }
    }

    public void NewItem(ItemSO itemData)
    {
        buttons[enabledItems].gameObject.SetActive(true);
        buttons[enabledItems].gameObject.GetComponent<Image>().sprite = itemData.icon;
        items[enabledItems] = itemData;
        enabledItems++;
    }

    private void LaunchInventory()
    {
        inventoryFrame.SetActive(!inventoryFrame.activeSelf);
    }
}
