using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    private static bool lastBalloonActive;

    // Start is called before the first frame update
    void Start()
    {
        lastBalloonActive = true;
    }

    // Update is called once per frame
    void Update()
    {

        
    }

    public static bool IsLastActive()
    {
        return lastBalloonActive;
    }

    private void OnTriggerEnter(Collider other)
    {
        this.gameObject.SetActive(false);
        lastBalloonActive = false;
        string handName = other.transform.parent.transform.parent.transform.parent.name;
        BalloonSceneController.UpdatePathLength(handName, other.transform.position);
    }

}
