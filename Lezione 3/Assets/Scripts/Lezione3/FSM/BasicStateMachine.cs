using System;
using UnityEngine;

namespace MiciomaXD
{
    [Serializable]
    public abstract class BasicStateMachine
    {
        private IBasicState currentState;

        public void SwitchState(IBasicState newState)
        {
            currentState.EndState();
            currentState = newState;
            currentState.BeginState();
        }

        public void MachineUpdate()
        {
            if (currentState != null)
            {
                currentState.UpdateState();
            }
        }

        public void Init(IBasicState initialState)
        {
            currentState = initialState;
            currentState.BeginState();
        }
    }
}