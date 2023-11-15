using Godot;
using System;

public partial class StateDebug : RichTextLabel
{
  [Export]
  private Node stateMachineHolder;

  private StateMachine stateMachine;

  public override void _Ready()
  {
    if (stateMachineHolder != null)
    {
      stateMachine = stateMachineHolder.GetNode<StateMachine>("StateMachine");
    }
  }

  public override void _Process(double delta)
  {
    if (stateMachine != null)
    {
      for (int i = 0; i < stateMachine.statesStack.Count; i++)
      {
        Text = i + stateMachine.statesStack[i].Name;
      }
    }
  }
}
