using Godot;
using System;
using System.Collections.Generic;


public partial class Cursor : Area2D
{
	private int currentTimeIndex = 0;

	[Export]
	public bool isPlayer;
	[Export]
	public string color;
	private bool clicked;
	private List<Vector2> mousePositions = new();
	private List<bool> mouseClicked = new();
	// private List<bool> lastMouseClicked = new();
	// private List<Vector2> lastMousePositions = new();
	
	Texture2D arrowTexture;
	Texture2D clickedTexture;
	Sprite2D _cursor;


	private bool click()
	{
		if (Input.IsActionPressed("click"))
		{
			_cursor.Texture = clickedTexture;
			return true;
		}
		else
		{
			_cursor.Texture = arrowTexture;
			return false;
		}

	}

	private void ghostClick(bool clicked)
	{
		if (clicked)
		{
			_cursor.Texture = clickedTexture;
			
		}
		else
		{
			_cursor.Texture = arrowTexture;		
		}
	}


	public override void _Ready()
	{
		_cursor = GetNode<Sprite2D>("Sprite2D");

		clickedTexture = ResourceLoader.Load<Texture2D>($"res://Sprites/{color}_arrow_clicked.png");
		arrowTexture = ResourceLoader.Load<Texture2D>($"res://Sprites/{color}_arrow.png");
	}
	
	public override void _Process(double delta)
	{
		if (isPlayer)
		{
			Vector2 mousePosition = GetGlobalMousePosition();
			mousePositions.Add(mousePosition);
			Position = mousePosition;

			clicked = click();
			mouseClicked.Add(clicked);
		}

		else
		{
			if (currentTimeIndex < mousePositions.Count)
				{
					Position = mousePositions[currentTimeIndex];
					bool ghost_click = mouseClicked[currentTimeIndex];
					ghostClick(ghost_click);

				}
				currentTimeIndex++;
		}

	}
}
