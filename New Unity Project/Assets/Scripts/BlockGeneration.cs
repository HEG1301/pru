using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = System.Random;

public class BlockGeneration : MonoBehaviour
{
	public GameObject coins;
	public GameObject[] BlockList;
	public GameObject NavMesh;
	public GameObject Plane;
	public GameObject BorderRight;
	public GameObject BorderLeft;
	public Random rdm;
	public int conteurBlock;
	public int i;
	public float coefSection;
	public int nbrPieceSection;
	public int nbrSection;
	public float posStartNewSection;
    // Start is called before the first frame update
    void Start()
    {
		nbrPieceSection = 15;
		this.nbrSection = 1;
		coefSection = 1;
		conteurBlock = 0;
		rdm = new Random();
		//his.posStartNewSection = 0.0f;
		generateNewSection();
		//generateNewSection();
    }

	public void generateNewSection()
	{
		this.nbrSection ++;
		
		//pas encore tester
		//this.nbrPieceSection = (int)(10*(1.0f+0.25f*nbrSection));
		//this.coefSection += this.coefSection * 0.05f * nbrSection;
		//fin de pas test		
		
		this.i = 0;
		// BorderRight.transform.localScale *= 2;
		BorderRight.GetComponent<Border>().expand();
		BorderLeft.GetComponent<Border>().expand();
		Plane.GetComponent<Border>().expand();
		Vector3 pos = Plane.GetComponent<Transform>().position;
		Plane.GetComponent<Transform>().position = new Vector3(pos.x,pos.y,BorderLeft.GetComponent<Transform>().position.z);
		
		Border ScriptRight = BorderRight.GetComponent<Border>();
		Border ScriptLeft = BorderLeft.GetComponent<Border>();
		Vector3[] extremityL = ScriptLeft.getNewSectorExtremity();
		Vector3[] extremityR = ScriptRight.getNewSectorExtremity();
		int limit = (int)(ScriptRight.newSectionArea(this.BorderLeft.transform.position.x+this.BorderLeft.transform.localScale.x) * 0.2f*coefSection);
		
		
		this.posStartNewSection = extremityL[0].z;
		//Debug.Log(posStartNewSection);
		while (i < limit)
		{
			GameObject tmp = createRandomBlock(extremityR,extremityL);
			tmp.name = "Block_" + conteurBlock;
			i += tmp.GetComponent<Block>().blockArea();
			conteurBlock ++;
		}
		limit += nbrPieceSection;
		while (i < limit)
		{
			GameObject tmp = Instantiate(coins,createRandomCoord(extremityR,extremityL),Quaternion.identity);
			tmp.GetComponent<Coins>().scriptBlockGen = this;
			//tmp.transform.eulerAngles = new Vector3(0,0,0);
			i ++;
		}
		Debug.Log("i = " + i);
		
		Plane.SetActive(true);
		NavMesh.GetComponent<NavMeshSurface>().BuildNavMesh();
		Plane.SetActive(false);
	}

    // Update is called once per frame
    void Update()
    {
		//Debug.Log(this.gameObject.transform.position);
		
		if (this.gameObject.transform.position.z <= this.posStartNewSection)
		{
			generateNewSection();
			return;
		}
	
		if (Input.GetKeyUp(KeyCode.Space))
		{
			Debug.Log("create section");
			BorderRight.GetComponent<Border>().expand();
			BorderLeft.GetComponent<Border>().expand();
		}
		if (Input.GetKeyUp(KeyCode.J))
		{
			Debug.Log("expand");
			Plane.GetComponent<Border>().expand();
			Plane.SetActive(true);
			NavMesh.GetComponent<NavMeshSurface>().BuildNavMesh();
		}
		if (Input.GetKeyUp(KeyCode.Space))
		{
			
		}
		if (Input.GetKeyUp(KeyCode.A))
		{
			Vector3[] extremity = BorderRight.GetComponent<Border>().getNewSectorExtremity();
			GameObject tmp = Instantiate(BlockList[0],extremity[0],Quaternion.identity);
			tmp.name = "Block_" + conteurBlock;
			tmp = Instantiate(BlockList[0],extremity[1],Quaternion.identity);
			tmp.name = "Block_" + conteurBlock;
			tmp.GetComponent<Block>().scriptBlockGen = this;
			conteurBlock ++;
		}
		if (Input.GetKeyUp(KeyCode.G))
		{
			Vector3[] extremityR = BorderRight.GetComponent<Border>().getNewSectorExtremity();
			Vector3[] extremityL = BorderLeft.GetComponent<Border>().getNewSectorExtremity();
			Vector3 pos = new Vector3(rdm.Next((int)extremityL[0].x,(int)extremityR[0].x),extremityL[0].y,rdm.Next((int)extremityR[1].z,(int)extremityR[0].z));
			GameObject tmp = Instantiate(BlockList[0],pos,Quaternion.identity);
			tmp.name = "Block_" + conteurBlock;
			tmp.GetComponent<Block>().scriptBlockGen = this;
			//tmp.GetComponent<BlockList[0]k>().checkAlone();
			conteurBlock ++;
		}
		if (Input.GetKeyUp(KeyCode.D))
		{
			Vector3[] extremityR = BorderRight.GetComponent<Border>().getZExtremity();
			Vector3[] extremityL = BorderLeft.GetComponent<Border>().getZExtremity();
			Vector3 pos = new Vector3(rdm.Next((int)extremityL[0].x,(int)extremityR[0].x),extremityL[0].y,rdm.Next((int)extremityR[1].z,(int)extremityR[0].z));
			GameObject tmp = Instantiate(BlockList[0],pos,Quaternion.identity);
			tmp.name = "Block_" + conteurBlock;
			tmp.GetComponent<Block>().scriptBlockGen = this;
			conteurBlock ++;
		}
		if (Input.GetKeyUp(KeyCode.M))
		{
			generateInNewSection();
		}
		
    }
	
	public GameObject getRandomInList()
	{
		int x = rdm.Next(0,100);
		if (x < 5)
			x = BlockList.Length - 1;
		else if (x < 30)
			x = 0;
		else
			x = (BlockList.Length>=1)?1:0;
		return BlockList[x];
	}
	
	public int getRandomRotation(Block script)
	{
		return (script.nbrAngle != 0)?(rdm.Next(0,script.nbrAngle) * 360/script.nbrAngle):0;
	}
	
	public Vector3 createRandomCoord(Vector3[] extremityR,Vector3[] extremityL)
	{
		return new Vector3(rdm.Next((int)extremityL[0].x+(int)BorderLeft.transform.localScale.x-1,(int)extremityR[0].x-(int)BorderRight.transform.localScale.x)+1,
							extremityL[0].y,
							rdm.Next((int)extremityR[1].z,(int)extremityR[0].z));
	}
	
	public GameObject createRandomBlock(Vector3[] extremityR,Vector3[] extremityL)
	{
		Vector3 pos = createRandomCoord(extremityR,extremityL);
		GameObject tmp = Instantiate(getRandomInList(),pos,Quaternion.identity);
		tmp.transform.eulerAngles = new Vector3(90,getRandomRotation(tmp.GetComponent<Block>()),0);
		tmp.GetComponent<Block>().scriptBlockGen = this;
		return tmp;
	}
	public void generateInNewSection()
	{		
		Border ScriptRight = BorderRight.GetComponent<Border>();
		Border ScriptLeft = BorderLeft.GetComponent<Border>();
		this.i = 0;
		Vector3[] extremityL = ScriptLeft.getNewSectorExtremity();
		Vector3[] extremityR = ScriptRight.getNewSectorExtremity();
		int limit = (int)(ScriptRight.newSectionArea(this.BorderLeft.transform.position.x+this.BorderLeft.transform.localScale.x));
		limit = (int)(limit * 0.2f*coefSection);
		//Debug.Log(limit);
		while (i < limit)
		{
			GameObject tmp = createRandomBlock(extremityR,extremityL);
			tmp.name = "Block_" + conteurBlock;
			i += tmp.GetComponent<Block>().blockArea();
			conteurBlock ++;
		}
		limit = i + nbrPieceSection;
		while (i < limit)
		{
			GameObject tmp = Instantiate(coins,createRandomCoord(extremityR,extremityL),Quaternion.identity);
			tmp.GetComponent<Coins>().scriptBlockGen = this;
			tmp.transform.eulerAngles = new Vector3(0,0,0);
			i ++;
		}
	}
}
