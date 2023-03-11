using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cave : MonoBehaviour
{
	public IEnumerator cor;
	public PlayerInGame tempPlayer;
	void Start()
	{
		this.cor = addOxy();
	}
    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			tempPlayer = other.gameObject.GetComponent<PlayerInGame>();
			tempPlayer.curentApnee = tempPlayer.maxApnee; 
			StartCoroutine(cor);
		}
	}
	
	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			//StopCoroutine(cor);
			tempPlayer = null;
		}
	}
	
	IEnumerator addOxy()
	{
		while (tempPlayer != null)
		{
			tempPlayer.OxyCurent += 0.15f * Time.deltaTime;
			yield return new WaitForSeconds(0.001f);
		}
	}
}
