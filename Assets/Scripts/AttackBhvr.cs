using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBhvr : StateMachineBehaviour
{
	// Variables
	public float enableHTBX = 0.256f;
	public float disableHTBX = 0.565f;
	public HumanBodyBones attackBone;

	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		animator.GetBoneTransform(attackBone).GetComponent<Collider>().enabled = false;
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		// Check if the collider should be active or not
		bool colliderEnabler = stateInfo.normalizedTime > enableHTBX && stateInfo.normalizedTime < disableHTBX;
		
		// Activate / deactivate it based on the boolean value above
		animator.GetBoneTransform(attackBone).GetComponent<Collider>().enabled = colliderEnabler;
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{

	}
}
