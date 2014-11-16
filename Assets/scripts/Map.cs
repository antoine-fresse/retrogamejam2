using UnityEngine;
using System.Collections;
using System.Collections.Generic;


struct Block {
	int x;
	int y;
	int width;
	int length;

	public Block(int a, int b, int w, int l) {
		x = a;
		y = b;
		width = w;
		length = l;
	}

	public bool isInBlock(int i, int j) {
		return i >= x && i <= x + width && j >= y && j <= y + length;
	}
}

public class Map : MonoBehaviour {

	public Transform building;

	public int nbRows = 20;
	public int nbCols = 20;

	public int nbMainStreets = 4;
	public int nbSideStreets = 4;

	
	List<int> mainStreets;
	List<int> sideStreets;

	Block emptyBlock;
	// Use this for initialization
	void Start () {
        Screen.SetResolution(640, 475, true);
		transform.position = new Vector3(-nbRows / 2.0f, -nbCols/2.0f,0.0f);
		mainStreets = new List<int>();
		sideStreets = new List<int>();
		

		for (int n = 0; n < nbMainStreets; n++) {
			int t;
			do {
				t = Random.Range(0, nbRows - 1);
			} while (mainStreets.Contains(t) || mainStreets.Contains(t - 1) || mainStreets.Contains(t + 1) || mainStreets.Contains(t - 2) || mainStreets.Contains(t + 2) || mainStreets.Contains(t - 3) || mainStreets.Contains(t + 3));

			mainStreets.Add(t);
			 
		}

		mainStreets.Sort();
		for (int n = 0; n < nbSideStreets; n++) {
			int t;
			do {
				t = Random.Range(0, nbCols - 1);
			} while (sideStreets.Contains(t) || sideStreets.Contains(t - 1) || sideStreets.Contains(t + 1));

			sideStreets.Add(t);
		}
		sideStreets.Sort();

		emptyBlock = new Block(mainStreets[0], sideStreets[0], mainStreets[1] - mainStreets[0], sideStreets[1] - sideStreets[0]);

		for (int i = 0; i < nbRows; i++) {
			for (int j = 0; j < nbCols; j++) {
				if (!mainStreets.Contains(i) && !sideStreets.Contains(j) && !mainStreets.Contains(i - 1) && !mainStreets.Contains(i + 1) && !emptyBlock.isInBlock(i,j)) {
                    int rot = Mathf.RoundToInt(Random.value * 3.0f);
					var t = (Transform)Instantiate(building, transform.position + new Vector3(i, j, 0), Quaternion.AngleAxis(rot * 90.0f, new Vector3(0.0f, 0.0f, 1.0f)));
					t.parent = transform;
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
