using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TurnManager : MonoBehaviour
{
    [SerializeField]
    private int maxTurn;

    [SerializeField]
    public int currentTurn;

    public static TurnManager instance;
    public UnityEvent onTurnIncremented;

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
        }
        else {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void incrementTurn() {
        if (currentTurn != maxTurn) {
            currentTurn += 1;
            // Send signal to all TurnHandler
            onTurnIncremented.Invoke();
        }
    }

    public void resetTurn() {
        currentTurn = 0;
    }

    public int getCurrentTurn() {
        return currentTurn;
    }

    public int getMaxTurn() {
        return maxTurn;
    }
}
