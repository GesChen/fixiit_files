using UnityEngine;
public class Handbook : MonoBehaviour
{
    public GameObject[] pages;
    public GameObject prevButton;
    public GameObject nextButton;
    public int currentPage;
    void Update()
    {
        foreach(GameObject page in pages) page.SetActive(false);
        pages[currentPage].SetActive(true);

        prevButton.SetActive(true);
        nextButton.SetActive(true);
        if (currentPage <= 0)  prevButton.SetActive(false);
        if (currentPage >= pages.Length - 1) nextButton.SetActive(false);
    }
    public void nextPage() { currentPage += 1; }
    public void prevPage() { currentPage -= 1; }
}
