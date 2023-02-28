using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : Equipment
{
	public float oxyCapacity;
	public float malusStrenght;
	public float malusDexterity;
	
	public Tank()
	{
		this.oxyCapacity = 24f;
		this.costUpgrade = 250;
		this.malusDexterity = 0.5f;
		this.malusStrenght = 0.75f;
	}
	public Tank(float oxyCapacity,int costUpgrade,float malusDexterity,float malusStrenght)
	{
		this.oxyCapacity = (oxyCapacity<24)?24:oxyCapacity;
		this.costUpgrade = (costUpgrade<250)?250:costUpgrade;
		this.malusDexterity = (malusDexterity > 1)?0.5f:malusDexterity;
		this.malusStrenght = (malusStrenght > 1)?0.75f:malusStrenght;
		this.level = 1;
	}
	
	public override int upgrade(float gold)
	{
		if (gold >= this.costUpgrade)
		{
			this.oxyCapacity += this.level / 5;
			this.malusDexterity += 0.025f;
			this.malusStrenght += 0.05f;
			return this.costUpgrade;
		}
		return -1;
	}
	
	public override string ParseToString()
	{
		return this.goldPrice + "," + this.gemPrice + "," + this.level + "," + this.costUpgrade + "," + this.costGold + "," + this.isCarried + "," + this.oxyCapacity + "," + this.malusStrenght + "," + this.malusDexterity;
	}
}
