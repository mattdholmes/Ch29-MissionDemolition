using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// YOU must implement the Slingshot

public class Slingshot : MonoBehaviour {
    public GameObject prefabProjectile;
    public float velocityMult = 4f;
    public bool _______________________;
    public GameObject launchPoint;
    public Vector3 launchPos;
    public GameObject projectile;
    public bool aimingMode;


    // Place class variables here

    private void Awake()
    {
        Transform launchpointTrans = transform.Find("LaunchPoint");
        launchPoint = launchpointTrans.gameObject;
        launchPoint.SetActive( false );
        launchPos = launchpointTrans.position;
    
    }

    private void OnMouseEnter()
    {
        print("Slingshot: OnMouseEnter()");
        launchPoint.SetActive( true );
    }

    private void OnMouseExit()
    {
        print("Slingshot: OnMouseExit()");
        launchPoint.SetActive( false );
    }

    private void OnMouseDown()
    {
        aimingMode = true;
        projectile = Instantiate(prefabProjectile) as GameObject;
        projectile.transform.position = launchPos;
        GetComponent<Rigidbody>().isKinematic = true;
    }

    private void Update()
    {
        if (!aimingMode) return;
        Vector3 mousePos2D = Input.mousePosition;
        mousePos2D.z = -Camera.main.transform.position.z;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);
        Vector3 mouseDelta = mousePos3D - launchPos;
        float maxMagnitude = this.GetComponent<SphereCollider>().radius;
        if (mouseDelta.magnitude > maxMagnitude)
        {
            mouseDelta.Normalize();
            mouseDelta *= maxMagnitude;
        }
        Vector3 projPos = launchPos + mouseDelta;
        projectile.transform.position = projPos;

        if (Input.GetMouseButtonUp(0))
        {
            aimingMode = false;
            GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<Rigidbody>().velocity = -mouseDelta * velocityMult;
            projectile = null;
            MissionDemolition.ShotFired();
        }

    }
}
