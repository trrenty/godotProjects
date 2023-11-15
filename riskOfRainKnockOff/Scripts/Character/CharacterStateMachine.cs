using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class CharacterStateMachine : StateMachine
{
  public override void _Ready()
  {
    IdleState idleState = GetNode<IdleState>("Idle");
    statesMap.Add("idle", idleState);
    Initialize(idleState.GetPath());
  }

  private static readonly HashSet<String> PRIORITY_STATES = new HashSet<string> { "stun", "jump", "attack" };
  public override void ChangeState(String stateName)
  {
    if (!Active)
    {
      return;
    }
    if (PRIORITY_STATES.Contains(stateName))
    {
      this.statesStack.Prepend(statesMap[stateName]);
    }
    if (stateName.Equals("jump"))
    {

    }
  }
}
