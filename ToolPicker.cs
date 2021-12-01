using UnityEngine;
using UnityEngine.UI;
public class ToolPicker : MonoBehaviour
{
    public int currentTool; //0-handtool 1-buildtool
    public Image handToolImage;
    public Image buildToolImage;
    public Image handbookImage;
    public Color normalColor;
    public Color selectedColor;
    public GameManager gm;
    public void changeTool(int tool)
    {
        currentTool = tool;
        handToolImage.color = normalColor;
        buildToolImage.color = normalColor;
        handbookImage.color = normalColor;
        gm.handbookOpen = false;
        if (currentTool == 0)
        {
            StartCoroutine(gm.stopBuilding());
            handToolImage.color = selectedColor;
        }
        else if (currentTool == 1)
        {
            StartCoroutine(gm.startBuilding());
            buildToolImage.color = selectedColor;
        }
        else if (currentTool == 2)
        {
            StartCoroutine(gm.stopBuilding());
            gm.handbookOpen = true;
            handbookImage.color = selectedColor;
        }
    }
}
