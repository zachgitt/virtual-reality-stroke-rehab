﻿using System.Collections;
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

    void AcornGrabGameUpdate()
    {
        if (m_grabbedObj.name.Equals("Acorn"))
        {
            if (BasketSceneController.acorns.Count == 1)
                BasketSceneController.StartGameTimer();
            BasketSceneController.AddInteraction();
        }
    }

    void AcornReleaseGameUpdate()
    {
        if (m_grabbedObj.name.Equals("Acorn"))
        {
            m_grabbedObj.GetComponentInChildren<Rigidbody>().useGravity = true;
            m_grabbedObj.GetComponentInChildren<Rigidbody>().isKinematic = false;
        }
    }

    void CheckPinchIndex()
    {
        float pinchStrength = hand.GetFingerPinchStrength(OVRHand.HandFinger.Index);
        if (!m_grabbedObj && (pinchStrength > pinchThreshold) && (m_grabCandidates.Count > 0))
        {
            GrabBegin();
            startingMaterial = m_grabbedObj.GetComponentInChildren<MeshRenderer>().material;
            m_grabbedObj.GetComponentInChildren<MeshRenderer>().material = outlineMaterial;
            AcornGrabGameUpdate();
        }

        else if (m_grabbedObj && !(pinchStrength > pinchThreshold))
        {
            m_grabbedObj.GetComponentInChildren<MeshRenderer>().material = startingMaterial;
            AcornReleaseGameUpdate();
            GrabEnd();
        }
    }
}
