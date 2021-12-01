using UnityEngine;

public class ChainInteraction : MonoBehaviour
{
    public Interactable trigger;
    public Interactable target;
    void Update()
    {
        if (trigger.toggled) target.toggled = true;
        else target.toggled = false;
    }
}
