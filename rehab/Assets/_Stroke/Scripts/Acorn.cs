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
        startingMaterial = acornPrefab.GetComponentInChildren<MeshRenderer>().material;
    }

    private GameObject GetGrabbedAcorn()
    {
        return BasketSceneController.acorns[BasketSceneController.acorns.Count - 1];
    }

    private void ChangeGrabbedAcornMaterial(Material mat)
    {
        grabbedAcorn = GetGrabbedAcorn();
        grabbedAcorn.GetComponentInChildren<MeshRenderer>().material = mat;
    }

    private void ActivateAcornGravity()
    {
        GetGrabbedAcorn().GetComponentInChildren<Rigidbody>().useGravity = true;
    }

    public override void GrabBegin(OVRGrabber hand, Collider grabPoint)
    {
        ChangeGrabbedAcornMaterial(outlineMaterial);
        base.GrabBegin(hand, grabPoint);
    }

    public override void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity)
    {
        ChangeGrabbedAcornMaterial(startingMaterial);
        ActivateAcornGravity();
        base.GrabEnd(linearVelocity, angularVelocity);
    }
}
