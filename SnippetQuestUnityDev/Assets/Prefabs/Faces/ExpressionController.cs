using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpressionController : MonoBehaviour
{
    public Transform eyes;
    public Transform mouth;

    public Animator eyesAnimator;
    public Animator mouthAnimator;

    public float maxEyeMoveDist = 0.3f;

    private IEnumerator Blink;

    private bool isExpressing = false;

    void Start()
    {
        Blink = calcBlink();
        StartCoroutine(Blink);
    }

    void Update()
    {
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

    }

    private IEnumerator calcBlink()
    {
        while (true)
        {
            int delay = Random.Range(0, 8);
            Debug.Log("ExpressionController>calcBlink: delay = " + delay);
            yield return new WaitForSeconds(delay);
            eyesAnimator.SetTrigger("DoEyeblink");
        }
    }

}
