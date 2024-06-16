using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{

    [SerializeField] private GameObject inventoryFrame;
    [SerializeField] private Button[] buttons;

    private int enabledItems = 0;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < buttons.Length; i++)
        {
            int buttonIndex = i;
            buttons[i].onClick.AddListener(() => ButtonClicked(buttonIndex));
        }
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
        
    }

    public void NewItem(ItemSO itemData)
    {
        buttons[enabledItems].gameObject.SetActive(true);
        buttons[enabledItems].gameObject.GetComponent<Image>().sprite = itemData.icon;

        enabledItems++;
    }

    private void LaunchInventory()
    {
        inventoryFrame.SetActive(!inventoryFrame.activeSelf);
    }
}
