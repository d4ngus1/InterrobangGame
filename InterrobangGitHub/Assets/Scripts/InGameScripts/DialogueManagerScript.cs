using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class DialogueManagerScript : MonoBehaviour
{
    public Text nameText;
    public Text textInDialogue;
    public Animator animator;
    private Queue<string> typedSentences;


    // Use this for initialization
    void Start()
    {
        typedSentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        //move the dialogue onto the screen
        animator.SetBool("isOpen", true);

        nameText.text = dialogue.name;

        typedSentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            typedSentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }


    public void DisplayNextSentence()
    {
        //if there arent any more sentences to display then end the dialogue
        if (typedSentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        //types the sentences letter by letter 
        string sentence = typedSentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentenceInLetters(sentence));
    }

    IEnumerator TypeSentenceInLetters(string sentence)
    {
        textInDialogue.text = "";

        //for all chars that are stored in the array 
        foreach (char letter in sentence.ToCharArray())
        {
            textInDialogue.text += letter;
            //return after one frame 
            yield return null;
        }
    }

    void EndDialogue()
    {
        //close the dialogue back down
        animator.SetBool("isOpen", false);
    }
}
