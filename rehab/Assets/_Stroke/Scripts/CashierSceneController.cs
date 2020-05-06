using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashierSceneController : MonoBehaviour
{
    public GameObject belt;
    public GameObject fruitBox;
    public GameObject meatBox;
    public GameObject dairyBox;

    private List<Vector3> startPos;
    
    // Start is called before the first frame update
    void Start()
    {
        RandomizeBagOrder();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RandomizeBagOrder()
    {
        startPos = new List<Vector3>()
        {
            fruitBox.transform.position,
            meatBox.transform.position,
            dairyBox.transform.position
        };

        for (int i = 0; i < startPos.Count; i++)
        {
            Vector3 temp = startPos[i];
            int swap = Random.Range(i, startPos.Count);
            startPos[i] = startPos[swap];
            startPos[swap] = temp;
        }

        fruitBox.transform.position = startPos[0];
        meatBox.transform.position = startPos[1];
        dairyBox.transform.position = startPos[2];
    }
}
