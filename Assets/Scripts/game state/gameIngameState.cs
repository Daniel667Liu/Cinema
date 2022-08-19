using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameIngameState : gameBaseState
{
    public float waitTime = 0f;
    public override void EnterState(GameStateManager stateManager)
    {
        stateManager.audioControl.peopelPlay();
    }
    public override void UpdateState(GameStateManager stateManager)
    {
        if (stateManager.timeGapNow > 0.2f) 
        {
            stateManager.timeGapNow -= 0.03f * Time.deltaTime;
        }
        
        
        waitTime += 1f * Time.deltaTime;
        if (waitTime >= stateManager.timeGapNow) 
        {
            //Debug.Log(waitTime);
            stateManager.gp.randomStandUp();
            waitTime = 0f;
            Debug.Log(stateManager.gp.unselected.Count);
        }

        if (stateManager.gp.unselected.Count <= 1) 
        {
            stateManager.GameFinish();
        }

    }

    public override void ExitState(GameStateManager stateManager)
    {
       
    }
}
