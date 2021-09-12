/*
 * Created by Logan Edmund, 3/2/21
 * Last Modified by Logan Edmund, 7/10/21
 * 
 * A DialogTrigger is simply a container that stores a dialog pack and sends it to the dialogManager when necessary to display it onscreen.
 * 
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    //Default dialog
    public Dialog defaultDialog;
    private ExpressionController faceReference;

    public void TriggerDialog()
    {
        //Add methods to OndialogOver to restore control after conversation
        DialogManager.OnDialogOver += DialogOver;

        //Disable player movement
        PlayerController.Instance.DisableAllMovement();

        //Begin dialog
        defaultDialog.DisplayDialog();

        //Begin facial animations
        //faceReference.ChangeExpression(defaultDialog.eyesExpression, defaultDialog.mouthExpression);

        //Begin music changes
        AudioManager.Instance.BGMFocusActivity(1.5f);
    }

    /*
    public void TriggerDialog(Dialog d)
    {
        //Add methods to OndialogOver to restore control after conversation
        DialogManager.OnDialogOver += DialogOver;

        //Disable player movement
        PlayerController.Instance.DisableAllMovement();

        //Begin dialog
        DialogManager.Instance.StartDialog(d);

        //Begin facial animations
        //faceReference.ChangeExpression(d.eyesExpression, d.mouthExpression);

        //Begin music changes
        AudioManager.Instance.BGMFocusActivity(1.5f);
    }
    */

    public void TriggerDialog(Dialog d, ExpressionController charFace)
    {
        //Add methods to OndialogOver to restore control after conversation
        DialogManager.OnDialogOver += DialogOver;

        //Disable player movement
        PlayerController.Instance.DisableAllMovement();

        DialogManager.Instance.SetCharacterFace(faceReference);
        try
        {
            d.DisplayDialog();
        }
        catch
        {
            Debug.LogWarning("Could not display Dialog.");
        }

        //Begin music changes
        AudioManager.Instance.BGMFocusActivity(1.5f);
    }
    
    /*
    public void TriggerDialog(DialogChoice d, ExpressionController charFace)
    {
        Debug.Log("Running Dialog with a DialogChoice...");
        //Add methods to OndialogOver to restore control after conversation
        DialogManager.OnDialogOver += DialogOver;

        //Disable player movement
        PlayerController.Instance.DisableAllMovement();

        //Begin dialog
        DialogManager.Instance.StartDialogChoice(d, charFace);

        //Begin facial animations
        //charFace.ChangeExpression(d.eyesExpression, d.mouthExpression);

        //Begin music changes
        AudioManager.Instance.BGMFocusActivity(1.5f);
    }
    */

    public void DialogOver()
    {
        //Restore Player Movement
        PlayerController.Instance.EnableAllMovement();

        //Change music back to exploration
        AudioManager.Instance.BGMFocusExploration(1.5f);

        //Remove these methods from OndialogOver
        DialogManager.OnDialogOver -= DialogOver;
    }

    public void SetFaceReference(ExpressionController exc)
    {
        faceReference = exc;
    }
}
