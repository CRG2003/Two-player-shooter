using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightController : MonoBehaviour
{
    bool running = false;

    Light myLight;

    void Start()
    {
        myLight = GetComponent<Light>();
    }
    void Update()
    {
        if (running == false) 
        {
            StartCoroutine(dimming());
        }
    }
    IEnumerator dimming()
    {
        running = true;
        yield return new WaitForSeconds(0.2f);
        myLight.intensity = 20;
        yield return new WaitForSeconds(0.04f);
        myLight.intensity = 21;
        yield return new WaitForSeconds(0.04f);
        myLight.intensity = 22;
        yield return new WaitForSeconds(0.04f);
        myLight.intensity = 23;
        yield return new WaitForSeconds(0.04f);
        myLight.intensity = 24;
        yield return new WaitForSeconds(0.04f);
        myLight.intensity = 25;
        yield return new WaitForSeconds(0.04f);
        myLight.intensity = 26;
        yield return new WaitForSeconds(0.04f);
        myLight.intensity = 27;
        yield return new WaitForSeconds(0.04f);
        myLight.intensity = 28;
        yield return new WaitForSeconds(0.04f);
        myLight.intensity = 29;
        yield return new WaitForSeconds(0.04f);
        myLight.intensity = 30;
        running = false;
    }
}
