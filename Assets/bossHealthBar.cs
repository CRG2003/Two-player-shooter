using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossHealthBar : MonoBehaviour
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
        if (world.GetComponent<worldController>().stage == "Boss"){
            rect.anchoredPosition = pos;
            rect.anchoredPosition = new Vector2(pos.x - (100f - world.GetComponent<worldController>().bossHealth)*12.4f, -504);
        }
        else{
            rect.anchoredPosition = new Vector2(-2000, -2000);
        }
    }
}
