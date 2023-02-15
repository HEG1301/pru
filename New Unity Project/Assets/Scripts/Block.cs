using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Block : MonoBehaviour
{
	private const float Pi = Mathf.PI;
	
	public float width;
	public float length;
	
	public int nbrAngle;
	private float[] lengthRay;
	
	void Start()
	{
		this.nbrAngle = 16;
		this.lengthRay = new float[nbrAngle];
		this.gameObject.transform.localScale = new Vector3(width,length,0.2f);
		for (int i = 0; i < nbrAngle; i++)
		{
			float angle = i * (360/nbrAngle) * Mathf.Deg2Rad;
			lengthRay[i] =Mathf.Abs(Mathf.Cos(angle)*width/2) + Mathf.Abs(Mathf.Sin(angle)*length/2);
		}
		Debug.Log("isAlone :" + checkAlone2());
	}

    void Update()
    {
        
    }
	
	public void PrintArray()
	{
		string s = "[" + this.lengthRay[0];
		for (int i = 1;i<nbrAngle;i++)
		{
			s += " | " + this.lengthRay[i];
		}
		s += "]";
		Debug.Log(s);
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
	
	public GameObject[] getCollidingObject()
	{
		List<GameObject> L = new List<GameObject>();
		RaycastHit hit;
		for (int i = 0;i < this.nbrAngle;i++)
		{
			Debug.Log(Physics.Raycast(transform.position,new Vector4(this.transform.rotation.x,this.transform.rotation.y,i * (360/nbrAngle) * Mathf.Deg2Rad,this.transform.rotation.w),out hit,lengthRay[i]));
			//L.Add(hit.rigidbody.gameObject);
		}
		return L.ToArray();
	}
	
	public bool checkAlone2()
	{
		//Vector3 scaleCollide = new Vector3(this.gameObject.transform.position.x/2.1f,this.gameObject.transform.position.y/2.1f,this.gameObject.transform.position.z);
		GameObject[] hits = getCollidingObject();
		if (hits.Length <= 1)
		{
			//Debug.Log(this.gameObject.name + " true "+ this.gameObject.transform.position);
			return true;
		}
		Debug.LogWarning("colid " + hits.Length + " " +  this.gameObject.transform.position + " " + this.gameObject.name); //+ convertString(hits)
		return false;
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
