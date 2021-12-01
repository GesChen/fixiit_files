using UnityEngine;

public class Billboarding : MonoBehaviour
{
    void Update()
    {
        //transform.LookAt(Camera.main.transform.position);
        transform.rotation = Camera.main.transform.rotation;
    }
}
