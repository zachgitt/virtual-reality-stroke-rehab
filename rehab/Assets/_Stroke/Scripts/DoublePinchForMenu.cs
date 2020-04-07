using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static OVRHand;
using UnityEngine.SceneManagement;


public class DoublePinchForMenu : MonoBehaviour
{
    public OVRHand leftHand;
    public OVRHand rightHand;
    public float threshold;

    // Update is called once per frame
    void Update()
    {
        float leftPinchStrength = leftHand.GetFingerPinchStrength(HandFinger.Index);
        float rightPinchStrength = rightHand.GetFingerPinchStrength(HandFinger.Index);
        if (leftPinchStrength > threshold && rightPinchStrength > threshold)
        {
            SceneManager.LoadScene(0);
        }
    }
}
