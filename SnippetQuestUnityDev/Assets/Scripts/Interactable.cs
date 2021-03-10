using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float gizmoDisplayRadius = 3f;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, gizmoDisplayRadius);
    }

    public virtual void Interact()
    {
        Debug.Log("Called Interact on " + gameObject.name);
        //Perform interact actions here or in extended classes
    }
}
