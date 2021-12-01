using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buildable : MonoBehaviour
{
    public bool on = false;
    public bool built = false;
    public Transform spawnPos;
    public Transform tooltip;
    public float tooltipScaleSmoothness;
    public bool highlighted;
    public bool dragging;
    public Vector3 mouseDragOffset;
    public bool dontOffset;
    public bool touchingBuildableOn;
    public Transform currentlyTouchingTransform;
    void Update()
    {
        if (highlighted&&!dragging)
        {
            tooltip.transform.localScale = Vector3.Lerp(tooltip.transform.localScale, Vector3.one, tooltipScaleSmoothness);
        }
        else
        {
            tooltip.transform.localScale = Vector3.Lerp(tooltip.transform.localScale, Vector3.zero, tooltipScaleSmoothness);
        }
        if (dragging)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit,Mathf.Infinity,FindObjectOfType<GameManager>().buildableOn))
            {
                transform.position = hit.point+mouseDragOffset;
                currentlyTouchingTransform = hit.transform;
            }
        }
    }
    private void FixedUpdate()
    {
        GetComponent<Rigidbody>().isKinematic = dragging || touchingBuildableOn;
    }
    private void OnMouseEnter() { highlighted = true; }
    private void OnMouseExit() { highlighted = false; }
    void OnMouseDown()
    {
        RaycastHit hit;
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit);
        if(!dontOffset)mouseDragOffset = transform.position - hit.point;
        dragging = true;
    }
    private void OnMouseUp()
    {
        dragging = false;
        if (touchingBuildableOn)
        {
            var a = FindObjectOfType<GameManager>();
            a.objsNormalScale.RemoveAt(a.buildableObjs.IndexOf(gameObject));
            a.buildableObjs.Remove(gameObject);
            transform.parent = currentlyTouchingTransform;
            built = true;
            Destroy(GetComponent<Rigidbody>());
            Destroy(this);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Buildable On") && other.gameObject.tag != "Table")
        {
            touchingBuildableOn = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        touchingBuildableOn = false;
    }
}
