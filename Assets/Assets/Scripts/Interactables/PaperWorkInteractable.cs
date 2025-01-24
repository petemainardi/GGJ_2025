using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperWorkInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private string interactText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Highlight()
    {

    }
    public void UnHighlight()
    {

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
