using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchTrigger : MonoBehaviour
{
	// Variables
	public string OpponentLayer;
	public int player_health;
	public int opponent_health;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.layer  == LayerMask.NameToLayer(OpponentLayer))
		{
			other.GetComponentInParent<Animator>().Play("TakeHit");
			if(other.GetComponentInParent<Fighter>().GetComponent<Fighter>().health > 0)
				other.GetComponentInParent<Fighter>().GetComponent<Fighter>().health -= 25;
		}
	}
}
