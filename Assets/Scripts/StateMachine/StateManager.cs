using System;
using UnityEngine;
using System.Collections.Generic;

public abstract class StateManager<EState> : MonoBehaviour where EState : Enum
{
    protected Dictionary<EState , BaseState<EState>> States = new Dictionary<EState, BaseState<EState>>();
    protected BaseState<EState> CurrentState;
    protected bool IsTransitioning = false;

    void Start(){
        CurrentState.EnterState();
    }
    void Update(){
        EState nextStateKey = CurrentState.GetNextState();
        if(!IsTransitioning && nextStateKey.Equals(CurrentState.StateKey)){
            CurrentState.UpdateState();
        } else if (!IsTransitioning) {
            TransitionToState(nextStateKey);
        }
    }

    void TransitionToState(EState nextStateKey){
        IsTransitioning = true;
        CurrentState.ExitState();
        CurrentState = States[nextStateKey];
        CurrentState.EnterState();
        IsTransitioning = false;
    }

    void OnTriggerEnter(Collider other){
        CurrentState.OnTriggerEnter(other);
    }
    void OnTriggerStay(Collider other){
        CurrentState.OnTriggerStay(other);
    }
    void OnTriggerExit(Collider other){
        CurrentState.OnTriggerExit(other);
    }
}
