using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RaycastManager : MonoBehaviour
{

    public float interactionDistance;
    public RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        RaycastHit();
    }

    // Update is called once per frame
    public void RaycastHit()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(transform.position, transform.forward * interactionDistance, Color.yellow);

        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
           
        }
        
    }
    
    // public void Door()
    // {
    //     if (hit.collider != null && hit.collider.gameObject.layer == LayerMask.NameToLayer("Door") && Input.GetMouseButtonDown(0))
    //     {
    //         Debug.Log("yes");
    //     }
    // }
}
