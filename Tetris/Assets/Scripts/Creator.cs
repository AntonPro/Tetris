using UnityEngine;
using System.Collections;

public class Creator : MonoBehaviour {
	
	public GameObject[] figures;

	// Use this for initialization
	void Start () {
		Create ();
	}

	public void Create() {
		int i = Random.Range (0, figures.Length);
		Instantiate (figures[i], transform.position, Quaternion.identity);
	}
}