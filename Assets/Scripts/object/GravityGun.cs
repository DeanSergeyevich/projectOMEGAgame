using UnityEngine;

public class GravityGun : MonoBehaviour
{
    public GameObject heldObject;
    private Transform attractor;
    public float pickupDistance = 1.0f;

   // private bool isRightMouseButtonDown = false;
    private Vector3 initialCameraPosition;
    private Quaternion initialCameraRotation;

    // Добавим переменную для передачи ссылки на heldObject
    private RotateObjectScript rotateObjectScript;


    void Start()
    {
        attractor = GameObject.Find("GravityAttractor").transform;

        initialCameraPosition = Camera.main.transform.position;
        initialCameraRotation = Camera.main.transform.rotation;

        // Ищем компонент RotateObjectScript на том же объекте
        rotateObjectScript = GetComponent<RotateObjectScript>();
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (heldObject == null)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, pickupDistance))
                {
                    if (hit.collider.GetComponent<Rigidbody>() != null && !hit.collider.GetComponent<HingeJoint>())
                    {
                        heldObject = hit.collider.gameObject;
                        heldObject.GetComponent<Rigidbody>().useGravity = false;
                        
                        // Передаем ссылку на heldObject скрипту RotateObjectScript
                        rotateObjectScript.SetHeldObject(heldObject);
                    }
                }
            }
            else
            {
                heldObject.GetComponent<Rigidbody>().useGravity = true;
                heldObject = null;

                // Передаем ссылку на heldObject скрипту RotateObjectScript (нулевую ссылку)
                rotateObjectScript.SetHeldObject(null);
            }
            
        }

        if (Input.GetMouseButtonDown(2) && heldObject != null)
        {
            Rigidbody rb = heldObject.GetComponent<Rigidbody>();
            rb.velocity = Camera.main.transform.forward * 10f;
            heldObject.GetComponent<Rigidbody>().useGravity = true;
            heldObject = null;

            // Передаем ссылку на heldObject скрипту RotateObjectScript (нулевую ссылку)
            rotateObjectScript.SetHeldObject(null);
        }

        if (heldObject != null)
        {
            Vector3 targetPosition = attractor.position;
            heldObject.transform.position = Vector3.Lerp(heldObject.transform.position, targetPosition, Time.deltaTime * 10f);
        }

    }

}
