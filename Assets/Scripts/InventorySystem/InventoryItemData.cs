using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObjects/Inventory Item Data")]
public class InventoryItemData : ScriptableObject
{
    public string id;
    public string displayName;
    public Sprite icon;
    public ItemUsage usage;
    public GameObject prefab;
    public bool notResetting;
    public DialogueData infoDialogue;
}
