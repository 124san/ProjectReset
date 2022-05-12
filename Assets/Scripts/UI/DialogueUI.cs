using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum DialogueBubbleTarget {
    NoTarget = 0,
    Player = 1,
    Initiator = 2
}

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
    // Current Initiator of the dialogue
    private GameObject initiator;
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
    public void ShowDialogue(DialogueData dialogueObject, GameObject initiator = null) {
        isOpen = true;
        dialogueBox.SetActive(true);
        if (initiator != null) {
            this.initiator = initiator;
        }
        StartCoroutine(StepThroughDialogue(dialogueObject));
    }
    // Go through each string in dialogue
    private IEnumerator StepThroughDialogue(DialogueData dialogueObject) {

        for (int i = 0; i < Mathf.Max(dialogueObject.Dialogues.Length, dialogueObject.DialogueOld.Length); i++) {
            // New
            if (dialogueObject.Dialogues?.Length >= 1) {
                var currentDialogue = dialogueObject.Dialogues[i];
                // Update Dialogue box position based on bubble target
                switch (currentDialogue.bubbleTarget)
                {
                    case DialogueBubbleTarget.Player:
                        var playerScreenPos = CameraPivot.instance.WorldToScreenPointCenterPivot(PlayerManager.instance.transform.position);
                        dialogueBox.GetComponent<RectTransform>().anchoredPosition = playerScreenPos;
                        break;
                    case DialogueBubbleTarget.Initiator:
                        var initScreenPos = CameraPivot.instance.WorldToScreenPointCenterPivot(initiator.transform.position);
                        dialogueBox.GetComponent<RectTransform>().anchoredPosition = initScreenPos;
                        break;
                    
                    default:
                        // TODO implement no target
                        break;
                }
                string dialogue = currentDialogue.dialogueText;
                yield return RunTypingEffect(dialogue);
                textLabel.text = dialogue;
                if (i == dialogueObject.Dialogues.Length - 1 && dialogueObject.HasResponses) break;
                yield return null;
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            }
            // Old dialogue code
            else {
                string dialogue = dialogueObject.DialogueOld[i];
                yield return RunTypingEffect(dialogue);
                textLabel.text = dialogue;
                if (i == dialogueObject.DialogueOld.Length - 1 && dialogueObject.HasResponses) break;
                yield return null;
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            }
        }
        if (dialogueObject.HasResponses) {
            responseHandler.ShowResponses(dialogueObject.Responses);
        }
        else {
            CloseDialogueBox();
            if (responseEvent != null) {
                responseEvent.OnPickedResponse?.Invoke();
            }
            responseEvent = null;
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
