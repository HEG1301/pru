using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGeneration : MonoBehaviour
{
	private System.Random rdm;
	public GameObject bloc;
    // Start is called before the first frame update
    void Start()
    {
		rdm = new System.Random();
        create(new Vector3(0,0,0),"bloc 1");
		Thread.Sleep(3000);
		create(new Vector3(2,1,0),"bloc 2");
		Thread.Sleep(3000);
		create(new Vector3(1.5f,0.5f,0),"bloc 3");
		Thread.Sleep(3000);
    }


	public void create(Vector3 pos,string name)
	{
		GameObject tmp = Instantiate(bloc,pos,Quaternion.identity);
		tmp.name = name;
		Debug.Log(name);
		if (!tmp.GetComponent<Block>().verifCreation());
		{
			float x = rdm.Next(-3,3),y = rdm.Next(-3,3);
			tmp.transform.position = new Vector3(tmp.transform.position.x+x,tmp.transform.position.y+y,0);
			//yield return new WaitForSeconds(0.1f);
		}
	}

    // Update is called once per frame
    void Update()
    {
       
    }
}
