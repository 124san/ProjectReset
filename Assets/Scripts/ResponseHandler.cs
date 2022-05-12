using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class ResponseHandler : MonoBehaviour
{
    [SerializeField] RectTransform responseBox;
    [SerializeField] RectTransform responseButtonTemplate;
    [SerializeField] RectTransform responseContainer;
    DialogueUI dialogueUI;
    ResponseEvent[] responseEvents;
    List<GameObject> tempButtons = new List<GameObject>();
    public void ShowResponses(Response[] responses) {
        float responseBoxHeight = 0;

        for (int i = 0; i < responses.Length; i++)
        {
            Response response = responses[i];
            int responseIndex = i;
            GameObject responseButton = Instantiate(responseButtonTemplate.gameObject, responseContainer);
            responseButton.gameObject.SetActive(true);
            responseButton.GetComponentInChildren<TMP_Text>().text = response.ResponseText;
            responseButton.GetComponent<Button>().onClick.AddListener(() => OnPickedResponse(response, responseIndex));
            tempButtons.Add(responseButton);

            responseBoxHeight += responseButtonTemplate.sizeDelta.y;
        }

        responseBox.sizeDelta = new Vector2(responseBox.sizeDelta.x, responseBoxHeight);
        responseBox.gameObject.SetActive(true);
    }

    void OnPickedResponse(Response response, int responseIndex) {
        responseBox.gameObject.SetActive(false);
        foreach (GameObject button in tempButtons) {
            Destroy(button);
        }
        tempButtons.Clear();
        if (responseEvents != null && responseIndex <= responseEvents.Length) {
            responseEvents[responseIndex].OnPickedResponse?.Invoke();
        }
        responseEvents = null;
        if (response.DialogueObject)
            dialogueUI.ShowDialogue(response.DialogueObject);
        else {
            dialogueUI.CloseDialogueBox();
        }
    }
    void Start()
    {
        dialogueUI = GetComponent<DialogueUI>();
    }
    public void AddResponseEvents(ResponseEvent[] responseEvents) {
        this.responseEvents = responseEvents;
    }
}
