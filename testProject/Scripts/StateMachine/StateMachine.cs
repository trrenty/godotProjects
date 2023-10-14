using Godot;
using Godot.Collections;
using System;
using System.Linq;

public partial class StateMachine : Node
{
    [Signal]
    public delegate void StateChangedEventHandler();

    [Export]
    private NodePath StartState { get; set; }
    private Dictionary<String, State> statesMap;
    private Array<State> statesStack;
    private State currentState = null;

    private Boolean Active { set; get; } = false;

    public override void _Ready()
    {
        if (StartState == null) {
            StartState = GetChild(0).GetPath();
        }
        foreach (Node child in GetChildren())
        {
            Error err = child.Connect("finished", ChangeState);
            if (err != null) {
                GD.PrintErr(err);
            }  
        }
        Initialize(StartState);
        
    }

    private void Initialize(NodePath statePath) {
        SetActive(true);
        statesStack.Prepend(GetNode(statePath));
        currentState = statesStack[0];
        currentState.Enter();
    }

    private void SetActive(Boolean isActive) {
        Active = isActive;
        SetPhysicsProcess(isActive);
        SetProcessInput(isActive);
        if (!Active) {
            statesStack.Clear();
            currentState = null;
        }
    }

    public override void _UnhandledInput(InputEvent @event)
    {
       currentState.HandleInput(@event);
    }

    public override void _PhysicsProcess(double delta)
    {
        currentState.Update(delta);
    }

    public void OnAnimationFinished(String animationName) {
        if (!Active) {
            return;
        }
        currentState.OnAnimationFinished(animationName);
    }

    public void ChangeState(String stateName) {
        if (!Active) {
            return;
        }
        currentState.Exit();

        if (stateName.Equals("previous")) {
            statesStack.RemoveAt(0);
        } else {
            statesStack[0] = statesMap[stateName];
            currentState = statesStack[0];
            EmitSignal("state_changed", currentState);

            if (!stateName.Equals( "previous")) {
                currentState.Enter();
            }

        }
    }

}
