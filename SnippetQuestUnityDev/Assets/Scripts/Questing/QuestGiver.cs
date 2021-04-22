/*
 * Created by Logan Edmund, 4/21/21
 * Last Modified by Logan Edmund, 4/21/21
 * 
 * Handles the distribution of quests to the player
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{

    public Quest quest;

    //Need a reference to the player or the player's quest log

    public void AcceptQuest()
    {
        quest.IsActive = true;
        GameObject.FindGameObjectWithTag("Player").GetComponent<QuestLog>().questList.Add(quest);
    }



}
