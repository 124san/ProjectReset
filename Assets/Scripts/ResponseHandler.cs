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
    List<GameObject> tempButtons = new List<GameObject>();
    // Start is called before the first frame update
    public void ShowResponses(Response[] responses) {
        float responseBoxHeight = 0;

        foreach (Response response in responses) {
            GameObject responseButton = Instantiate(responseButtonTemplate.gameObject, responseContainer);
            responseButton.gameObject.SetActive(true);
            responseButton.GetComponent<TMP_Text>().text = response.ResponseText;
            responseButton.GetComponent<Button>().onClick.AddListener(() => OnPickedResponse(response));
            tempButtons.Add(responseButton);

            responseBoxHeight += responseButtonTemplate.sizeDelta.y;
        }

        responseBox.sizeDelta = new Vector2(responseBox.sizeDelta.x, responseBoxHeight);
        responseBox.gameObject.SetActive(true);
    }

    void OnPickedResponse(Response response) {
        responseBox.gameObject.SetActive(false);
        foreach (GameObject button in tempButtons) {
            Destroy(button);
        }
        dialogueUI.ShowDialogue(response.DialogueObject);
    }
    void Start()
    {
        dialogueUI = GetComponent<DialogueUI>();
    }
}
