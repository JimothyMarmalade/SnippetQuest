using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GLD_GameController : MonoBehaviour
{
    public GLD_UIController UIControllerInstance;
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
            obj.GetComponent<GLD_PuzzlePickup>().SetControllerReference(this);
        }
    }

    public void ActivateSnippetButton(int ID)
    {
        UIControllerInstance.ActivateSnippetButton(ID);
    }

    public void Update()
    {
        
    }

}
