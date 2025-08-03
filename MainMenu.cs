using Godot;
using System;

public partial class MainMenu : Control
{
// MainMenu.cs on root node
    public override void _Ready()
    {
        GetNode<Button>("Level1Button").Pressed += OnLevelButtonPressed;
    }

    private void OnLevelButtonPressed()
    {
        GD.Print("Level 1 button pressed, changing scene to Level1.tscn");
        GetTree().ChangeSceneToFile("res://Level1.tscn");
    }
}