using Godot;
using System;

public partial class Hud : CanvasLayer
{
  [Signal]
  public delegate void StartGameEventHandler();

  public void ShowMessage(String text)
  {
    Label message = GetNode<Label>("Message");
    message.Text = text;
    message.Show();

    GetNode<Timer>("MessageTimer").Start();
  }

  async public void ShowGameOver()
  {
    ShowMessage("GameOver");

    var messageTimer = GetNode<Timer>("MessageTimer");

    await ToSignal(messageTimer, Timer.SignalName.Timeout);

    var message = GetNode<Label>("Message");
    message.Text = "Dodge the\nCreeps!";
    message.Show();

    await ToSignal(GetTree().CreateTimer(1.0), SceneTreeTimer.SignalName.Timeout);
    GetNode<Button>("StartButton").Show();
  }

  public void UpdateScore(int score)
  {
    GetNode<Label>("ScoreLabel").Text = score.ToString();
  }

  private void OnStartButtonPressed()
  {
    GetNode<Button>("StartButton").Hide();
    EmitSignal(SignalName.StartGame);
  }

  private void OnMessageTimerTimeout()
  {
    GetNode<Label>("Message").Hide();
  }
}
