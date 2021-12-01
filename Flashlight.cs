using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public Buildable wire;
    public Interactable body;
    public Interactable battery;
    public Interactable head;
    public GameManager gm;
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }
    void Update()
    {
        if (wire == null && !body.toggled && !head.toggled&&!battery.toggled)
        {
            gm.winConditionsMet = true;
        }
    }
}
