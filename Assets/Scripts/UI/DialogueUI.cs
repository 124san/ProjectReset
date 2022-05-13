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
    // Next indicator to indicate player can move to next dialogue
    [SerializeField] GameObject nextIndicator;

    // Dialogue Box and text label for no bubble
    [SerializeField] GameObject dialogueBoxNoBubble;
    [SerializeField] TMP_Text textLabelNoBubble;
    // Next indicator to indicate player can move to next dialogue
    [SerializeField] GameObject nextIndicatorNoBubble;
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
                // Disable the indicator that tells player to go to next dialogue
                nextIndicator.SetActive(false);
                nextIndicatorNoBubble.SetActive(false);
                
                var currentDialogue = dialogueObject.Dialogues[i];
                var currentLabel = textLabel;
                var currNextIndicator = nextIndicator;
                // Update Dialogue box position based on bubble target
                switch (currentDialogue.bubbleTarget)
                {
                    case DialogueBubbleTarget.Player:
                        dialogueBox.SetActive(true);
                        dialogueBoxNoBubble.SetActive(false);
                        var playerScreenPos = CameraPivot.instance.WorldToScreenPointCenterPivot(PlayerManager.instance.transform.position);
                        dialogueBox.GetComponent<RectTransform>().anchoredPosition = playerScreenPos;
                        break;
                    case DialogueBubbleTarget.Initiator:
                        dialogueBox.SetActive(true);
                        dialogueBoxNoBubble.SetActive(false);
                        var initScreenPos = CameraPivot.instance.WorldToScreenPointCenterPivot(initiator.transform.position);
                        dialogueBox.GetComponent<RectTransform>().anchoredPosition = initScreenPos;
                        break;
                    
                    default:
                        // Use the no bubble dialogue box instead of bubble box
                        dialogueBox.SetActive(false);
                        dialogueBoxNoBubble.SetActive(true);
                        currentLabel = textLabelNoBubble;
                        currNextIndicator = nextIndicatorNoBubble;
                        break;
                }
                string dialogue = currentDialogue.dialogueText;
                yield return RunTypingEffect(dialogue, currentLabel);
                currentLabel.text = dialogue;
                if (i == dialogueObject.Dialogues.Length - 1 && dialogueObject.HasResponses) break;
                yield return null;
                currNextIndicator.SetActive(true);
                // Wait until player input, currently it is a placeholder
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            }
            // Old dialogue code
            else {
                dialogueBox.SetActive(false);
                dialogueBoxNoBubble.SetActive(true);
                string dialogue = dialogueObject.DialogueOld[i];
                yield return RunTypingEffect(dialogue, textLabelNoBubble);
                textLabelNoBubble.text = dialogue;
                if (i == dialogueObject.DialogueOld.Length - 1 && dialogueObject.HasResponses) break;
                yield return null;
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            }
        }
        if (dialogueObject.HasResponses) {
            if (dialogueBox.activeInHierarchy)
                responseHandler.ShowResponses(dialogueObject.Responses, true);
            else responseHandler.ShowResponses(dialogueObject.Responses, false);
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
    private IEnumerator RunTypingEffect(string dialogue, TMP_Text label) {
        typewriterEffect.Run(dialogue, label);
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
        dialogueBoxNoBubble.SetActive(false);
        textLabel.text = string.Empty;
        textLabelNoBubble.text = string.Empty;
        nextIndicator.SetActive(false);
        nextIndicatorNoBubble.SetActive(false);
    }
}
