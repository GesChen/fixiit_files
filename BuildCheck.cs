using UnityEngine;
using UnityEngine.UI;
public class BuildCheck : MonoBehaviour
{
    public Sprite canBuildSprite;
    public Sprite cantBuildSprite;
    public Image indicator;
    public LayerMask buildableLayers;
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, buildableLayers) && hit.transform.gameObject.tag != "Table")  indicator.sprite = canBuildSprite;
        else indicator.sprite = cantBuildSprite;
    }
}
