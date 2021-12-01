using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableLimit : MonoBehaviour
{
    public Interactable limiter;
    public Interactable limited;
    void Update()
    {
        limited.toggleEnabled = limiter.toggled;
    }
}
