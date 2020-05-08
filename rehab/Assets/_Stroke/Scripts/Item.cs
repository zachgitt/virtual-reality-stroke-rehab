using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : OVRGrabbable
{
    private bool inBox;
    private bool inCorrectBox;
    private bool missed;

    protected override void Start()
    {
        inBox = false;
        inCorrectBox = false;
        missed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= 0 && !inBox)
        {
            missed = true;
            GetComponentInChildren<Rigidbody>().useGravity = false;
        }

    }

    public bool IsInBox() { return inBox; }
    public bool IsInCorrectBox() { return inCorrectBox; }
    public bool WasMissed() { return missed; }
    

    private void OnTriggerEnter(Collider other)
    {
        if (!inBox && other.name.Contains("Box"))
        {
            Vector3 triggerPos = transform.position;
            inBox = true;
            GetComponentInChildren<Rigidbody>().useGravity = false;
            GetComponentInChildren<Rigidbody>().isKinematic = true;
            transform.position = triggerPos;

            // Item was placed in correct box
            if (other.CompareTag(tag))
                inCorrectBox = true;
        }
    }


    public override void GrabBegin(OVRGrabber hand, Collider grabPoint)
    {
        base.GrabBegin(hand, grabPoint);
    }

    public override void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity)
    {
        base.GrabEnd(linearVelocity, angularVelocity);
    }
}
