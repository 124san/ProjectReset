using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    [SerializeField]
    private int maxTurn;

    [SerializeField]
    private int currentTurn;

    public static TurnManager instance;

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
        }
        else {
            instance = this;
            GameObject.DontDestroyOnLoad(gameObject);
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
            foreach (TurnHandler turnHandler in FindObjectsOfType<TurnHandler>()) {
                turnHandler.InvokeTurnEvent(currentTurn);
            }
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
