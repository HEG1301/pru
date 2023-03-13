using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = System.Random;

public class Shark : MonoBehaviour
{
	public float zMax;
	public float zMin;
	public float xMin;
	public float xMax;
	public float speed;
	public int damage;
	public float distance = 10;
	
	public GameObject target;
	public NavMeshAgent navMesh;
    // Start is called before the first frame update
    void Start()
    {
		navMesh = this.gameObject.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
	{ 
		//Debug.Log(navMesh);
		GameObject tmp = GameObject.FindWithTag("Player").gameObject;
		target = (getDistanceOk(tmp.transform.position,distance))?tmp:null;
		
		if (target != null)
		{
			Debug.Log(target);
			navMesh.SetDestination(target.transform.position);
			if ((navMesh.remainingDistance <= navMesh.stoppingDistance))
			{
				Debug.LogError("this is working");
				target.gameObject.GetComponent<PlayerInGame>().lifeCurent -= damage*Time.deltaTime;
			}
		}
		else
		{
			if (navMesh.hasPath)
			{
				if ((navMesh.remainingDistance <= navMesh.stoppingDistance))
				{
					Vector3 newDest = newDestination();
					Debug.Log(newDest);
					navMesh.SetDestination(newDest);
				}
			}
			else
			{
				Vector3 newDest = newDestination();
				Debug.Log(newDest);
				navMesh.SetDestination(newDest);
			}
		}
    }
	
	public Vector3 newDestination()
	{
		Random rdm = new Random();
		return new Vector3(rdm.Next((int)xMin+1,(int)xMax),-0.25f,rdm.Next((int)zMin+1,(int)zMax));
	}
	void OnColliderEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			other.gameObject.GetComponent<PlayerInGame>().lifeCurent -= damage;
		}
	}
	void OnColliderExit(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			other.gameObject.GetComponent<PlayerInGame>().lifeCurent -= damage;
		}
	}
	void OnColliderStay(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			Debug.LogWarning("this is working");
			other.gameObject.GetComponent<PlayerInGame>().lifeCurent -= damage*Time.deltaTime;
		}
	}
	
	public bool getDistanceOk(Vector3 posTarget,float maxDis)
	{
		if (Vector3.Distance(posTarget,this.transform.position) > maxDis)
			return false;
		else
		{
			RaycastHit[] hits = Physics.RaycastAll(this.transform.position,(posTarget-this.transform.position),maxDis);
			foreach (RaycastHit hit in hits)
			{
				if (hit.transform.gameObject.tag == "blockObstacle")
					return false;
			}
			return true;
		}
	}
}
