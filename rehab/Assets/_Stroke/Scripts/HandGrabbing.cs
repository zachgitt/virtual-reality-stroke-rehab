using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGrabbing : OVRGrabber
{
    private OVRHand hand;
    private float pinchThreshold = 0.7f;
    public Material outlineMaterial;
    private Material startingMaterial;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        hand = GetComponent<OVRHand>();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        CheckPinchIndex();      
    }

    void CheckPinchIndex()
    {
        float pinchStrength = hand.GetFingerPinchStrength(OVRHand.HandFinger.Index);
        if (!m_grabbedObj && (pinchStrength > pinchThreshold) && (m_grabCandidates.Count > 0))
        {
            GrabBegin();
            startingMaterial = m_grabbedObj.GetComponentInChildren<MeshRenderer>().material;
            m_grabbedObj.GetComponentInChildren<MeshRenderer>().material = outlineMaterial;
        }

        else if (m_grabbedObj && !(pinchStrength > pinchThreshold))
        {
            m_grabbedObj.GetComponentInChildren<MeshRenderer>().material = startingMaterial;
            if (m_grabbedObj.name.Equals("Acorn"))
            {
                m_grabbedObj.GetComponentInChildren<Rigidbody>().useGravity = true;
                m_grabbedObj.GetComponentInChildren<Rigidbody>().isKinematic = false;
            }
            GrabEnd();
        }
    }
}
