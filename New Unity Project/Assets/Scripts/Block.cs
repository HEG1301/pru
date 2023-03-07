using System.Threading;
using System.Text;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = System.Random;


public class Block : MonoBehaviour
{
	private const float Pi = Mathf.PI;
	
	//private int[][] previousAction = new int[][]{{-1,-1},{-1,-1}};
	private int[][] previousAction = new int[][]{new int[]{-1,-1},new int[]{-1,-1}};
	[SerializeField]
	private float area;
	public float deltaMove;
	public int conteur = 0;
	//public int nbrAngle;
	public BlockGeneration scriptBlockGen;
	public int nbrAngle;
	private float[] lengthRay;
	private Random rdm;
	
	void Start()
	{
		rdm = new Random();
		//Debug.Log(this.gameObject.name + "(" + this.gameObject.GetInstanceID() + ")");
		//Debug.Log(this.gameObject.GetComponent<Rigidbody>().collisionDetectionMode);
		Physics.queriesHitTriggers = true;
		this.nbrAngle = 16;
		this.lengthRay = new float[nbrAngle];
		//this.gameObject.transform.localScale = new Vector3(width,length,0.2f);
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
	/*
	void OnTriggerEnter(Collider other)
	{
		this.conteur ++;
	}
	*/
	void OnTriggerStay(Collider other)
	{
		if (other.gameObject.name != this.gameObject.name && other.gameObject.tag == "obstacle")
		{
			if (other.gameObject.GetInstanceID() > this.gameObject.GetInstanceID())
			{
				
				//Debug.LogWarning(this.gameObject.name + "   " + other.gameObject.name);
				if (this.conteur >= 120)
				{
					scriptBlockGen.i -= this.blockArea();
					Debug.Log("destroy" + this.gameObject.name);
					conteur = 0;
					Destroy(this.gameObject);
				}
				tryMove(other);
				//Destroy(this.gameObject);
			}
		}
	}
	/*
	void OnTriggerExit(Collider other)
	{
		this.conteur --;
	}
	*/
	
	private void tryMove(Collider other)
	{
		rdm = new Random();
		Vector3 o_center = other.bounds.center;
		Vector3 pos = this.gameObject.transform.position;
		//Debug.Log(o_center + " | " + pos);
		int x = 0,y = 0,dir = 0;
		if (pos == o_center)
		{
			dir = rdm.Next(0,4);
			if ((float)dir/2f >= 1f)
				pos.x += dir%2*(-1)*other.bounds.size.x; //*Time.deltaTime;
			else
				pos.y += dir%2*(-1)*other.bounds.size.y; //* Time.deltaTime;
		}
		else
		{
			if (this.gameObject.transform.position.x > o_center.x)
			{
				x = 1;
				pos.x += this.deltaMove; // * Time.deltaTime;
			}
			else if (this.gameObject.transform.position.x < o_center.x)
			{
				x = 2;
				pos.x -= this.deltaMove; // * Time.deltaTime;
			}
			if (this.gameObject.transform.position.y > o_center.y)
			{
				y = 1;
				pos.y += this.deltaMove; // * Time.deltaTime;
			}
			else if (this.gameObject.transform.position.y < o_center.y)
			{
				y = 2;
				pos.y -= this.deltaMove; // * Time.deltaTime;
			}
		}
		/*if (previousAction[0][0] == x && previousAction[0][1] == y && previousAction[1][0] != x && previousAction[1][1] != y)
		{
			Debug.Log("Destroy    "  +this.gameObject.name);
			this.gameObject.transform.position = pos;
			//Destroy(this.gameObject);
		}
		else
		{*/
			conteur += 1;
			this.gameObject.transform.position = pos;
			previousAction = new int[][]{new int[]{previousAction[1][0],previousAction[1][1]},new int[]{x,y}};
		//}
		
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
		return (int)this.area + 1;
	}
}
