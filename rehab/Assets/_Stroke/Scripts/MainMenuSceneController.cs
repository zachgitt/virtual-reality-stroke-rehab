using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuSceneController : MonoBehaviour
{
    public GameObject balloonScene;
    public GameObject basketScene;
    public GameObject cubeScene;
    private Material mat;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        balloonScene.GetComponent<MeshRenderer>().material = mat;

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

            default:
                Debug.Log("This should not be reached.");
                break;
        }
    }
}
