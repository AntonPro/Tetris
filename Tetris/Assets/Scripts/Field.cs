using UnityEngine;
using System.Collections;

public class Field : MonoBehaviour {

	public static int width = 10;
	public static int height = 25;
	public static int points;

	public static Transform[,] field = new Transform[width,height];

	//round value vector2 because of Unity
	public static Vector2 RoundVector2(Vector2 vec) {
		return new Vector2(Mathf.Round(vec.x), Mathf.Round(vec.y));
	}

	//check position, is it inside borders?
	public static bool CheckBorders(Vector2 pos) {
		return ((int)pos.x >= 0 && (int)pos.x < width && (int)pos.y >= 0);
	}

	//delete row and values in array
	public static void RemoveRow(int y) {
		for (int x=0; x<width; x++) {
			Destroy(field[x,y].gameObject);
			field[x,y] = null;
		}
	}

	//lower row one line down
	public static void LowerRow(int y) {
		for (int x=0; x<width; x++) {
			if (field[x,y] != null) {
				//change values
				field[x,y-1] = field[x, y];
				field[x,y] = null;
				//change block's position
				field[x, y-1].position += new Vector3(0, -1, 0);
			}
		}
	}

	//lower all possible rows above
	public static void LowerAllRowsAbove(int y) {
		for (int i=y; i<height; i++)
			LowerRow (i);
	}

	//check row
	public static bool IsRowFull(int y) {
		for (int x=0; x<width; x++)
			if (field [x, y] == null)
				return false;
		return true;
	}

	//remove all full rows in field, change y value after each removing
	public static void RemoveFullRows() {
		int count = 0;
		for (int y=0; y<height; y++) {
			if (IsRowFull(y)) {
				count++;	//count amount of rows
				RemoveRow(y);
				LowerAllRowsAbove(y+1);
				y--;
			}
		}

		//add points
		switch(count) 
		{
			case 1: points += 100;
					break;
			case 2: points += 300;
					break;
			case 3: points += 1000;
					break;
			case 4: points += 1500;
					break;
			default:
					break;
		}

		//show points
		GameObject.Find ("score").GetComponent<TextMesh> ().text = points.ToString();

		//change fall speed
		if (points < 300)
			Figure.speedFall = 1f;
		else if (points >= 300 && points < 500)
			Figure.speedFall = 0.5f;
		else if (points >= 500)
			Figure.speedFall = 0.25f;
	}
}