using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] GameObject dialogueBox;
    [SerializeField] TMP_Text textLabel;
    public bool isOpen {get; private set;}
    public static DialogueUI instance;
    private ResponseHandler responseHandler;
    private TypewriterEffect typewriterEffect;
    // Start is called before the first frame update
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

    public void ShowDialogue(DialogueData dialogueObject) {
        isOpen = true;
        dialogueBox.SetActive(true);
        StartCoroutine(StepThroughDialogue(dialogueObject));
    }

    private IEnumerator StepThroughDialogue(DialogueData dialogueObject) {

        for (int i = 0; i < dialogueObject.Dialogue.Length; i++) {
            string dialogue = dialogueObject.Dialogue[i];
            yield return typewriterEffect.Run(dialogue, textLabel);
            if (i == dialogueObject.Dialogue.Length - 1 && dialogueObject.HasResponses) break;

            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        }
        if (dialogueObject.HasResponses) {
            responseHandler.ShowResponses(dialogueObject.Responses);
        }
        else CloseDialogueBox();
    }

    private void CloseDialogueBox() {
        isOpen = false;
        dialogueBox.SetActive(false);
        textLabel.text = string.Empty;
    }
}
