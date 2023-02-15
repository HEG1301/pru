using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class BlockGeneration : MonoBehaviour
{
	public GameObject bloc;
	public GameObject BorderRight;
	public GameObject BorderLeft;
	public Random rdm;
	public int conteur;
    // Start is called before the first frame update
    void Start()
    {
		conteur = 0;
		rdm = new Random();
    }



    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyUp(KeyCode.Space))
		{
			BorderRight.GetComponent<Border>().expand();
			BorderLeft.GetComponent<Border>().expand();
		}
		
		if (Input.GetKeyUp(KeyCode.A))
		{
			Vector3[] extremity = BorderRight.GetComponent<Border>().getNewSectorExtremity();
			GameObject tmp = Instantiate(bloc,extremity[0],Quaternion.identity);
			tmp.name = "Block_" + conteur;
			tmp = Instantiate(bloc,extremity[1],Quaternion.identity);
			tmp.name = "Block_" + conteur;
			conteur ++;
		}
		if (Input.GetKeyUp(KeyCode.G))
		{
			Vector3[] extremityR = BorderRight.GetComponent<Border>().getNewSectorExtremity();
			Vector3[] extremityL = BorderLeft.GetComponent<Border>().getNewSectorExtremity();
			Vector3 pos = new Vector3(rdm.Next((int)extremityL[0].x,(int)extremityR[0].x),rdm.Next((int)extremityR[1].y,(int)extremityR[0].y),extremityL[0].z);
			GameObject tmp = Instantiate(bloc,pos,Quaternion.identity);
			tmp.name = "Block_" + conteur;
			//tmp.GetComponent<Block>().checkAlone();
			conteur ++;
		}
		if (Input.GetKeyUp(KeyCode.D))
		{
			Vector3[] extremityR = BorderRight.GetComponent<Border>().getYExtremity();
			Vector3[] extremityL = BorderLeft.GetComponent<Border>().getYExtremity();
			Vector3 pos = new Vector3(rdm.Next((int)extremityL[0].x,(int)extremityR[0].x),rdm.Next((int)extremityR[1].y,(int)extremityR[0].y),extremityL[0].z);
			GameObject tmp = Instantiate(bloc,pos,Quaternion.identity);
			tmp.name = "Block_" + conteur;
			conteur ++;
		}
		if (Input.GetKeyUp(KeyCode.M))
		{
			generateNewSection();
		}
    }
	
	public GameObject createRandomBloc(Vector3[] extremityR,Vector3[] extremityL)
	{
		Vector3 pos = new Vector3(rdm.Next((int)extremityL[0].x+(int)BorderLeft.transform.localScale.x-1,(int)extremityR[0].x-(int)BorderRight.transform.localScale.x)+1,rdm.Next((int)extremityR[1].y,(int)extremityR[0].y	),extremityL[0].z);
		return Instantiate(bloc,pos,Quaternion.identity);
	}
	public void generateNewSection()
	{		
		Border ScriptRight = BorderRight.GetComponent<Border>();
		Border ScriptLeft = BorderLeft.GetComponent<Border>();
		int i = 0;
		int limit = (int)(ScriptRight.newSectionArea(this.BorderLeft.transform.position.x+this.BorderLeft.transform.localScale.x));
		limit = (int)(limit * 0.3f);
		Debug.Log(limit);
		while (i < limit)
		{
			GameObject tmp = createRandomBloc(ScriptRight.getNewSectorExtremity(),ScriptLeft.getNewSectorExtremity());
			tmp.name = "Block_" + conteur;
			if (tmp.GetComponent<Block>().checkAlone())
			{
				i += tmp.GetComponent<Block>().blockArea();
				conteur ++;
			}
			else
			{
				Destroy(tmp);
			}
		}
		Debug.Log(i);
	}
}
