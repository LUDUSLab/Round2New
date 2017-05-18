using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Sprites;

public class CheckpointController : MonoBehaviour {

	public Sprite CheckNo;
	public Sprite CheckYes;
	private SpriteRenderer CheckpointSpriteRenderer;
	private bool CheckpointTrigger;

	// Use this for initialization
	void Start () {
		CheckpointSpriteRenderer = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D (Collider2D other){

		if (other.tag == "Player") {
			CheckpointSpriteRenderer.sprite = CheckYes;
			CheckpointTrigger = true;
		}
	}
}
