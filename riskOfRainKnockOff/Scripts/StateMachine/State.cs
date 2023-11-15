using Godot;
using System;

public partial class State : Node
{
    [Signal]
    public delegate void FinishedEventHandler(String nextState);

    public virtual void Enter() {
        return;
    }

    public virtual void Exit() {
        return;
    }

    public virtual void HandleInput(InputEvent eventToHandle) {
        return;
    }

    public virtual void Update(double delta) {
        return;
    }

    public virtual void OnAnimationFinished(String animationName) {
        return;
    }

}
