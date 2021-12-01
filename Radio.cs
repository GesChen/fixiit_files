using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : MonoBehaviour
{
    public AudioSource mrbluesky;
    public Interactable onoffswitch;
    public Buildable speaker;
    public Buildable antenna;
    public Interactable antennainter;
    public GameManager gm;
    public bool isplaying;
    void Start() { gm = FindObjectOfType<GameManager>(); }
    void Update()
    {
        if (onoffswitch.toggled && speaker == null && antenna == null && antennainter.toggled && !isplaying) 
        {
            isplaying = true;
            gm.winConditionsMet = true;
            mrbluesky.Play();
        }
        if (!onoffswitch.toggled || !antennainter.toggled)
        {
            isplaying = false;
            mrbluesky.Stop();
        }
    }
}
