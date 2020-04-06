using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acorn : OVRGrabbable
{

    public GameObject acornPrefab;
    public Material outlineMaterial;

    private Material startingMaterial;
    private GameObject grabbedAcorn;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        startingMaterial = this.GetComponentInChildren<MeshRenderer>().material;
        //Debug.Log("Staring Material: " + startingMaterial);
    }

    protected void Update()
    {
        //this.GetComponentInChildren<MeshRenderer>().material = outlineMaterial;
        //Debug.Log("Material: " + this.GetComponentInChildren<MeshRenderer>().material);

        //if (this.isGrabbed)
        //{
        //    this.GetComponentInChildren<MeshRenderer>().material = outlineMaterial;
        //    this.GetComponentInChildren<Rigidbody>().useGravity = true;
        //}
        //else
        //{
        //    this.GetComponentInChildren<MeshRenderer>().material = startingMaterial;
        //}

    }

    //public override void GrabBegin(OVRGrabber hand, Collider grabPoint)
    //{
    //    base.GrabBegin(hand, grabPoint);
    //    this.GetComponentInChildren<MeshRenderer>().material = outlineMaterial;
    //    //ChangeGrabbedAcornMaterial(outlineMaterial);
    //}

    //public override void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity)
    //{
    //    ChangeGrabbedAcornMaterial(startingMaterial);
    //    ActivateAcornGravity();
    //    base.GrabEnd(linearVelocity, angularVelocity);
    //}
}
