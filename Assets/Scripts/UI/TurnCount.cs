using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TurnCount : MonoBehaviour
{
    int currentTurn;
    int maxTurn;
    // Start is called before the first frame update
    void Start()
    {
        TurnManager.instance.onTurnIncremented.AddListener(UpdateTurnCount);
        UpdateTurnCount();
    }

    void UpdateTurnCount() {
        currentTurn = TurnManager.instance.getCurrentTurn();
        maxTurn = TurnManager.instance.getMaxTurn();
        TMP_Text textLabel = GetComponentInChildren<TMP_Text>();
        textLabel.text = $"Turn: {currentTurn}/{maxTurn}";
    }
}
