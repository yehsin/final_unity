using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappingRope : MonoBehaviour
{
    //public GameObject copy_body;

    public EventCOntrol mode;
    public GameObject Hook;
    public GameObject hookHolder;

    public GameObject[] bodys = new GameObject[14];

    private LineRenderer lr;
    private Vector3 grapplePoint;
    public LayerMask WhatIsGrappleable;
    public Transform gunTip, camera, player;
    
    private float maxDistance = 100f;
    private SpringJoint joint;

    bool flying;

    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    void FixedUpdate()
    {
        if (flying)
        {
            player.GetComponent<Rigidbody>().mass = 20;

            for (int i = 0; i < 14; i++)
            {
                bodys[i].GetComponent<Rigidbody>().useGravity = false;
                bodys[i].GetComponent<Rigidbody>().mass = 0.01f;
            }
        }
        else if(!flying)
        {
            player.GetComponent<Rigidbody>().mass = 1;

            for (int i = 0; i < 14; i++)
            {
                bodys[i].GetComponent<Rigidbody>().useGravity = true;
                bodys[i].GetComponent<Rigidbody>().mass = 1f;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (mode.mode == 1)
        {
            DrawRope();

            if (Input.GetMouseButtonDown(1))
            {
                //StartGrapple();

            }
            else if (Input.GetMouseButtonUp(1))
            {
                StopGrapple();
            }
        }
        
        
    }

    void LastUpdate()
    {
        DrawRope();
    }

    public void StartGrapple()
    {
        RaycastHit hit;
        if (Physics.Raycast(camera.position, camera.forward, out hit, maxDistance, WhatIsGrappleable))
        {
            

            grapplePoint = hit.point;
            
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);

            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;

            
            joint.spring = 25f;
            joint.damper = 0.7f;
            joint.massScale = 4.5f;
            currentGrapplePosition = gunTip.position;

            flying = true;
            
        }
    }
    private Vector3 currentGrapplePosition;
    public void ReturnHook()
    {
        Hook.transform.rotation = hookHolder.transform.rotation;
        Hook.transform.position = hookHolder.transform.position;
        StopGrapple();
    }

    void DrawRope()
    {
        if (!joint) return;

        lr.SetVertexCount(2);

        currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, grapplePoint, Time.deltaTime * 8f);
        lr.SetPosition(0, gunTip.position);
        lr.SetPosition(1, grapplePoint);
    }

    void StopGrapple()
    {
        lr.positionCount = 0;
        Destroy(joint);
        flying = false;
       
    }

    public bool IsGrappling()
    {
        return joint != null;
    }

    public Vector3 GetGrapplePoint()
    {
        return grapplePoint;
    }
}
