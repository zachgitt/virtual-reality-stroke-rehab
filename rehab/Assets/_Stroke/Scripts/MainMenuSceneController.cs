using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuSceneController : MonoBehaviour
{
    public GameObject balloonScene;
    public GameObject basketScene;
    public GameObject cubeScene;
    public GameObject cashierScene;

    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
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
