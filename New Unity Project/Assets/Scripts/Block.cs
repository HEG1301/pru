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
		//Debug.Log(this.gameObject.GetComponent<Rigidbody>().collisionDetectionMode);
		Physics.queriesHitTriggers = true;
		this.nbrAngle = 16;
		this.lengthRay = new float[nbrAngle];
		this.gameObject.transform.localScale = new Vector3(width,length,0.2f);
		//this.gameObject.GetComponent<BoxCollider>().size = this.gameObject.transform.localScale;
		/*for (int i = 0; i < nbrAngle; i++)
		{
			float angle = i * (360/nbrAngle) * Mathf.Deg2Rad;
			lengthRay[i] =Mathf.Abs(Mathf.Cos(angle)*width/2) + Mathf.Abs(Mathf.Sin(angle)*length/2);
		}
		*/
		//Debug.Log("isAlone :" + checkAlone());
	}

    void Update()
    {
        
    }
	
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.name != this.gameObject.name)
		{
			Debug.LogWarning("collide " + this.gameObject.transform.position + " | " + other.gameObject.transform.position + "  " + other.gameObject.name);
		}
	}
	
	public void PrintArray()
	{
		string s = "Array(" + this.lengthRay.Length + ")[" + this.lengthRay[0];
		for (int i = 1;i<nbrAngle;i++)
		{
			s += " | " + this.lengthRay[i];
		}
		s += "]";
		Debug.Log(s);
	}
	
	private string convertString(RaycastHit[] list)
	{
		string s = "list(" + list.Length + ")[";
		foreach (RaycastHit cld in list)
		{
			s += cld.rigidbody.gameObject.name + ",";
		}
		return s + "]";
	}
	
	/*
	public GameObject[] getCollidingObject()
	{
		List<GameObject> L = new List<GameObject>();
		RaycastHit hit;
		for (int i = 0;i < this.nbrAngle;i++)
		{
			if (Physics.Raycast(this.gameObject.transform.position,this.gameObject.transform.TransformDirection(Vector3.forward),out hit,lengthRay[i]))
			{
				Debug.Log("1a");
				Debug.DrawRay(transform.position,this.gameObject.transform.TransformDirection(Vector3.forward) * lengthRay[i], Color.yellow,10f);
			}
			else
			{
				Debug.Log("2a");
	            Debug.DrawRay(this.gameObject.transform.position,this.gameObject.transform.TransformDirection(Vector3.forward) * lengthRay[i], Color.white,10f);
			}
			//L.Add(hit.rigidbody.gameObject);
		}
		return L.ToArray();
	}
	*/
	
	/*
	public bool checkAlone2()
	{
		//Vector3 scaleCollide = new Vector3(this.gameObject.transform.position.x/2.1f,this.gameObject.transform.position.y/2.1f,this.gameObject.transform.position.z);
		GameObject[] hits = getCollidingObject();
		if (hits.Length < 1)
		{
			Debug.Log("1");
			//Debug.Log(this.gameObject.name + " true "+ this.gameObject.transform.position);
			return true;
		}
		else if (hits.Length == 1)
		{
			Debug.Log("2");
			return hits[0].name == this.gameObject.name;
		}
		Debug.LogWarning("colid " + hits.Length + " " +  this.gameObject.transform.position + " " + this.gameObject.name); //+ convertString(hits)
		return false;
	}
	*/
	
	public bool checkTrulyNotAlone(RaycastHit[] clds)
	{
		foreach (RaycastHit cld in clds)
		{
			if (cld.rigidbody.gameObject.name != this.gameObject.name) //cld.gameObject.name != "BorderRight" && cld.gameObject.name != "BorderLeft")
			{
				Debug.LogWarning(cld.rigidbody.gameObject.transform.position + " " + cld.rigidbody.gameObject.name);
				return true;
			}
			
		}
		return false;
	}
	
	public bool checkAlone()
	{
		Vector3 scaleCollide = new Vector3(this.gameObject.transform.localScale.x/2.2f,this.gameObject.transform.localScale.y/2.2f,this.gameObject.transform.localScale.z);
		RaycastHit[] hits = Physics.BoxCastAll(this.gameObject.transform.position,scaleCollide,this.gameObject.transform.forward,Quaternion.identity);
		if (!checkTrulyNotAlone(hits))
		{
			Debug.Log(this.gameObject.name + "|" + convertString(hits) + " " + this.gameObject.transform.position);
			return true;
		}
		Debug.LogWarning("colid : " + convertString(hits) + " | " +  this.gameObject.transform.position + " " + this.gameObject.name);
		return false;
	}
	
	
	public int blockArea()
	{
		return (int)(this.width * this.length);
	}
}
