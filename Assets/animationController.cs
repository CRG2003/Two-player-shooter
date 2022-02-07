using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationController : MonoBehaviour
{
    GameObject world;

    bool played = false;

    private Animator intro;
    void Start()
    {
        world = GameObject.Find("World");
        intro = GetComponent<Animator>();

        bool played = intro.GetBool("boss");
    }


    void Update()
    {
        if (world.GetComponent<worldController>().stage == "Boss" && played == false){
            intro.Play("Base Layer.bossIntro");
            played = true;
        }
    }
}
