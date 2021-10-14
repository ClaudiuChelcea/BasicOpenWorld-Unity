using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
        public PlayerMovement character;
  
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {       
                // Display character`s health points
                switch(character.health)
		{
                        case 200:
                                this.GetComponentInChildren<MeshRenderer>().enabled = true;
                                this.gameObject.transform.Find("Hit1").gameObject.SetActive(true);
                                this.gameObject.transform.Find("Hit2").gameObject.SetActive(true);
                                this.gameObject.transform.Find("Hit3").gameObject.SetActive(true);
                                this.gameObject.transform.Find("Hit4").gameObject.SetActive(true);
                                this.gameObject.transform.Find("Hit5").gameObject.SetActive(true);
                                this.gameObject.transform.Find("Hit6").gameObject.SetActive(true);
                                this.gameObject.transform.Find("Hit7").gameObject.SetActive(true);
                                this.gameObject.transform.Find("Hit8").gameObject.SetActive(true);
                                break;
                        case 175:
                                this.gameObject.transform.Find("Hit1").gameObject.SetActive(false);
                                break;
                        case 150:
                                this.gameObject.transform.Find("Hit1").gameObject.SetActive(false);
                                this.gameObject.transform.Find("Hit2").gameObject.SetActive(false);
                                break;
                        case 125:
                                this.gameObject.transform.Find("Hit1").gameObject.SetActive(false);
                                this.gameObject.transform.Find("Hit2").gameObject.SetActive(false);
                                this.gameObject.transform.Find("Hit3").gameObject.SetActive(false);
                                break;
                        case 100:
                                this.gameObject.transform.Find("Hit1").gameObject.SetActive(false);
                                this.gameObject.transform.Find("Hit2").gameObject.SetActive(false);
                                this.gameObject.transform.Find("Hit3").gameObject.SetActive(false);
                                this.gameObject.transform.Find("Hit4").gameObject.SetActive(false);
                                break;
                        case 75:
                                this.gameObject.transform.Find("Hit1").gameObject.SetActive(false);
                                this.gameObject.transform.Find("Hit2").gameObject.SetActive(false);
                                this.gameObject.transform.Find("Hit3").gameObject.SetActive(false);
                                this.gameObject.transform.Find("Hit4").gameObject.SetActive(false);
                                this.gameObject.transform.Find("Hit5").gameObject.SetActive(false);
                                break;
                        case 50:
                                this.gameObject.transform.Find("Hit1").gameObject.SetActive(false);
                                this.gameObject.transform.Find("Hit2").gameObject.SetActive(false);
                                this.gameObject.transform.Find("Hit3").gameObject.SetActive(false);
                                this.gameObject.transform.Find("Hit4").gameObject.SetActive(false);
                                this.gameObject.transform.Find("Hit5").gameObject.SetActive(false);
                                this.gameObject.transform.Find("Hit6").gameObject.SetActive(false);
                                break;
                        case 25:
                                this.gameObject.transform.Find("Hit1").gameObject.SetActive(false);
                                this.gameObject.transform.Find("Hit2").gameObject.SetActive(false);
                                this.gameObject.transform.Find("Hit3").gameObject.SetActive(false);
                                this.gameObject.transform.Find("Hit4").gameObject.SetActive(false);
                                this.gameObject.transform.Find("Hit5").gameObject.SetActive(false);
                                this.gameObject.transform.Find("Hit6").gameObject.SetActive(false);
                                this.gameObject.transform.Find("Hit7").gameObject.SetActive(false);
                                break;
                        case 0:
                                this.gameObject.transform.Find("Hit1").gameObject.SetActive(false);
                                this.gameObject.transform.Find("Hit2").gameObject.SetActive(false);
                                this.gameObject.transform.Find("Hit3").gameObject.SetActive(false);
                                this.gameObject.transform.Find("Hit4").gameObject.SetActive(false);
                                this.gameObject.transform.Find("Hit5").gameObject.SetActive(false);
                                this.gameObject.transform.Find("Hit6").gameObject.SetActive(false);
                                this.gameObject.transform.Find("Hit7").gameObject.SetActive(false);
                                this.gameObject.transform.Find("Hit8").gameObject.SetActive(false);
                                this.GetComponentInChildren<MeshRenderer>().enabled = false;
                                break;
                        default:
                                Debug.Log("Unexpected situation encountered!");
                                break;
		}
                
        }
}
