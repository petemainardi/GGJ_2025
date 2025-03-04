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

    public void Highlight()
    {
        Renderer render = GetComponentInParent<Renderer>();
        Material mat = render.material;
        mat.SetInt("_LookingAt", 1);
    }
    public void UnHighlight()
    {
        Renderer render = GetComponentInParent<Renderer>();
        Material mat = render.material;
        mat.SetInt("_LookingAt", 0);
    }

    public void Interact(Transform interactorTransform)
    {
        Debug.Log(interactText);
    }


public string GetInteractText()
    {
    return interactText; 
    }
}
