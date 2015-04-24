using UnityEngine;
using System.Collections;

public class Figure : MonoBehaviour {

	public static float speedFall = 1f;
	private float lastFall = 0;

	// Use this for initialization
	void Start () {
		if (!IsValidGridPos ()) {
			Debug.Log("Game Over");
			Destroy(gameObject);
		}
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.LeftArrow)) {
			transform.position += new Vector3(-1, 0, 0);

			if (IsValidGridPos())
				UpdateGrid();
			else
				transform.position += new Vector3(1, 0, 0);
		} else if (Input.GetKeyDown(KeyCode.RightArrow)) {
			transform.position += new Vector3(1, 0, 0);

			if (IsValidGridPos())
				UpdateGrid();
			else
				transform.position += new Vector3(-1, 0, 0);
		} else if (Input.GetKeyDown(KeyCode.UpArrow)) {
			transform.Rotate(0, 0, -90);

			if (IsValidGridPos())
				UpdateGrid();
			else 
				transform.Rotate(0, 0, 90);
		} else if (Input.GetKeyDown(KeyCode.DownArrow) || Time.time - lastFall >= speedFall) {
			transform.position += new Vector3(0, -1, 0);

			if (IsValidGridPos())
				UpdateGrid();
			else {
				transform.position += new Vector3(0, 1, 0);
				Field.RemoveFullRows();
				FindObjectOfType<Creator>().Create();
				enabled = false;
			}

			lastFall = Time.time;
		}
	}
	
	// Update is called once per frame
	private void UpdateGrid () {
		for (int y=0; y<Field.height; y++)	
			for (int x=0; x<Field.width; x++)
				if (Field.field [x, y] != null)
				if (Field.field [x, y].parent == transform)
					Field.field [x, y] = null;

		foreach (Transform child in transform) {
			Vector2 vec = Field.RoundVector2(child.position);
			Field.field[(int)vec.x, (int)vec.y] = child;
		}
	}

	private bool IsValidGridPos() {
		foreach (Transform child in transform) {
			Vector2 vec = Field.RoundVector2(child.position);

			if (!Field.CheckBorders(vec))
				return false;

			if (Field.field[(int)vec.x, (int)vec.y] != null &&
			    Field.field[(int)vec.x, (int)vec.y].parent != transform)
				return false;
		}
		return true;
	}
}