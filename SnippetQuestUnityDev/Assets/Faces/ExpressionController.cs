using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpressionController : MonoBehaviour
{
    public Transform eyes;
    public Transform mouth;

    public Animator eyesAnimator;
    public Animator mouthAnimator;

    public enum EyesExpression {None, Angry, Sad, Curious, Surprised, Upset, Happy};
    public enum MouthExpression {None, Angry, Sad, Curious, Surprised, Upset, Happy, Open_O, MessageBox};

    public EyesExpression eyeCurrEXP = EyesExpression.None;
    public MouthExpression mouthCurrEXP = MouthExpression.None;


    public float maxEyeMoveDist = 0.3f;

    private IEnumerator Blink;

    public bool manualControlEnabled = false;
    private bool isExpressing = false;

    void Start()
    {
        Blink = calcBlink();
        StartCoroutine(Blink);
    }

    void Update()
    {
        if (manualControlEnabled)
            CheckPlayerInput();
    }

    private IEnumerator calcBlink()
    {
        while (true)
        {
            int delay = Random.Range(0, 8);
            //Debug.Log("ExpressionController>calcBlink: delay = " + delay);
            yield return new WaitForSeconds(delay);
            eyesAnimator.SetTrigger("DoEyeblink");
        }
    }

    public void CheckPlayerInput()
    {
        //Eye Movement
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            eyes.localPosition = new Vector3(eyes.localPosition.x + 0.05f, eyes.localPosition.y, eyes.localPosition.z);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            eyes.localPosition = new Vector3(eyes.localPosition.x - 0.05f, eyes.localPosition.y, eyes.localPosition.z);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            eyes.localPosition = new Vector3(eyes.localPosition.x, eyes.localPosition.y + 0.03f, eyes.localPosition.z);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            eyes.localPosition = new Vector3(eyes.localPosition.x, eyes.localPosition.y - 0.03f, eyes.localPosition.z);
        }

        //Mouth Movement
        if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            mouth.localPosition = new Vector3(mouth.localPosition.x + 0.05f, mouth.localPosition.y, mouth.localPosition.z);
        }
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            mouth.localPosition = new Vector3(mouth.localPosition.x - 0.05f, mouth.localPosition.y, mouth.localPosition.z);
        }
        if (Input.GetKeyDown(KeyCode.Keypad8))
        {
            mouth.localPosition = new Vector3(mouth.localPosition.x, mouth.localPosition.y, mouth.localPosition.z - 0.03f);
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            mouth.localPosition = new Vector3(mouth.localPosition.x, mouth.localPosition.y, mouth.localPosition.z + 0.03f);
        }


        //Press the up arrow button to reset the trigger and set another one
        if (Input.GetKeyDown(KeyCode.Q))
        {
            //toggle IsAngry
            bool toggle = !eyesAnimator.GetBool("IsAngry");
            eyesAnimator.SetBool("IsAngry", toggle);
            mouthAnimator.SetBool("MouthAngry", toggle);
            isExpressing = toggle;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            //toggle IsAngry
            bool toggle = !eyesAnimator.GetBool("IsSad");
            eyesAnimator.SetBool("IsSad", toggle);
            mouthAnimator.SetBool("MouthSad", toggle);
            isExpressing = toggle;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            //toggle IsAngry
            bool toggle = !eyesAnimator.GetBool("IsCurious");
            eyesAnimator.SetBool("IsCurious", toggle);
            mouthAnimator.SetBool("MouthCurious", toggle);
            isExpressing = toggle;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            //toggle IsAngry
            bool toggle = !eyesAnimator.GetBool("IsSurprised");
            eyesAnimator.SetBool("IsSurprised", toggle);
            mouthAnimator.SetBool("MouthSurprised", toggle);
            isExpressing = toggle;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            //toggle IsAngry
            bool toggle = !eyesAnimator.GetBool("IsUpset");
            eyesAnimator.SetBool("IsUpset", toggle);
            mouthAnimator.SetBool("MouthUpset", toggle);
            isExpressing = toggle;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            //toggle IsAngry
            bool toggle = !eyesAnimator.GetBool("IsHappy");
            eyesAnimator.SetBool("IsHappy", toggle);
            mouthAnimator.SetBool("MouthSmile", toggle);
            isExpressing = toggle;
        }



        if (Input.GetKeyDown(KeyCode.Z))
        {
            //toggle IsAngry
            bool toggle = !mouthAnimator.GetBool("MouthOpen");
            mouthAnimator.SetBool("MouthOpen", toggle);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            //toggle IsAngry
            bool toggle = !mouthAnimator.GetBool("MouthMessageBox");
            mouthAnimator.SetBool("MouthMessageBox", toggle);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            //toggle IsAngry
            bool toggle = !mouthAnimator.GetBool("MouthSmile");
            mouthAnimator.SetBool("MouthSmile", toggle);
        }

    }

    //Hard-wipes the face animator to stop any current non-default expressions
    public void ClearExpression()
    {
        foreach (AnimatorControllerParameter parameter in eyesAnimator.parameters)
        {
            if (parameter.type == AnimatorControllerParameterType.Bool)
                eyesAnimator.SetBool(parameter.name, false);
        }
        foreach (AnimatorControllerParameter parameter in mouthAnimator.parameters)
        {
            if (parameter.type == AnimatorControllerParameterType.Bool)
                mouthAnimator.SetBool(parameter.name, false);
        }

        isExpressing = false;
    }

    //turns on the associated expression
    public void ChangeExpression(EyesExpression eyes, MouthExpression mouth)
    {
        if (isExpressing)
            ClearExpression();

        switch (eyes)
        {
            case EyesExpression.None:
                eyeCurrEXP = eyes;
                break;
            case EyesExpression.Angry:
                eyesAnimator.SetBool("IsAngry", true);
                eyeCurrEXP = eyes;
                break;
            case EyesExpression.Curious:
                eyesAnimator.SetBool("IsCurious", true);
                eyeCurrEXP = eyes;
                break;
            case EyesExpression.Sad:
                eyesAnimator.SetBool("IsSad", true);
                eyeCurrEXP = eyes;
                break;
            case EyesExpression.Happy:
                eyesAnimator.SetBool("IsHappy", true);
                eyeCurrEXP = eyes;
                break;
            case EyesExpression.Surprised:
                eyesAnimator.SetBool("IsSurprised", true);
                eyeCurrEXP = eyes;
                break;
            case EyesExpression.Upset:
                eyesAnimator.SetBool("IsUpset", true);
                eyeCurrEXP = eyes;
                break;
        }

        switch (mouth)
        {
            case MouthExpression.None:
                mouthCurrEXP = mouth;
                break;
            case MouthExpression.Angry:
                mouthAnimator.SetBool("MouthAngry", true);
                mouthCurrEXP = mouth;
                break;
            case MouthExpression.Curious:
                mouthAnimator.SetBool("MouthCurious", true);
                mouthCurrEXP = mouth;
                break;
            case MouthExpression.Sad:
                mouthAnimator.SetBool("MouthSad", true);
                mouthCurrEXP = mouth;
                break;
            case MouthExpression.Happy:
                mouthAnimator.SetBool("MouthSmile", true);
                mouthCurrEXP = mouth;
                break;
            case MouthExpression.Surprised:
                mouthAnimator.SetBool("MouthSurprised", true);
                mouthCurrEXP = mouth;
                break;
            case MouthExpression.Upset:
                mouthAnimator.SetBool("MouthUpset", true);
                mouthCurrEXP = mouth;
                break;
            case MouthExpression.Open_O:
                mouthAnimator.SetBool("MouthOpen", true);
                mouthCurrEXP = mouth;
                break;
            case MouthExpression.MessageBox:
                mouthAnimator.SetBool("MouthMessageBox", true);
                mouthCurrEXP = mouth;
                break;
        }

        if (eyeCurrEXP != EyesExpression.None || mouthCurrEXP != MouthExpression.None)
            isExpressing = true;
        else
            isExpressing = false;


    }

}
