using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TurnHandler : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    public TurnEvent[] events;

    private UnityEvent[] eventList;

    void Start()
    {
        eventList = new UnityEvent[TurnManager.instance.getMaxTurn()];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncrementTurn() {
        TurnManager.instance.incrementTurn();
    }

    public void InvokeTurnEvent(int turn) {
        foreach(TurnEvent thisEvent in events) {
            if (thisEvent.turn == turn) {
                Debug.Log(thisEvent);
                thisEvent.TriggeredEvent.Invoke();
                break;
            }
        }
    }
}
