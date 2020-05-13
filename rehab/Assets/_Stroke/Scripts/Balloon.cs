using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    private bool popped;
    private string poppedBy;

    // Start is called before the first frame update
    void Start()
    {
        popped = false;
        poppedBy = "none";
    }

    public bool IsPopped() { return popped; }
    public string PoppedBy() { return poppedBy; }

    private string GetHand(Collider hand)
    {
        return hand.transform.parent.transform.parent.transform.parent.name;
    }
    private void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);
        popped = true;

        string handName = GetHand(other);
        if (handName.Contains("L"))
            poppedBy = "left";
        else if (handName.Contains("R"))
            poppedBy = "right";
    }

}
