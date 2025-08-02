using Godot;
using System;
using System.Collections.Generic;

public partial class Main : Node2D
{
	private Sprite2D _cursor;
	private Sprite2D _cursorGhost;

	private List<Vector2> mousePositions = new();
	private List<Vector2> lastMousePositions = new();
	private int currentTimeIndex = 0;
	private double timeAccumulator = 0.0;
	//private const double sampleRate = 0.1; // Every 0.1 seconds
	
	public override void _Ready()
	{
		_cursor = GetNode<Sprite2D>("Cursor");
		_cursorGhost = GetNode<Sprite2D>("CursorGhost");
	}
	
	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("ui_select"))
		{
			GD.Print("Spacebar was pressed!");
			currentTimeIndex = 0;
			lastMousePositions = mousePositions;
			mousePositions = new();
		}
		Vector2 mousePosition = GetGlobalMousePosition();
		mousePositions.Add(mousePosition);
		_cursor.Position = mousePosition;
		if (currentTimeIndex < lastMousePositions.Count) {
			_cursorGhost.Position = lastMousePositions[currentTimeIndex];
		}
		currentTimeIndex++;

		GD.Print("Mouse Position: ", mousePosition);
	}
}
