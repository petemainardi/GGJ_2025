using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private string interactText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

public void Interact(Transform interactorTransform)
    {
        Debug.Log("I am Box");
    }

public string GetInteractText()
    {
    return interactText; 
    }
}
