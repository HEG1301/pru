using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Equipment
{
	public Sprite sprite;
	public float goldPrice;
	public float gemPrice;
	public int level;
	public int costUpgrade;
    public bool costGold;
	public bool isCarried;
	public bool isBougth;
	
	public Equipment()
	{
		this.sprite = null;
		this.goldPrice = 0;
		this.gemPrice = 0;
		this.level = 1;
		this.costUpgrade = 50;
		this.costGold = true;
		this.isCarried = false;
		this.isBougth = false;
	}
	
	private void updateCostUpgrade()
	{
		if (level%5 == 0)
		{
			this.costUpgrade /= 10;
			this.costGold = false;
		}
		else if (!costGold)
		{
			this.costUpgrade *= 20;
			this.costGold = true;
		}
		else
		{
			this.costUpgrade *= 2;
		}
		//return this.costUpgrade;
	}
	
	public virtual int upgrade(float gold)
	{
		return -1;
	}
	
	public virtual string ParseToString()
	{
		return this.goldPrice + "," + this.gemPrice + "," + this.level + "," + this.costUpgrade + "," + this.costGold + "," + this.isCarried;
	}
}
