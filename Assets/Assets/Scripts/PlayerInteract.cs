using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private GameInput gameInput;

    [SerializeField] private GameObject player;
    [SerializeField] private Transform holdPos;
    [SerializeField] private Transform cameraTransform;

    //IInteractable highlightInteractable;

    [SerializeField] private float throwForce = 500f; //force at which the object is thrown at
    [SerializeField] private float pickUpRange = 5f; //how far the player can pickup the object from
    private GameObject heldObj; //object which we pick up
    private Rigidbody heldObjRb; //rigidbody of object we pick up
    private bool canDrop = true; //this is needed so we don't throw/drop object when rotating the object
    private int LayerNumber; //layer index
    // Start is called before the first frame update
    void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
        LayerNumber = LayerMask.NameToLayer("holdLayer"); //if your holdLayer is named differently make sure to change this ""
    }

    private void Update()
    {
        /*RaycastHit hit;
        Vector3 forward = cameraTransform.TransformDirection(Vector3.forward);
        if (Physics.Raycast(cameraTransform.position, forward, out hit, pickUpRange))
        {


            if (hit.transform.gameObject.TryGetComponent(out highlightInteractable))
            {
                highlightInteractable.Highlight();
            }
            else
            {
                if (highlightInteractable != null)
                {
                    highlightInteractable.UnHighlight();
                    highlightInteractable = null;
                }
            }
        }
        else
        {
            if (highlightInteractable != null)
            {
                highlightInteractable.UnHighlight();
                highlightInteractable = null;
            }
        }*/
    }


    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        //Debug.DrawRay(cameraTransform.position, cameraTransform.TransformDirection(Vector3.forward) * 3, Color.green, 1);
        if (heldObj == null) //if currently not holding anything
        {
            RaycastHit hit;
            Vector3 forward = cameraTransform.TransformDirection(Vector3.forward);
            Debug.DrawRay(cameraTransform.position, forward, Color.green, 1);
            if (Physics.Raycast(cameraTransform.position, forward, out hit, pickUpRange))
            {


                if (hit.transform.gameObject.TryGetComponent(out IInteractable interactable))
                {
                    if (hit.transform.gameObject.tag == "canPickUp")
                    {
                        PickUpObject(hit.transform.gameObject);
                    }
                    else
                    {
                        interactable.Interact(transform);
                    }



                }
            }
        }
        else //check if placing object
        {
            RaycastHit hit;
            Vector3 forward = cameraTransform.TransformDirection(Vector3.forward);
            if (Physics.Raycast(cameraTransform.position, forward, out hit, pickUpRange))
            {
                Debug.Log("Did hit something");
                if (hit.transform.gameObject.tag == "placeableSpot")
                {
                    Debug.Log(hit.transform.gameObject);
                    PlaceObject(hit.transform.gameObject);
                }
                else
                {
                    if (canDrop == true)
                    {
                        //StopClipping(); //prevents object from clipping through walls
                        DropObject();
                    }
                }

            }
            else
            {
                if (canDrop == true)
                {
                    //StopClipping(); //prevents object from clipping through walls
                    DropObject();
                }
            }
            
        }
        
    }

    void PickUpObject(GameObject pickUpObj)
    {
        if (pickUpObj.GetComponent<Rigidbody>()) //make sure the object has a RigidBody
        {
            heldObj = pickUpObj; //assign heldObj to the object that was hit by the raycast (no longer == null)
            heldObjRb = pickUpObj.GetComponent<Rigidbody>(); //assign Rigidbody
            heldObj.GetComponent<Collider>().enabled = false;
            heldObjRb.isKinematic = true;
            heldObjRb.transform.parent = holdPos.transform; //parent object to holdposition
            heldObjRb.transform.position = holdPos.transform.position;
            heldObj.layer = LayerNumber; //change the object layer to the holdLayer
            //make sure object doesnt collide with player, it can cause weird bugs
            Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), true);
        }
    }
    void DropObject()
    {
        //re-enable collision with player
        Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
        heldObj.layer = 0; //object assigned back to default layer
        heldObj.GetComponent<Collider>().enabled = true;
        heldObjRb.isKinematic = false;
        heldObj.transform.parent = null; //unparent object
        heldObj = null; //undefine game object
    }
    void PlaceObject(GameObject placeSpotObj)
    {
        Debug.Log("PlacingObject");
        //re-enable collision with player
        Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
        heldObj.transform.position = placeSpotObj.transform.position;
        heldObj.transform.rotation = placeSpotObj.transform.rotation;
        heldObj.GetComponent<Collider>().enabled = true;
        heldObj.layer = 0; //object assigned back to default layer
        heldObjRb.isKinematic = false;
        heldObj.transform.parent = null; //unparent object
        heldObj = null; //undefine game object
    }
    void MoveObject()
    {
        //keep object position the same as the holdPosition position
        heldObj.transform.position = holdPos.transform.position;
    }
    void ThrowObject()
    {
        //same as drop function, but add force to object before undefining it
        Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
        heldObj.layer = 0;
        heldObjRb.isKinematic = false;
        heldObj.transform.parent = null;
        heldObjRb.AddForce(transform.forward * throwForce);
        heldObj = null;
    }
    void StopClipping() //function only called when dropping/throwing
    {
        var clipRange = Vector3.Distance(heldObj.transform.position, transform.position); //distance from holdPos to the camera
        //have to use RaycastAll as object blocks raycast in center screen
        //RaycastAll returns array of all colliders hit within the cliprange
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, transform.TransformDirection(Vector3.forward), clipRange);
        //if the array length is greater than 1, meaning it has hit more than just the object we are carrying
        if (hits.Length > 1)
        {
            //change object position to camera position 
            heldObj.transform.position = transform.position + new Vector3(0f, -0.5f, 0f); //offset slightly downward to stop object dropping above player 
            //if your player is small, change the -0.5f to a smaller number (in magnitude) ie: -0.1f
        }
    }
}
