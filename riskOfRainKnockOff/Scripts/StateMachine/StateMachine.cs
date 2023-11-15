using Godot;
using Godot.Collections;
using System;
using System.Linq;

public partial class StateMachine : Node
{
    [Signal]
    public delegate void StateChangedEventHandler();

    [Export]
    protected NodePath StartState { get; set; }
    public Dictionary<String, State> statesMap = new Dictionary<string, State>();
    public Array<State> statesStack = new Array<State>();
    public State currentState = null;

    public Boolean Active { set; get; } = false;

    public override void _Ready()
    {
        if (StartState == null)
        {
            StartState = GetChild(0).GetPath();
        }
        foreach (Node child in GetChildren())
        {
            if (child is State)
            {
                ((State)child).Finished += this.ChangeState;
            }
        }
        Initialize(StartState);

    }

    protected virtual void Initialize(NodePath statePath)
    {
        SetActive(true);
        statesStack.Prepend(GetNode(statePath));
        currentState = statesStack[0];
        currentState.Enter();
    }

    protected virtual void SetActive(Boolean isActive)
    {
        Active = isActive;
        SetPhysicsProcess(isActive);
        SetProcessInput(isActive);
        if (!Active)
        {
            statesStack.Clear();
            currentState = null;
        }
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (currentState != null)
        {
            currentState.HandleInput(@event);
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        if (currentState != null)
        {
            currentState.Update(delta);
        }
    }

    public virtual void OnAnimationFinished(String animationName)
    {
        if (!Active)
        {
            return;
        }
        currentState.OnAnimationFinished(animationName);
    }

    public virtual void ChangeState(String stateName)
    {
        if (!Active)
        {
            return;
        }
        currentState.Exit();

        if (stateName.Equals("previous"))
        {
            statesStack.RemoveAt(0);
        }
        else
        {
            statesStack[0] = statesMap[stateName];
            currentState = statesStack[0];
            EmitSignal("state_changed", currentState);

            if (!stateName.Equals("previous"))
            {
                currentState.Enter();
            }

        }
    }

}
