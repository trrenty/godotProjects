using Godot;
using System;

public partial class State : Node
{
    [Signal]
    public delegate void FinishedEventHandler(State nextState);

    public void Enter() {
        return;
    }

    public void Exit() {
        return;
    }

    public void HandleInput(InputEvent eventToHandle) {
        return;
    }

    public void Update(double delta) {
        return;
    }

    public void OnAnimationFinished(String animationName) {
        return;
    }

}
