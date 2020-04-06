using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuSceneController : MonoBehaviour
{
    public Scene balloonScene;
    public Scene basketScene;
    public Scene cubeScene;

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
        if (SceneManager.LoadScene(other.scene))
        {
            Debug.Log("TODO");
        }
    }
}
