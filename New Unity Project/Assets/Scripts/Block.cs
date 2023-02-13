using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
	
	public float width;
	public float length;
	
	void Start()
	{		
		this.gameObject.transform.localScale = new Vector3(width,length,0.2f);
	}

    void Update()
    {
        
    }
	
	private string convertString(Collider[] list)
	{
		string s = "";
		foreach (Collider cld in list)
		{
			s += cld.gameObject.name + ",";
		}
		return s;
	}
	public bool checkAlone()
	{
		Vector3 scaleCollide = new Vector3(this.gameObject.transform.position.x/2.1f,this.gameObject.transform.position.y/2.1f,this.gameObject.transform.position.z);
		Collider[] hits = Physics.OverlapBox(this.gameObject.transform.position,scaleCollide,Quaternion.identity);
		if (hits.Length <= 1)
		{
			Debug.Log(this.gameObject.name + " true "+ this.gameObject.transform.position);
			return true;
		}
		Debug.LogWarning("colid " + convertString(hits) + " " +  this.gameObject.transform.position + " " + this.gameObject.name);
		return false;
	}
	
	
	public int blockArea()
	{
		return (int)(this.width * this.length);
	}
}
