using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generateSegment : MonoBehaviour {
	public GameObject[] pathPrefabs;
	private GameObject[] pathSegments;

	private float endLength = 0;
	private int nextPlatform = 1;

	public Transform player;

	// Use this for initialization
	void Start () {
		pathSegments = new GameObject[pathPrefabs.Length];

		for (int i = 0; i < pathPrefabs.Length; i++) {
			GameObject pathSegment = (GameObject)Instantiate (pathPrefabs[i], transform.position, transform.rotation);
			pathSegment.transform.SetParent (transform);
			pathSegments[i] = pathSegment;
		}
	}
		
	// Update is called once per frame
	void Update () {
		Vector3 distance = player.position - transform.position;
		double zDist = Vector3.Dot(distance, transform.forward.normalized);

		if (zDist > endLength) {
			placeNextPlatform ();
		}
	}

	void placeNextPlatform () {
		pathSegments [nextPlatform].transform.position = new Vector3 (0, 0, endLength + 20);
		endLength += 20;
		nextPlatform = (nextPlatform + 1) % pathSegments.Length;
	}
}
