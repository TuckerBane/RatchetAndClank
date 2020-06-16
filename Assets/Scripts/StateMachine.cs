using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    StateMachine owner;
    public State(StateMachine owner) { this.owner = owner; }

    public virtual void OnEnter(State oldState) { }
    public virtual void OnUpdate() { }
    public virtual void OnExit(State newState) { }
    public System.Guid GetTypeGUID()
    {
        return this.GetType().GUID;
    }
}

public class StateMachine : MonoBehaviour {

    State currentState = null;
    State nextState = null;

    Dictionary<System.Guid, State> typeToInstance;

    void AddStateToMachine(State state)
    {
        typeToInstance.Add(state.GetTypeGUID(), state);
    }

	// Use this for initialization
	void Start () {
        typeToInstance = new Dictionary<System.Guid, State>();
        // add all states here
    }

    // Update is called once per frame
    void Update () {
		if(nextState != null)
        {
            currentState.OnExit(nextState);
            State oldState = currentState;
            currentState = nextState;
            nextState = null;
            currentState.OnEnter(oldState);
        }
        if(currentState != null)
            currentState.OnUpdate();
	}
}
