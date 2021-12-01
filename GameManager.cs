using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [Header("Energy")]
    public float maxEnergy;
    public float currentEnergy;
    public Slider energyGauge;
    public Image energyGaugeFill;
    public Gradient energyGaugeColGradient;
    public float targetCurrentEnergy;
    public float targetSmoothness;
    public float energyAutoDecreaseAmount;
    public bool dontDecrease;

    [Header("Building")]
    //public ToolPicker toolPicker;
    public bool currentlyBuilding;
    public List<GameObject> buildableObjs = new List<GameObject>();
    public List<Vector3> objsNormalScale = new List<Vector3>();
    public float buildableScaleSmoothness;
    public LayerMask buildableOn;
    public TextMeshProUGUI currentlyLookingAtText;
    public LayerMask toFix;

    [Header("Sleep")]
    public Image sleepScreen;
    public List<Image> sleepScreenImgComponents = new List<Image>();
    public List<TextMeshProUGUI> sleepScreenTextComponents = new List<TextMeshProUGUI>();
    public float sleepFadeSmoothness;
    private bool sleepCRinProg;

    [Header("Completion")]
    public bool winConditionsMet = false;
    public Image winScreen;
    public List<Image> winScreenImgComponents = new List<Image>();
    public List<TextMeshProUGUI> winScreenTextComponents = new List<TextMeshProUGUI>();
    public float winScreenFadeSmoothness;
    public Transform sendToCustomerButton;

    [Header("Handbook")]
    public bool handbookOpen;
    public Transform handbookObj;
    public float openSmoothness;
    public Vector3 openSize;
    void Start()
    {
        energyGauge.maxValue = maxEnergy;
        currentEnergy = maxEnergy;
        targetCurrentEnergy = currentEnergy;
        foreach(GameObject obj in buildableObjs)
        {
            objsNormalScale.Add(obj.transform.localScale);
        }
        foreach (Image img in sleepScreen.GetComponentsInChildren<Image>())
        {
            sleepScreenImgComponents.Add(img);
        }
        foreach (TextMeshProUGUI txt in sleepScreen.GetComponentsInChildren<TextMeshProUGUI>())
        {
            sleepScreenTextComponents.Add(txt);
        }
        foreach (Image img in winScreen.GetComponentsInChildren<Image>())
        {
            winScreenImgComponents.Add(img);
        }
        foreach (TextMeshProUGUI txt in winScreen.GetComponentsInChildren<TextMeshProUGUI>())
        {
            winScreenTextComponents.Add(txt);
        }
    }
    void Update()
    {
        energyGauge.value = currentEnergy;
        energyGaugeFill.color = energyGaugeColGradient.Evaluate(currentEnergy/maxEnergy);
        currentEnergy = Mathf.Lerp(currentEnergy, targetCurrentEnergy, targetSmoothness);
        if(!dontDecrease) targetCurrentEnergy -= energyAutoDecreaseAmount;
        if (currentEnergy <= 0)
        {
            StartCoroutine(sleep());
            currentEnergy = maxEnergy;
            targetCurrentEnergy = maxEnergy;
        }
        if (sleepCRinProg) energyGauge.value = 0;

        for (int x = buildableObjs.Count - 1; x >= 0; x--)
        {
            GameObject obj = buildableObjs[x];
            if (!obj.GetComponent<Buildable>().built)
            {
                if (obj.GetComponent<Buildable>().on)
                {
                    obj.transform.localScale = Vector3.Lerp(obj.transform.localScale, objsNormalScale[x], buildableScaleSmoothness);
                }
                else
                {
                    obj.transform.localScale = Vector3.Lerp(obj.transform.localScale, Vector3.zero, buildableScaleSmoothness);
                }
                if (obj.transform.localScale.x >= 0.95f)
                {
                    obj.GetComponent<Rigidbody>().isKinematic = false;
                }
            }

        }
        if (winConditionsMet) sendToCustomerButton.gameObject.SetActive(true);

        if (handbookOpen) handbookObj.localScale = Vector3.Lerp(handbookObj.localScale, openSize, openSmoothness);
        else handbookObj.localScale = Vector3.Lerp(handbookObj.localScale, Vector3.zero, openSmoothness);

        RaycastHit hit;
        if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out hit)&&hit.transform.tag=="Table"){
            currentlyLookingAtText.text = "Currently Looking at: \n"+hit.transform.name;
        }
    }
    public IEnumerator startBuilding()
    {
        currentlyBuilding = true;
        foreach(GameObject obj in buildableObjs)
        {
            obj.GetComponent<Rigidbody>().velocity = Vector3.zero;
            obj.GetComponent<Rigidbody>().isKinematic = true;
            obj.transform.position = obj.GetComponent<Buildable>().spawnPos.position;
            obj.transform.rotation = obj.GetComponent<Buildable>().spawnPos.rotation;
            obj.GetComponent<Buildable>().on = true;
            yield return new WaitForSeconds(0.2f);
        }
    }
    public IEnumerator stopBuilding()
    {
        currentlyBuilding = false;
        foreach (GameObject obj in buildableObjs)
        {
            obj.GetComponent<Buildable>().on = false;
            yield return new WaitForSeconds(0.2f);
        }
    }
    IEnumerator sleep()
    {
        sleepScreen.gameObject.SetActive(true);
        sleepCRinProg = true;
        while (sleepScreen.color.a <= 0.95f)
        {
            foreach (Image img in sleepScreenImgComponents)
            {
                Color iC = new Color(img.color.r,img.color.g,img.color.b, Mathf.Lerp(img.color.a, 1, sleepFadeSmoothness));
                img.color = iC;
            }
            foreach (TextMeshProUGUI txt in sleepScreenTextComponents)
            {
                Color tC = new Color(txt.color.r, txt.color.g, txt.color.b, Mathf.Lerp(txt.color.a, 1, sleepFadeSmoothness));
                txt.color = tC;
            }
            yield return new WaitForSeconds(0.01f);
        }
        foreach (Image img in sleepScreenImgComponents)
        {
            img.color = new Color(img.color.r, img.color.g, img.color.b, 1);
        }
        foreach (TextMeshProUGUI txt in sleepScreenTextComponents)
        {
            txt.color = new Color(txt.color.r, txt.color.g, txt.color.b, 1);
        }
        sleepCRinProg = false;
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Win()
    {
        if (winConditionsMet) StartCoroutine(WinCR());
    }
    IEnumerator WinCR()
    {
        winScreen.gameObject.SetActive(true);
        while (winScreen.color.a <= 0.95f)
        {
            foreach (Image img in winScreenImgComponents)
            {
                Color iC = new Color(img.color.r, img.color.g, img.color.b, Mathf.Lerp(img.color.a, 1, winScreenFadeSmoothness));
                img.color = iC;
            }
            foreach (TextMeshProUGUI txt in winScreenTextComponents)
            {
                Color tC = new Color(txt.color.r, txt.color.g, txt.color.b, Mathf.Lerp(txt.color.a, 1, winScreenFadeSmoothness));
                txt.color = tC;
            }
            yield return new WaitForSeconds(0.01f);
        }
        foreach (Image img in winScreenImgComponents)
        {
            img.color = new Color(img.color.r, img.color.g, img.color.b, 1);
        }
        foreach (TextMeshProUGUI txt in winScreenTextComponents)
        {
            txt.color = new Color(txt.color.r, txt.color.g, txt.color.b, 1);
        }
    }
    public void NextLevel()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        SceneManager.LoadScene(0);
    }
    public void FinishDay()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        SceneManager.LoadScene(4);
    }
}
