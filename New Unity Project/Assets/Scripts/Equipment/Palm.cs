using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Palm : Equipment
{
	private float bonusStrenght;
	
	public Palm()
	{
		this.costUpgrade = 50;
		this.bonusStrenght = 1.5f;
	}
	public Palm(int costUpgrade,float bonusStrenght)
	{
		this.costUpgrade = (costUpgrade<50)?50:costUpgrade;
		this.bonusStrenght = (bonusStrenght < 1)?1.5f:bonusStrenght;
		this.level = 1; 
	}
	
	public override int upgrade(float gold)
	{
		if (gold >= this.costUpgrade)
		{
			this.bonusStrenght += 0.025f;
			return this.costUpgrade;
		}
		return -1;
	}
	
	public override string ParseToString()
	{
		return this.goldPrice + "," + this.gemPrice + "," + this.level + "," + this.costUpgrade + "," + this.costGold + "," + this.isCarried + "," + this.bonusStrenght;
	}
}
