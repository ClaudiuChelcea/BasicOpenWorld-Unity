using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchTrigger : MonoBehaviour
{
	// Variables
	public string OpponentLayer;
	public int player_health;
	public int opponent_health;
	private AudioSource audioSrc;

	// Start is called before the first frame update
	void Start()
	{
		audioSrc = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update()
	{

	}

	// When the punch is triggered
	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.layer  == LayerMask.NameToLayer(OpponentLayer))
		{
			// Play on hit sound
			audioSrc.Play();
			
			// Play take hit animation
			other.GetComponentInParent<Animator>().Play("TakeHit");

			// Subtract health if getting hit
			if(other.GetComponentInParent<Fighter>().GetComponent<Fighter>().health > 0)
				other.GetComponentInParent<Fighter>().GetComponent<Fighter>().health -= 25;
		}
	}
}
