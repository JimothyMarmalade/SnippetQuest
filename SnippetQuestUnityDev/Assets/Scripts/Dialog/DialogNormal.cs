/*
 /* Created by Logan Edmund, 7/13/21
 * Last Modified by Logan Edmund, 7/13/21
 * 
 * Regular dialog. Holds a Dialog variable to go to after completion.
 * 
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New DialogNormal", menuName = "Dialog/New DialogNormal")]
public class DialogNormal : Dialog
{
    [Header("Next Dialog")]
    public Dialog NextDialog;


    public override void DisplayDialog()
    {
        DialogManager.Instance.DisplayDialogNormal(this);
        DialogManager.Instance.SetNextDialog(NextDialog);
    }


}
