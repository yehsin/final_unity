using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hook : MonoBehaviour
{
    public EventCOntrol mode;

    public GameObject[] bodys = new GameObject[14];
    public GameObject APR_COMP;

    public GameObject Hook;
    public GameObject hookHolder;
    public GameObject hookOBJ;
    public GameObject hookPoint;
    public GameObject hookConnect;
    public GameObject hookPrefab;
    Vector3 targetPoint;

    public LayerMask WhatIsGrappleable;

    public Transform cam;

    public float hookTravelSpeed;
    public float playerTravelSpeed;

    public static bool fired;
    public bool hooked;

    public float maxDistance;
    private float currentDistance;

    private bool grounded;

    //private bool mode;
    private bool swing;

    Vector3 hookDirection;
    // Start is called before the first frame update
    void Start()
    {
        //mode = false;
    }

    // Update is called once per frame
    void Update()
    {

        

        print(mode);
        CheckIfGrounded();
        // firing the hook

        if (Input.GetMouseButtonUp(0))
        {
            ReturnHook();
        }

        if (mode.mode == 0)
        {
            if (Input.GetMouseButtonDown(0) && fired == false)
            {
                //hookDirection = transform.TransformPoint(hookHolder.transform.position) - transform.TransformPoint(hookPoint.transform.position);
                // hookDirection.Normalize();

                fired = true;

                HookFire();
                
            }





            if (fired)
            {
                LineRenderer rope = GetComponent<LineRenderer>();
                rope.SetVertexCount(2);
                rope.SetPosition(0, hookHolder.transform.position);
                rope.SetPosition(1, Hook.transform.position);

                //Hook.transform.rotation = cam.rotation;
            }

            if (fired == true && hooked == false)
            {
                //Hook.transform.LookAt(targetPoint);
                //Vector3 Dir = (targetPoint - Hook.transform.position).normalized;

                //Vector3 newDir = Vector3.RotateTowards(Dir);




                //Hook.transform.position = Vector3.Lerp(Hook.transform.position, targetPoint, Time.deltaTime * hookTravelSpeed / Vector3.Distance(Hook.transform.position, targetPoint));
                //Hook.transform.Translate(cam.forward * Time.deltaTime * hookTravelSpeed);
                currentDistance = Vector3.Distance(transform.position, Hook.transform.position);

                if (currentDistance >= maxDistance)
                {
                    ReturnHook();
                }
            }

            if (hooked == true && fired == true)
            {

                Hook.transform.parent = hookOBJ.transform;

                
                //transform.position = Vector3.Lerp(transform.position, Hook.transform.position, (Time.deltaTime * playerTravelSpeed + Time.deltaTime*10) / Vector3.Distance(transform.position, Hook.transform.position));
                float distanceToHook = Vector3.Distance(transform.position, Hook.transform.position);

                Vector3 go_to_direction = (Hook.transform.position - transform.position).normalized;

                this.GetComponent<Rigidbody>().useGravity = false;
                //APR_COMP.transform.SetParent(this.transform);
                for (int i = 0; i < 14 ; i++)
                {
                    //bodys[i].transform.SetParent(this.transform);
                    
                    bodys[i].GetComponent<Rigidbody>().useGravity = false;
                    //bodys[i].GetComponent<Rigidbody>().mass = 0.001f;
                }
                this.transform.GetComponent<Rigidbody>().velocity = go_to_direction * 100f;
                if (distanceToHook < 1)
                {
                    //if (grounded == false)
                    
                        //this.transform.Translate(Vector3.up * Time.deltaTime * 200f);
                        // this.transform.Translate(Vector3.forward * Time.deltaTime * 13f);
                    //this.GetComponent<Rigidbody>().AddForce(transform.up * 100f);

                    
                    StartCoroutine("climb");


                }


            }
            else
            {
                Hook.transform.parent = hookHolder.transform;
                this.GetComponent<Rigidbody>().useGravity = true;
                //APR_COMP.transform.SetParent(this.transform.parent.transform);
                for (int i = 0; i < 14; i++)
                {
                    //bodys[i].transform.SetParent(this.transform.parent.transform);
                    bodys[i].GetComponent<Rigidbody>().useGravity = true;
                    //bodys[i].GetComponent<Rigidbody>().mass = 1f;
                }
            }
        }
       
    
    }

    IEnumerator climb()
    {
        yield return new WaitForSeconds(0.1f);
        ReturnHook();
        //this.transform.Translate(Vector3.up * Time.deltaTime * 50f);
        this.GetComponent<Rigidbody>().velocity = Vector3.up * 75f;
    }

    public void ReturnHook()
    {
        Hook.transform.rotation = hookHolder.transform.rotation;
        Hook.transform.position = hookHolder.transform.position;
        fired = false;
        hooked = false;

        LineRenderer rope = GetComponent<LineRenderer>();
        rope.SetVertexCount(0);

        
    }

    void CheckIfGrounded()
    {
        RaycastHit hit;
        float distance = 1f;
        Vector3 dir = new Vector3(0, -1);

        if (Physics.Raycast(transform.position, dir, out hit, distance))
        {
            grounded = true;
        }

        else
        {
            grounded = false;
        }
    }

    public void HookFire()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.position, cam.forward, out hit, maxDistance, WhatIsGrappleable))
        {

            //Hook.transform.Translate((hit.point - Hook.transform.position).normalized * Time.deltaTime * hookTravelSpeed);

            targetPoint = hit.point;

            Hook.transform.position = hit.point;
            //hooked = true;
        }

        else
        {
            ReturnHook();
        }
    }
}
