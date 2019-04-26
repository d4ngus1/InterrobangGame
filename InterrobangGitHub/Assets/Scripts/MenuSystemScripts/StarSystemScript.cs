using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Diagnostics;
using System.Threading;
using Debug = UnityEngine.Debug;


public class StarSystemScript : MonoBehaviour
{
	public Stopwatch timing;
    public Sprite bronzeStar, silverStar, goldStar;
    public GameObject starSystem, timeStar;
    public Text timeStarText, currentTimeText;
    public double goldStarTime, silverStarTime;
    public GameObject characterSwitchStar;
    public Text switchStarText, currentSwitchText;
    public int goldSwitches, silverSwitches;
    public GameObject collectableStar;
    public Text collectableStarText, currentCollectableText;
    public int goldCollectablesAmount, silverCollectablesAmount;
    [HideInInspector]
    public bool showStarSystem;
    double timeInSeconds;
    int amountOfCharacterSwitches, amountOfCollectables;
    Image timeStarImage, characterSwitchStarImage, collectableStarImage;

    // Use this for initialization
    void Start()
    {
		timing = new Stopwatch();
		timing.Start();
        timeStarImage = timeStar.GetComponent<Image>();
        characterSwitchStarImage = characterSwitchStar.GetComponent<Image>();
        collectableStarImage = collectableStar.GetComponent<Image>();

        //sets up the text with the inputed values from the inspector
        timeStarText.text = "Gold: " + goldStarTime + "\nSilver: " + silverStarTime;
        switchStarText.text = "Gold: " + goldSwitches + "\nSilver: " + silverSwitches;
        collectableStarText.text = "Gold: " + goldCollectablesAmount + "\nSilver: " + silverCollectablesAmount;
    }

    // Update is called once per frame
	void Update()
    {
        //gets the amount of character switches from the character switch script
        amountOfCharacterSwitches = GameObject.FindObjectOfType<SwitchingCharacters>().amountOfCharacterSwitches;

        //gets the amount of collectables from the manager
        amountOfCollectables = GameObject.FindObjectOfType<CollectableStarManager>().totalNumberOfCollectables;

        //when the collectable has been shown start the star system
        if (showStarSystem)
        {
			timing.Stop();
            //star for how quickly a level is complete
            TimeStar();
            //star for how many times the player has switched between the characters
            CharacterSwitchStar();
            //star for the amount of collectables the player has got
            CollectableStar();
        }

        Debug.Log(amountOfCharacterSwitches);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "zombie")
        {

            //sets the text to the current values to be shown to the player 
			currentTimeText.text = "Time Star" + "\nYour Time: " + Math.Round(timing.Elapsed.TotalSeconds);
            currentSwitchText.text = "Character Switches Star" + "\nYour Character Switches: " + amountOfCharacterSwitches;
            currentCollectableText.text = "Collectables Star" + "\nCollectables Found: " + amountOfCollectables;
        }
    }

    private void TimeStar()
    {
		timeInSeconds = timing.Elapsed.TotalSeconds;
        //checks the time against the stars and set the sprite to the correct star image
        if (timeInSeconds < goldStarTime)
        {
            timeStarImage.sprite = goldStar;
        }
        else if (timeInSeconds > goldStarTime && timeInSeconds < silverStarTime)
        {
            timeStarImage.sprite = silverStar;
        }
        else if (timeInSeconds > silverStarTime)
        {
            timeStarImage.sprite = bronzeStar;
        }

        timeStarImage.GetComponent<Animator>().SetTrigger("ShowStar");
    }

    private void CharacterSwitchStar()
    {
        if (amountOfCharacterSwitches <= goldSwitches)
        {
            characterSwitchStarImage.sprite = goldStar;
        }
        else if (amountOfCharacterSwitches > goldSwitches && amountOfCharacterSwitches <= silverSwitches)
        {
            characterSwitchStarImage.sprite = silverStar;
        }
        else if (amountOfCharacterSwitches > silverSwitches)
        {
            characterSwitchStarImage.sprite = bronzeStar;
        }

        characterSwitchStarImage.GetComponent<Animator>().SetTrigger("ShowStar");
    }

    private void CollectableStar()
    {
        if (amountOfCollectables >= goldCollectablesAmount)
        {
            collectableStarImage.sprite = goldStar;
        }
        else if (amountOfCollectables < goldCollectablesAmount && amountOfCollectables >= silverCollectablesAmount)
        {
            collectableStarImage.sprite = silverStar;
        }
        else if (amountOfCollectables < silverCollectablesAmount)
        {
            collectableStarImage.sprite = bronzeStar;
        }

        collectableStarImage.GetComponent<Animator>().SetTrigger("ShowStar");
    }
}
