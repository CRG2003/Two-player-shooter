using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class victoryScreen : MonoBehaviour
{
    GameObject world;

    RectTransform rect;

    Vector2 pos;

    void Start()
    {
        world = GameObject.Find("World");
        rect = GetComponent<RectTransform>();
        Vector2 pos = rect.position;
    }

    void Update()
    {
        if (world.GetComponent<worldController>().stage == "victory"){
            rect.anchoredPosition = pos;
        } 
        else{
            rect.anchoredPosition = new Vector2(-2000, -2000);
        }
    }
}
