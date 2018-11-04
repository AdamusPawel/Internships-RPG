using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    public enum QuestProgress { NOT_AVAILABLE, AVAILABLE, ACCEPTED, COMPLETE, DONE }

    public string title;                                  // title for the quest
    public int id;                                        // id of quest
    public QuestProgress progress;                        // state of quest (enum)
    public string description;                            // description given by questlog/giver
    public string hint;                                   // hints how to complete quest
    public string summary;                                // 
    public int nextQuest;                                 // if here is any "chain" quest line

    public string questObjective;                         // name of the quest objective
    public int questObjectiveCount;                       // current number of them
    public int questObjectiveRequirement;                 // req amount of objectives

    public int expReward;                                 // exp
    public int goldReward;                                // gold
    public string itemReward;                             // items 
}
