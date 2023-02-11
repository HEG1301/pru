using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
 
   public bool verifCreation()
    {
        Collider[] hitColliders = Physics.OverlapBox(this.gameObject.transform.position,transform.localScale/2,Quaternion.identity);
		if (hitColliders.Length == 1)
		{
			Debug.Log(hitColliders[0].gameObject.name);
			return true;
		}
		else
		{
			Debug.Log(hitColliders.Length);
			Debug.Log("destruction");
			return false;
		}
    }

	
	void Start()
	{
		
	}

    void Update()
    {
        
    }
}
