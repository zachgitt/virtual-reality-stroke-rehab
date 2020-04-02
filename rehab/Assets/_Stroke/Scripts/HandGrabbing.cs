using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGrabbing : OVRGrabber
{
    private OVRHand hand;
    private float pinchThreshold = 0.7f;

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
        bool isGrabbing = !m_grabbedObj && (pinchStrength > pinchThreshold)
            && m_grabCandidates.Count > 0;
        bool isReleasing = m_grabbedObj && !(pinchStrength > pinchThreshold);

        if (isGrabbing)
            GrabBegin();
        else if (isReleasing)
            GrabEnd();
    }
}
