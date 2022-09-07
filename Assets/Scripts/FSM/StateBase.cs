using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateBase
{
    protected string _name = "";

    public virtual void Enter(string[] args)
    {
        Debug.Log("Entering State: " + _name);
    }
    public virtual void Exit()
    {
        Debug.Log("Exiting State: " + _name);
    }
    public virtual void Update()
    {
        // Debug.Log("Updating State: " + _name);
    }
}

public class PlayingState : StateBase
{
    public PlayingState()
    {
        this._name = "Playing";
    }

    public override void Enter(string[] args)
    {
        Debug.Log("Entering Playing State");
        if (args != null && Array.IndexOf(args, "reset") > -1) GameManager.instance?.Prepare();
        GameManager.instance?.Play();
    }

    public override void Exit()
    {
        base.Exit();
        GameManager.instance.Stop();
    }

    public override void Update()
    {
        base.Update();
        if(Input.GetKeyUp(KeyCode.P))
        {
            StateMachine.instance.Pause();
        }

        if(Input.GetKeyUp(KeyCode.Escape))
        {
            StateMachine.instance.Menu();
        }
    }
}

public class MenuState : StateBase
{
    public MenuState()
    {
        this._name = "Menu";
    }

    public override void Enter(string[] args)
    {
        base.Enter(args);
        GameManager.instance?.Menu();
    }

    public override void Update()
    {
        base.Update();
    
    }

    public override void Exit()
    {
        base.Exit();
        GameManager.instance?.ShowMainMenu(false);
    }
}

public class PausedState : StateBase
{
    public PausedState()
    {
        this._name = "Paused";
    }

    public override void Enter(string[] args)
    {
        base.Enter(args);
        GameManager.instance.Pause(true);
    }

    public override void Exit() {
        base.Exit();
        GameManager.instance.Pause(false);
    }

    public override void Update()
    {
        base.Update();
        if(Input.GetKeyUp(KeyCode.P))
        {
            StateMachine.instance.Play();
        }
    }
}

public class ResetPositionState : StateBase
{
    public ResetPositionState()
    {
        this._name = "ResetPosition";
    }

    public override void Enter(string[] args)
    {
        base.Enter(args);
        GameManager.instance.Reset();
        StateMachine.instance.Play();
    }
}

public class GameOverState : StateBase
{
    public GameOverState()
    {
        this._name = "GameOver";
    }

    public override void Enter(string[] args)
    {
        base.Enter(args);
        GameManager.instance.GameOver();
    }

    public override void Update()
    {
        base.Update();
        if (Input.GetKeyUp(KeyCode.R))
        {
            StateMachine.instance.Menu();
        }
    }

    public override void Exit()
    {
        base.Exit();
        GameManager.instance.ShowGameOver(false);
    }
}