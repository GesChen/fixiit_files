using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public bool toggled;
    public bool toggleEnabled = true;
    private Vector3 originalRotation;
    public Vector3 toggledRotation;
    private Vector3 originalPosition;
    public Vector3 toggledPosition;
    private Vector3 originalScale;
    public Vector3 toggledScale;
    private Material originalMat;
    public Material toggledMat;
    public AudioSource toggleSound;
    public float returnWaitTime;
    public bool rotate;
    public bool move;
    public bool scale;
    public bool material;
    public bool doesSound;
    public bool doesReturn;
    public bool smoothTransition;
    public float smoothness;
    public bool animating;
    public GameManager gm;
    void Start()
    {
        originalRotation = transform.localRotation.eulerAngles;
        originalPosition = transform.localPosition;
        originalScale = transform.localScale;
        originalMat = GetComponent<MeshRenderer>().material;
        gm = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (smoothTransition)
        {
            if (toggled)
            {
                if(rotate) transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(toggledRotation), smoothness);
                if(move) transform.localPosition = Vector3.Lerp(transform.localPosition, toggledPosition, smoothness);
                if(scale) transform.localScale = Vector3.Lerp(transform.localScale, toggledScale, smoothness);
            }
            else
            {
                if(rotate) transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(originalRotation), smoothness);
                if(move) transform.localPosition = Vector3.Lerp(transform.localPosition, originalPosition, smoothness);
                if(scale) transform.localScale = Vector3.Lerp(transform.localScale, originalScale, smoothness);
            }
        }
        else
        {
            if (toggled)
            {
                if(rotate) transform.localRotation = Quaternion.Euler(toggledRotation);
                if(move) transform.localPosition = toggledPosition;
                if(scale) transform.localScale = toggledScale;
            }
            else
            {
                if(rotate) transform.localRotation = Quaternion.Euler(originalRotation);
                if(move) transform.localPosition = originalPosition;
                if(scale) transform.localScale = originalScale;
            }
        }
        if (toggled&&material)
        {
            GetComponent<MeshRenderer>().material = toggledMat;
        }
        else if(material)
        {
            GetComponent<MeshRenderer>().material = originalMat;
        }
        
    }
    private void OnMouseDown()
    {
        if (!gm.currentlyBuilding&&!gm.handbookOpen)
        {
            if (Input.GetMouseButton(0)&&!animating&&toggleEnabled)
            {
                if (doesReturn) StartCoroutine(waitToReturn(returnWaitTime));
                if (doesSound) toggleSound.Play();
                toggled = !toggled;
                gm.targetCurrentEnergy = gm.currentEnergy - 1;
            }
        }
    }
    IEnumerator waitToReturn(float time)
    {
        animating = true;
        yield return new WaitForSeconds(time);
        if (doesSound) toggleSound.Play();
        toggled = !toggled;
        animating = false;
    }
}
