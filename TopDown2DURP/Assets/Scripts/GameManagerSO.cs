using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Scriptable Objects/GameManager")]
public class GameManagerSO : ScriptableObject
{
    private Player player;
    private InventorySystem inventory;

    public InventorySystem Inventory { get => inventory; }

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
}
