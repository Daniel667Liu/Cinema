using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameStartState :gameBaseState
{
    public override void EnterState(GameStateManager stateManager)
    {
        stateManager.startUI.SetActive(true);
    }
    public override void UpdateState(GameStateManager stateManager)
    {
        
    }

    public override void ExitState(GameStateManager stateManager)
    {
        stateManager.startUI.SetActive(false);
    }
}
