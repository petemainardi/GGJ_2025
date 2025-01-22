using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private GameInput gameInput;
    float interactRange = 2f;
    // Start is called before the first frame update
    void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
        foreach (Collider collider in colliderArray)
        {
            if (collider.TryGetComponent(out IInteractable interactable))
            {
                interactable.Interact(transform);
            }
        }
    }
}
