using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Interaction : MonoBehaviour
{
    [Header("Grab")]
    public KeyCode interactKey = KeyCode.E;
    public LayerMask Object;
    private bool Touch;
    [SerializeField]
    private Transform CameraTransform;
    [SerializeField]
    private Transform HandPosition;
    
    //fürt Aktion bei Knopfdruck aus
    private void Update()
    {
        if(Input.GetKeyDown(interactKey))
        {
            TryInteract();
        }
    }
    //Aktion: habt mit Raycast getroffenes Objekt auf 
    private void Interact(GameObject hitobject)
    {
        Debug.Log("Interact");
        Touch = true;
        Rigidbody GameObject = hitobject.GetComponent<Rigidbody>();
        GameObject.isKinematic = true;

        hitobject.transform.SetParent(HandPosition.transform);
        //hitobject.localPosition = Vector3.zero;
       
       
    }

    //überprüft dei Ausführung des Raycasts und verwaltet die Aktion
    private void TryInteract()
    {

        bool Found = Physics.Raycast(CameraTransform.position, CameraTransform.forward, out RaycastHit hitinfo, 10f, Object);
        GameObject hitObject = hitinfo.transform.gameObject;
        Debug.DrawRay(CameraTransform.position, CameraTransform.forward * 10, Color.red,10);
        Debug.Log(hitObject.transform);
        if (Touch)
        {
            
            DropObject(hitObject);

            return;
        }
        if ( Found ) 
        {
            Debug.Log("IfFound");
            Interact(hitObject);
        }
        
        
    }

    //Aktion: lässt mit Raycast getroffenes Objekt fallen 
    private void DropObject(GameObject hitobject)
    {
        Debug.Log("Drop");
        hitobject.transform.SetParent(null);

        Rigidbody GameObject = hitobject.GetComponent<Rigidbody>();
        GameObject.isKinematic = false;

        Touch = false;
    }
    
    
}
