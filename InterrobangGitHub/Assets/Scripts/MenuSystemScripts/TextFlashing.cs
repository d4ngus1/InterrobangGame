using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextFlashing : MonoBehaviour
{

    Text text;

    public float alpha = 0.1f;
    [Range(1, 10)]
    public float alphaChangeSpeed = 5.0f;

    bool alphaIncrease = true;

    private void Start()
    {
        //find the text component the script is attached to 
        text = GetComponent<Text>();
        //set the starting colour up 
        text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
    }
    // Update is called once per frame
    void Update()
    {
        //update the alpha channel 
        ChangeAlphaChannel();

        //update the text to the new alpha set
        text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
    }

    void ChangeAlphaChannel()
    {
        //switches the bool from true to false
        if (alpha >= 1) alphaIncrease = false;
        if (alpha <= 0) alphaIncrease = true;

        //adds and decreased the amount of alpha on the text
        if (alphaIncrease == false) alpha -= alphaChangeSpeed * Time.deltaTime;
        if (alphaIncrease == true) alpha += alphaChangeSpeed * Time.deltaTime;
    }
}
