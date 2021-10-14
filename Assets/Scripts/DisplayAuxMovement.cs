using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayAuxMovement : MonoBehaviour
{
        // Variables
        public GameObject spaceBar; // space
        public GameObject sprintBar; // sprint
        public GameObject TauntBar; // taunt
        public GameObject DanceBar; // dance
	public GameObject attack; // atack
	public GameObject isHit; // isHit
        public PlayerMovement player;
	public Animator animator;

    // Start is called before the first frame update
    void Start()
    {

    }

	// Update is called once per frame
	void Update()
	{
		// Display the buttons if certain keys are pressed
		ShowButtonsOnEvent();

		// Keep the buttons hidden when dead
		InactivateHUDwhenDEAD();
	}

	// Display the buttons if certain keys are pressed
	private void ShowButtonsOnEvent()
	{
		// Activate buttons
		// Jump
		spaceBar.SetActive(Input.GetKey(KeyCode.Space));

		// Sprint
		sprintBar.SetActive(player.player_speed == 10);

		// Taunt
		TauntBar.SetActive(animator.GetCurrentAnimatorStateInfo(0).IsName("Taunt"));

		// Dance
		DanceBar.SetActive(animator.GetCurrentAnimatorStateInfo(0).IsName("Dance") || animator.GetCurrentAnimatorStateInfo(0).IsName("DanceContinue") ||
			Input.GetKey(KeyCode.Y) || animator.GetCurrentAnimatorStateInfo(0).IsTag("Dance"));
	
		// Attack
		attack.SetActive(animator.GetCurrentAnimatorStateInfo(0).IsName("LeftPunch") || animator.GetCurrentAnimatorStateInfo(0).IsName("RightPunch"));

		// IsHit
		isHit.SetActive(animator.GetCurrentAnimatorStateInfo(0).IsName("TakeHit"));
	}

	// Keep the buttons hidden when dead
	private void InactivateHUDwhenDEAD()
	{
		if (player.health <= 0)
		{
			spaceBar.SetActive(false);
			sprintBar.SetActive(false);
		}
	}
}
