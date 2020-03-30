using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squirrel : MonoBehaviour
{
    private SphereCollider sphere;
    private MeshRenderer mesh;
    // Start is called before the first frame update
    void Start()
    {
        mesh = this.GetComponentInChildren<MeshRenderer>();
        mesh.enabled = false;
        sphere = this.GetComponentInChildren<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!mesh.enabled)
        {
            mesh.enabled = true;
            this.sphere.radius = 0.2f;
            this.sphere.transform.position = new Vector3(0, 0, 0.05f);
        }
    }
}
