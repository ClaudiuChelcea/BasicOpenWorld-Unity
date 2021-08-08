using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchTrigger : MonoBehaviour
{
	public string OpponentLayer;
	//private AnimatorStateInfo get_animator_state;
	//public Animator get_animator;

	// Start is called before the first frame update
	void Start()
	{
		//get_animator = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update()
	{
		//get_animator_state = get_animator.GetCurrentAnimatorStateInfo(0);
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.layer  == LayerMask.NameToLayer(OpponentLayer)) //&& get_animator_state.IsName("Punch"))
		{
			other.GetComponentInParent<Animator>().Play("TakeHit");
		}
	}
}
