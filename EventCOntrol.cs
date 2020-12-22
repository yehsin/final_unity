using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventCOntrol : MonoBehaviour
{
    public GrappingRope grap;
    public hook Hook;

    public int mode;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        print(mode);

        if(Input.GetMouseButtonDown(0))
        {
            grap.ReturnHook();
            
            mode = 0;

            Hook.HookFire();
        }

        else if (Input.GetMouseButtonDown(1))
        {
            Hook.ReturnHook();
            
            mode = 1;
            
            grap.StartGrapple();
        }

        
    }
}
