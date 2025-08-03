using Godot;
using System;

public partial class VictoryBox : Area2D
{
    public void OnClick(String color)
    {
        if (color == "blue")
        {
            GD.Print("Level complete!"); 
            GetTree().ChangeSceneToFile("res://control.tscn");
        }
    }
}
