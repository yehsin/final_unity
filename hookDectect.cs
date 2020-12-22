using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hookDectect : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "Hookable")
        {
            player.GetComponent<hook>().hooked = true;
            player.GetComponent<hook>().hookOBJ = other.gameObject;
        }
    }
}
