using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class InteractionFilter : MonoBehaviour
{
    private XRBaseInteractable _interactable;

    private void Awake()
    {
        // Get the parent XRBaseInteractable component.
        _interactable = GetComponentInParent<XRBaseInteractable>();
    }

    public bool IsSelectableBy(XRBaseInteractor interactor)
    {
        // Access the interactable component and perform checks.
        if (_interactable != null)
        {
            Debug.Log(_interactable, _interactable);
        }

        return false; // Return true if the interactor is allowed to interact, false otherwise.
    }
}
