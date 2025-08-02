using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;

public partial class SceneManager : Node2D
{
	List<String> colors = new() { "red", "green", "blue", "yellow", "purple" };
	Stopwatch timekeeper = new();

	[Export]
	public int roundTime = 10;
	int time = 0;
	PackedScene cursorLoader = GD.Load<PackedScene>("res://cursor.tscn");
	private Dictionary<String, Cursor> cursors = new();
	int playerCount = 1;



	private void createCursor(String color)
	{
		Cursor cursor = (Cursor)cursorLoader.Instantiate();
		cursor.color = color;
		cursors[color] = cursor;
		AddChild(cursor);

		
		cursor.isPlayer = true;
	}

	private void checkTime()
	{
	}

	private void newRound()
	{
		timekeeper.Reset();
        foreach (String color in cursors.Keys)
        {
            cursors[color].isPlayer = false;
            cursors[color].currentTimeIndex = 0;
		}
		playerCount = playerCount % colors.Count;
		createCursor(colors[playerCount]);
		timekeeper.Start();

		playerCount++;
		
	}


	public override void _Ready()
	{
		timekeeper.Start();
		newRound();
	}
	public override void _Process(double delta)
	{
		if (timekeeper.ElapsedMilliseconds > roundTime * 1000)
		{
			newRound();
		}

	}

}
