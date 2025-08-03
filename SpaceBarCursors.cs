using Godot;
using System;
using System.Collections.Generic;

public partial class SpaceBarCursors : Node2D
{
	private Sprite2D _cursor;
	private Sprite2D _cursorGhost;

	private List<Vector2> mousePositions = new();
	private List<bool> mouseClicked = new();
	private List<bool> lastMouseClicked = new();
	private List<Vector2> lastMousePositions = new();
	private int currentTimeIndex = 0;
	private double timeAccumulator = 0.0;

	private string blueArrowSprite = "res://Sprites/blue_arrow.png";
	private Texture2D blueArrowTexture;
	private string blueArrowClicked = "res://Sprites/blue_arrow_clicked.png";
	private Texture2D blueArrowClickedTexture;

	private string redArrowSprite = "res://Sprites/red_arrow.png";
	private Texture2D redArrowTexture;
	private string redArrowClicked = "res://Sprites/red_arrow_clicked.png";
	private Texture2D redArrowClickedTexture;
	bool clicked;
	//private const double sampleRate = 0.1; // Every 0.1 seconds

	private bool click(Sprite2D cursor)
	{
		if (Input.IsActionPressed("click"))
		{
			cursor.Texture = blueArrowClickedTexture;
			return true;
		}
		else
		{
			cursor.Texture = blueArrowTexture;
			return false;
		}

	}

	private void ghostClick(bool clicked)
	{
		if (clicked)
		{
			_cursorGhost.Texture = redArrowClickedTexture;
			
		}
		else
		{
			_cursorGhost.Texture = redArrowTexture;
			
		}
	}

	public override void _Ready()
	{
		_cursor = GetNode<Sprite2D>("Cursor");
		_cursorGhost = GetNode<Sprite2D>("CursorGhost");

		blueArrowClickedTexture = ResourceLoader.Load<Texture2D>(blueArrowClicked);
		blueArrowTexture = ResourceLoader.Load<Texture2D>(blueArrowSprite);

		redArrowClickedTexture = ResourceLoader.Load<Texture2D>(redArrowClicked);
		redArrowTexture = ResourceLoader.Load<Texture2D>(redArrowSprite);

		Input.MouseMode = 0; //Turn off mouse cursor
	}
	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("ui_select"))
		{
			GD.Print("Spacebar was pressed!");
			currentTimeIndex = 0;
			lastMousePositions = mousePositions;
			lastMouseClicked = mouseClicked;
			mousePositions = new();
			mouseClicked = new();

		}

		Vector2 mousePosition = GetGlobalMousePosition();
		mousePositions.Add(mousePosition);
		_cursor.Position = mousePosition;

		clicked = click(_cursor);
		mouseClicked.Add(clicked);

		if (currentTimeIndex < lastMousePositions.Count)
		{
			_cursorGhost.Position = lastMousePositions[currentTimeIndex];
			bool ghost_click = lastMouseClicked[currentTimeIndex];
			ghostClick(ghost_click);

		}
		currentTimeIndex++;

		
		
		// GD.Print("Mouse Position: ", mousePosition);
	}
}
