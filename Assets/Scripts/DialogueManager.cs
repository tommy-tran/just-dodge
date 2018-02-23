using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

    Queue<string> sentences;
    public Text nameText;
    public Text dialogueText;
    public Animator anim;
    private bool state; // True when dialogue box is open
    public GameObject continueButton;

	void Awake () {
        sentences = new Queue<string>();
	}

    public void StartDialogue(Dialogue dialogue, bool interactable)
    {
        if (interactable) { continueButton.SetActive(true); } else { continueButton.SetActive(false); }
        anim.SetBool("isOpen", true);
        state = true;
        nameText.text = dialogue.name;
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        if (interactable) { DisplayNextSentence(true); } else { DisplayNextSentence(false); }   
    }

    public void DisplayNextSentence(bool interactable = true)
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines(); // when user continues while sentence is animating
        StartCoroutine(TypeSentence(sentence));
        if (!interactable) StartCoroutine(AutoPlay());
    }

    IEnumerator AutoPlay()
    {
        yield return new WaitForSeconds(3.5f);
        DisplayNextSentence(false);
    }

    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.04f);
        }
    }

    void EndDialogue()
    {
        anim.SetBool("isOpen", false);
        state = false;
    }

    public bool getState()
    {
        return state;
    }
}
