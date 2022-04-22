using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    // Panel that holds dialogue
    [SerializeField] GameObject dialogueBox;
    // Text label of the dialogue box
    [SerializeField] TMP_Text textLabel;
    public bool isOpen {get; private set;}
    public static DialogueUI instance;
    private ResponseHandler responseHandler;
    // Event for dialogue with no response
    private ResponseEvent responseEvent;
    private TypewriterEffect typewriterEffect;
    private void Awake() {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else {
            instance = this;
        }
    }
    private void Start() {
        typewriterEffect = GetComponent<TypewriterEffect>();
        responseHandler = GetComponent<ResponseHandler>();
        CloseDialogueBox();
    }
    public void AddResponseEvents(ResponseEvent[] responseEvents) {
        responseHandler.AddResponseEvents(responseEvents);
    }
    public void AddResponseEvent(ResponseEvent responseEvent) {
        this.responseEvent = responseEvent;
    }

    // Open dialogue box and show dialogue
    public void ShowDialogue(DialogueData dialogueObject) {
        isOpen = true;
        dialogueBox.SetActive(true);
        StartCoroutine(StepThroughDialogue(dialogueObject));
    }
    // Go through each string in dialogue
    private IEnumerator StepThroughDialogue(DialogueData dialogueObject) {

        for (int i = 0; i < dialogueObject.Dialogue.Length; i++) {
            string dialogue = dialogueObject.Dialogue[i];
            yield return RunTypingEffect(dialogue);
            textLabel.text = dialogue;
            if (i == dialogueObject.Dialogue.Length - 1 && dialogueObject.HasResponses) break;
            yield return null;
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        }
        if (dialogueObject.HasResponses) {
            responseHandler.ShowResponses(dialogueObject.Responses);
        }
        else {
            if (responseEvent != null) {
                responseEvent.OnPickedResponse?.Invoke();
            }
            responseEvent = null;
            CloseDialogueBox();
        }
    }

    // Press space to stop text early
    private IEnumerator RunTypingEffect(string dialogue) {
        typewriterEffect.Run(dialogue, textLabel);
        while (typewriterEffect.isRunning) {
            yield return null;
            if (Input.GetKeyDown(KeyCode.Space)) {
                typewriterEffect.Stop();
            }
        }
    }
    public void CloseDialogueBox() {
        isOpen = false;
        dialogueBox.SetActive(false);
        textLabel.text = string.Empty;
    }
}
