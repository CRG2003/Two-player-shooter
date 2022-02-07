using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class godModeToggle : MonoBehaviour
{
    bool played = false;
    private Animator intro;

    float godTimer;

    void Start(){
        intro = GetComponent<Animator>();

        bool played = intro.GetBool("played");
    }

    void Update(){
        godTimer -= Time.deltaTime;
        if (Input.GetKey(KeyCode.Q) && godTimer <= 0){
            intro.Play("Base Layer.GodMode");
            godTimer = 0.2f;
            played = true;
        }
    }
}
