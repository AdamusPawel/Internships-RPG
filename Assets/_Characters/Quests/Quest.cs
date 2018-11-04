using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    enum QuestProgress { NOT_AVAILABLE, AVAILABLE, ACCEPTED, COMPLETE, DONE }

    string title;                                  // title for the quest
    int id;                                        // id of quest
    QuestProgress progress;                        // state of quest (enum)
    string description;                            // description given by questlog/giver
    string hint;                                   // hints how to complete quest
    string summary;                                // 
    int nextQuest;                                 // if here is any "chain" quest line

    string questObjective;                         // name of the quest objective
    int questObjectiveCount;                       // current number of them
    int questObjectiveRequirement;                 // req amount of objectives

    int expReward;                                 // exp
    int goldReward;                                // gold
    string itemReward;                             // items rewarded

}
