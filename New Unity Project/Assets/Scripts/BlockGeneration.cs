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
    // Start is called before the first frame update
    void Start()
    {
		rdm = new Random();
    }



    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyUp(KeyCode.Space))
			BorderRight.GetComponent<Border>().expand();
		
		if (Input.GetKeyUp(KeyCode.A))
		{
			Vector3[] extremity = BorderRight.GetComponent<Border>().getNewSectorExtremity();
			Instantiate(bloc,extremity[0],Quaternion.identity);
			Instantiate(bloc,extremity[1],Quaternion.identity);
		}
		if (Input.GetKeyUp(KeyCode.G))
		{
			Vector3[] extremityR = BorderRight.GetComponent<Border>().getNewSectorExtremity();
			Vector3[] extremityL = BorderLeft.GetComponent<Border>().getNewSectorExtremity();
			Vector3 pos = new Vector3(rdm.Next((int)extremityL[0].x,(int)extremityR[0].x),rdm.Next((int)extremityR[1].y,(int)extremityR[0].y),extremityL[0].z);
			Instantiate(bloc,pos,Quaternion.identity);
		}
		if (Input.GetKeyUp(KeyCode.D))
		{
			Vector3[] extremityR = BorderRight.GetComponent<Border>().getYExtremity();
			Vector3[] extremityL = BorderLeft.GetComponent<Border>().getYExtremity();
			Vector3 pos = new Vector3(rdm.Next((int)extremityL[0].x,(int)extremityR[0].x),rdm.Next((int)extremityR[1].y,(int)extremityR[0].y),extremityL[0].z);
			Instantiate(bloc,pos,Quaternion.identity);
		}
    }
}
