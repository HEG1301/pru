using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
	int value;
	public BlockGeneration scriptBlockGen;
    // Start is called before the first frame update
    void Start()
    {
        this.value = 1;
		
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	void OnTriggerStay(Collider other)
	{
		if (other.gameObject != this.gameObject	&& other.gameObject.tag != "Player")
		{
			Vector3 newPos = scriptBlockGen.createRandomCoord(scriptBlockGen.BorderRight.GetComponent<Border>().getNewSectorExtremity(),scriptBlockGen.BorderLeft.GetComponent<Border>().getNewSectorExtremity());
			this.gameObject.transform.position = newPos;
		}
		else if (other.gameObject.tag == "Player")
		{
			other.gameObject.GetComponent<PlayerInGame>().money += this.value;
		}
	}
	
	
}
