using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Palm : Equipment
{
	public float bonusStrenght;
	
	public Palm()
	{
		this.costUpgrade = 50;
		this.bonusStrenght = 1.5f;
		//this.sprite = Resources.Load("linkOfSpriteForPalm");
	}
	public Palm(int costUpgrade,float bonusStrenght)
	{
		this.costUpgrade = costUpgrade;
		this.bonusStrenght = bonusStrenght;
		this.level = 1; 
	}
	
	public Palm(int costUpgrade,float bonusStrenght,int bougthPrice)
	{
		this.costUpgrade = costUpgrade;
		this.bonusStrenght = bonusStrenght;
		this.level = 1;
		this.goldPrice = bougthPrice;
	}
	
	public override int upgrade(float gold)
	{
		if (gold >= this.costUpgrade)
		{
			this.level += 1;
			this.bonusStrenght += 0.025f;
			return this.costUpgrade;
		}
		return -1;
	}
	
	public override string ParseToString()
	{
		return this.goldPrice + "," + this.gemPrice + "," + this.level + "," + this.costUpgrade + "," + this.costGold + "," + this.isCarried + "," + this.bonusStrenght;
	}
	
	public string getDescription()
	{
		return "this weapon is adding a bonus in strenght up to: " + this.bonusStrenght;
	}
}
