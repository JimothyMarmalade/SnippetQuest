/*
 * Created by Logan Edmund, 5/10/21
 * Last Modified by Logan Edmund, 5/10/21
 * 
 * Inheritable script -- inherited by other scripts to handle the enabling/disabling of player actions during gameplay
 * and to hold shared methods.
 * 
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerAction : MonoBehaviour
{
    
    public bool canPerform { get; set; }


    public virtual void Awake()
    {
        EnableAction();
    }


    public virtual void EnableAction()
    {
        canPerform = true;
    }

    public virtual void DisableAction()
    {
        canPerform = false;
    }


}
