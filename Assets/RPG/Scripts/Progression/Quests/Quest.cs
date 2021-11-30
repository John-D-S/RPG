using System.Collections;
using System.Collections.Generic;

using UnityEditor;

using UnityEngine;

public abstract class Quest
{
    public string questName;
    public bool questCompleted;

    public abstract bool QuestCondition { get; set; }
}

public class FetchQuest : Quest
{
    public Progress.Item item;
    public PlayerInventory inventory;
    public override bool QuestCondition { get; set; }
}