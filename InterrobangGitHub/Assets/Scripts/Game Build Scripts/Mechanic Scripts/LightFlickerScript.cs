using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlickerScript : MonoBehaviour
{

    Light pointLight;
    public float flickerSpeed;
    int flickerMode;
    Animator anim;
    public GameObject flame;

    // Use this for initialization
    void Start()
    {
        anim = flame.GetComponent<Animator>();
        pointLight = gameObject.GetComponent<Light>();
        flickerMode = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Shrink"))
        {
            flickerMode = 0;
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Expand"))
        {
            flickerMode = 1;
        }
   
        switch (flickerMode)
        {
            case (0):
                pointLight.range += flickerSpeed * Time.deltaTime;
                break;
            case (1):
                pointLight.range -= flickerSpeed * Time.deltaTime;
                break;
        }
    }

    
}
