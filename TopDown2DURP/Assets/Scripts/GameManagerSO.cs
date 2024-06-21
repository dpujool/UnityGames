using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "Scriptable Objects/GameManager")]
public class GameManagerSO : ScriptableObject
{
    [SerializeField]
    private Bomb bomb;
    [NonSerialized]
    private Vector3 initPlayerPosition = new (0.546f, 3.666f, 0);
    [NonSerialized]
    private Vector3 initPlayerRotation = new (0,1,0);
    //[NonSerialized]
    //private int healthPlayer = 3;
    [NonSerialized]
    private Dictionary<int, bool> items = new ();
    [NonSerialized]
    private List<ItemSO> itemsRecolected = new List<ItemSO>();
    [NonSerialized]
    private InventorySystem inventory;

    private Player player;


    public InventorySystem Inventory { get => inventory; }
    public Vector3 InitPlayerPosition { get => initPlayerPosition; }
    public Vector3 InitPlayerRotation { get => initPlayerRotation; }
    public Dictionary<int, bool> Items { get => items; set => items = value; }
    public List<ItemSO> ItemsRecolected { get => itemsRecolected; set => itemsRecolected = value; }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += NewSceneLoaded;
    }

    private void NewSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        player = FindObjectOfType<Player>();
        inventory = FindObjectOfType<InventorySystem>();
    }

    public void ChangePlayerState(bool state)
    {
        player.Interacting = state;
    }

    public void LoadNewScene(Vector3 newPosition, Vector2 newRotation, int newSceneIndex)
    {
        initPlayerPosition = newPosition;
        initPlayerRotation = newRotation;

        SceneManager.LoadScene(newSceneIndex);
    }

    public void AddItemInInventory(ItemSO item, int id)
    {
        inventory.NewItem(item);
        if (!items.ContainsKey(id))
        {
            items.Add(id, false);
        }
    }

    public void RefreshInventory()
    {
        foreach(var item in itemsRecolected)
        {
            inventory.NewItem(item);
        }
    }

    public bool CanActivateBomb()
    {
        return player.CanActivateBomb();
    }
}
