using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameManager gm;
    public int currentSlide;
    public GameObject[] slides;
    public GameObject slide2button;
    public GameObject slide3button;
    public GameObject slide4button;
    public GameObject slide5button;
    public GameObject slide6button;
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    public void buildToolClicked()
    {
        if (currentSlide == 0) currentSlide++;
    }
    public void bonvoyage()
    {
        gm.dontDecrease = false;
        slide6button.SetActive(false);
    }
    public void next()
    {
        currentSlide++;
    }
    void Update()
    {
        foreach(GameObject obj in slides)
        {
            obj.SetActive(false);
        }
        slides[currentSlide].SetActive(true);
        if (currentSlide == 1) slide2button.SetActive(true);
        else slide2button.SetActive(false);
        if (currentSlide == 2) { slide3button.SetActive(true); gm.dontDecrease = false; }
        else slide3button.SetActive(false);
        if (currentSlide == 3) { slide4button.SetActive(true); gm.dontDecrease = true; }
        else slide4button.SetActive(false);
        if (currentSlide == 4) slide5button.SetActive(true);
        else slide5button.SetActive(false);
        if (currentSlide == 5) slide6button.SetActive(true);
        else slide6button.SetActive(false);
    }
}
