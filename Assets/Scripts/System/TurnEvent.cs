using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class TurnEvent
{
    [HideInInspector] public string name;
    [SerializeField] public int turn;
    [SerializeField] private UnityEvent triggeredEvent;


    public UnityEvent TriggeredEvent => triggeredEvent;
}
