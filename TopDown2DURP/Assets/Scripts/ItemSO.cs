using UnityEngine;

[CreateAssetMenu(fileName = "Scriptable Objects/Item")]
public class ItemSO : ScriptableObject
{
    public float damage;
    public string itenName;
    public int levelToUse;
    public Sprite icon;
}
