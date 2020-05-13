using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadSceneScript : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        switch (this.gameObject.tag)
        {
            case "BalloonScene":
                SceneManager.LoadScene(1);
                break;

            case "BasketScene":
                SceneManager.LoadScene(2);
                break;

            case "CubeScene":
                SceneManager.LoadScene(3);
                break;

            case "CashierScene":
                SceneManager.LoadScene(4);
                break;

            default:
                Debug.Log("This should not be reached.");
                break;
        }
    }
}
