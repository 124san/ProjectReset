using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TurnHandler : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    public TurnEvent[] events;

    void Start()
    {
        TurnManager.instance.onTurnIncremented.AddListener(InvokeTurnEvent);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncrementTurn() {
        TurnManager.instance.incrementTurn();
    }

    public void InvokeTurnEvent() {
        int turn = TurnManager.instance.getCurrentTurn();
        foreach(TurnEvent thisEvent in events) {
            if (thisEvent.turn == turn) {
                Debug.Log(thisEvent);
                thisEvent.TriggeredEvent.Invoke();
                break;
            }
        }
    }
}
