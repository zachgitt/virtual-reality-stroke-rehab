using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGrabbing : OVRGrabber
{
    // Initialize public variables
    public Material outlineMaterial;

    // Intialize private variables
    private OVRHand hand;
    private float pinchThreshold = 0.6f;
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
            startingMaterial = m_grabbedObj.GetComponentInChildren<Renderer>().material;
            m_grabbedObj.GetComponentInChildren<Renderer>().material = outlineMaterial;
            m_grabbedObj.GetComponentInChildren<Renderer>().material.color = startingMaterial.color;
        }

        else if (m_grabbedObj && !(pinchStrength > pinchThreshold))
        {
            m_grabbedObj.GetComponentInChildren<Renderer>().material = startingMaterial;
            GrabEnd();
        }
    }
}
