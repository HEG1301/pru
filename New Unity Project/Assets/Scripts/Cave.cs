using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cave : MonoBehaviour
{
	public IEnumerator cor;
	public PlayerInGame tempPlayer;
	void Start()
	{
	}
	
	void Update()
	{
		if (tempPlayer != null)
		{
			if (tempPlayer.OxyCurent < tempPlayer.OxyMax)
				tempPlayer.OxyCurent += 0.5f*Time.deltaTime;
		}
	}
    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			tempPlayer = other.gameObject.GetComponent<PlayerInGame>();
			tempPlayer.curentApnee = tempPlayer.maxApnee;
			tempPlayer.inCave = true;
		}
	}
	
	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			//StopCoroutine(cor);
			other.gameObject.GetComponent<PlayerInGame>().inCave = false;
			tempPlayer = null;
		}
	}
}
