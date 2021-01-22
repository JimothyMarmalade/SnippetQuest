using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public UIController UIControllerInstance;
    public GameObject UICanvas;

    private List<GameObject> AllPuzzlePickups = new List<GameObject>();
    public GameObject PicrossPuzzlePickup1;
    public GameObject PicrossPuzzlePickup2;
    public GameObject PicrossPuzzlePickup3;
    public GameObject FutoshikiPuzzlePickupRand;

    public AudioSource AdventureSFX;
    public AudioClip PuzzleCollected;

    private void Awake()
    {
        UICanvas.SetActive(true);
        AllPuzzlePickups.Add(PicrossPuzzlePickup1);
        AllPuzzlePickups.Add(PicrossPuzzlePickup2);
        AllPuzzlePickups.Add(PicrossPuzzlePickup3);
        AllPuzzlePickups.Add(FutoshikiPuzzlePickupRand);

        foreach (GameObject obj in AllPuzzlePickups)
        {
            obj.GetComponent<PuzzlePickup>().SetControllerReference(this);
        }
    }

    public void ActivateSnippetButton(int ID)
    {
        UIControllerInstance.ActivateSnippetButton(ID);
    }

    public void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            if (!GameObject.FindGameObjectWithTag("Player").GetComponent<AdvancedThirdPersonMovement>().PlayerIsInSerenePlace)
            {
                Debug.Log("Terminating Application");
                Application.Quit();
            }
        }
    }

}
