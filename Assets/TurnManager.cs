using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TurnManager : MonoBehaviour
{
    [SerializeField]
    private int maxTurn;

    [SerializeField]
    private int currentTurn;

    public static TurnManager instance;
    public UnityEvent onTurnChanged;

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
        }
        else {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        ResetTurn();
    }

    public void incrementTurn() {
        if (currentTurn != maxTurn) {
            currentTurn += 1;
            // Send signal to all TurnHandler
            onTurnChanged.Invoke();
        }
    }

    public void ResetTurn() {
        currentTurn = 1;
        onTurnChanged.Invoke();
    }

    public int getCurrentTurn() {
        return currentTurn;
    }

    public int getMaxTurn() {
        return maxTurn;
    }
}
