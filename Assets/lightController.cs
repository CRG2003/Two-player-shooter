using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightController : MonoBehaviour
{
    bool running = false;

    Light myLight;

    GameObject world;

    void Start()
    {
        myLight = GetComponent<Light>();
        world = GameObject.Find("World");
    }
    void Update()
    {
        if (running == false && world.GetComponent<worldController>().timeStop == false) 
        {
            StartCoroutine(dimming());
        }
        else if (world.GetComponent<worldController>().timeStop == true){
            StartCoroutine(timeFreeze());
        }
    }
    IEnumerator dimming()
    {

        running = true;
        yield return new WaitForSeconds(0.2f);
        if (world.GetComponent<worldController>().timeStop == false){
            yield break;
        }
        myLight.intensity = 20;
        yield return new WaitForSeconds(0.04f);
        myLight.intensity = 21;
        if (world.GetComponent<worldController>().timeStop == false){
            yield break;
        }
        yield return new WaitForSeconds(0.04f);
        myLight.intensity = 22;
        if (world.GetComponent<worldController>().timeStop == false){
            yield break;
        }
        yield return new WaitForSeconds(0.04f);
        myLight.intensity = 23;
        if (world.GetComponent<worldController>().timeStop == false){
            yield break;
        }
        yield return new WaitForSeconds(0.04f);
        myLight.intensity = 24;
        if (world.GetComponent<worldController>().timeStop == false){
            yield break;
        }
        yield return new WaitForSeconds(0.04f);
        myLight.intensity = 25;
        if (world.GetComponent<worldController>().timeStop == false){
            yield break;
        }
        yield return new WaitForSeconds(0.04f);
        myLight.intensity = 26;
        if (world.GetComponent<worldController>().timeStop == false){
            yield break;
        }
        yield return new WaitForSeconds(0.04f);
        myLight.intensity = 27;
        if (world.GetComponent<worldController>().timeStop == false){
            yield break;
        }
        yield return new WaitForSeconds(0.04f);
        myLight.intensity = 28;
        if (world.GetComponent<worldController>().timeStop == false){
            yield break;
        }
        yield return new WaitForSeconds(0.04f);
        myLight.intensity = 29;
        if (world.GetComponent<worldController>().timeStop == false){
            yield break;
        }
        yield return new WaitForSeconds(0.04f);
        myLight.intensity = 30;
        running = false;
    }
    IEnumerator timeFreeze(){
        myLight.color = new Vector4(0f, 0.3f, 1f, 1f);
        yield return new WaitForSeconds(3f);
        myLight.color = new Vector4(0.4411f, 0f, 0.5283f, 1f);
    }
}
