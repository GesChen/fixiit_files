using UnityEngine;

public class MusicBox : MonoBehaviour
{
    public Buildable pin1;
    public Buildable pin2;
    public Buildable pin3;
    public Buildable pin4;
    public Interactable lid;
    public Interactable latch;
    public GameManager gm;
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (pin1==null && pin2==null && pin3==null && pin4==null&&!lid.toggled&&!latch.toggled)
        {
            gm.winConditionsMet = true;
        }
    }
}
