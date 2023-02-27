using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour
{
	float originalYScale = 30;
	int nbrExpansion = 1;
    // Start is called before the first frame update
    void Start()
    {
		this.originalYScale = this.gameObject.transform.localScale.y;
		Debug.Log(this.gameObject.transform.localScale  + "   |   " + originalYScale);
		this.nbrExpansion = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	public void expand()
	{
		Vector3 scale = this.gameObject.transform.localScale;
		Vector3 pos = this.gameObject.transform.position;
		pos.y -= originalYScale/2;
		nbrExpansion += 1;
		//Debug.Log(scale + "  -  " + nbrExpansion + "  -  " + originalYScale);
		scale.y = originalYScale * nbrExpansion;
		//Debug.Log(scale);
		this.gameObject.transform.position = pos;
		this.gameObject.transform.localScale = scale;
	}
	
	public Vector3[] getYExtremity()
	{
		Vector3	pos = this.gameObject.transform.position;
		Vector3 top = new Vector3(pos.x,pos.y + (this.gameObject.transform.localScale.y)/2,pos.z);
		Vector3 bottom = new Vector3(pos.x,pos.y - (this.gameObject.transform.localScale.y)/2,pos.z);
		return new Vector3[]{top,bottom};
	}
	
	public Vector3[] getNewSectorExtremity()
	{
		Vector3	pos = this.gameObject.transform.position;
		Vector3 bottom = new Vector3(pos.x,pos.y - (this.gameObject.transform.localScale.y)/2,pos.z);
		Vector3 top = new Vector3(pos.x,bottom.y+originalYScale,pos.z);
		return new Vector3[]{top,bottom};
	}
	
	public float newSectionArea(float startX)
	{
		float endX = this.gameObject.transform.position.x-this.gameObject.transform.localScale.x;
		float width = Mathf.Abs(endX - startX);
		return width * this.originalYScale;
	}
	
}
