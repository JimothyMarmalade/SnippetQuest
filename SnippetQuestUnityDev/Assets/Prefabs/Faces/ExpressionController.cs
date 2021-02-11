using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpressionController : MonoBehaviour
{
    public Animator faceAnimator;

    private IEnumerator Blink;

    void Start()
    {
        Blink = calcBlink();
        StartCoroutine(Blink);
    }

    void Update()
    {
        //Press the up arrow button to reset the trigger and set another one
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log("ExpressionController: Up Arrow Pressed, toggling IsAngry");
            //toggle IsAngry
            faceAnimator.SetBool("IsAngry", (!faceAnimator.GetBool("IsAngry")));
        }

    }

    private IEnumerator calcBlink()
    {
        while (true)
        {
            int delay = Random.Range(0, 6);
            Debug.Log("ExpressionController>calcBlink: delay = " + delay);
            yield return new WaitForSeconds(delay);
            faceAnimator.SetTrigger("DoEyeblink");
        }
    }

}
