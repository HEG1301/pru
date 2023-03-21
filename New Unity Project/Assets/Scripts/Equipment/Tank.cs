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
		//this.sprite = Resources.Load("linkOfSpriteForTank");
	}
	public Tank(float oxyCapacity,int costUpgrade,float malusDexterity,float malusStrenght)
	{
		this.oxyCapacity = oxyCapacity;
		this.costUpgrade = costUpgrade;
		this.malusDexterity = malusDexterity;
		this.malusStrenght = malusStrenght;
		this.level = 1;
	}
	public Tank(float oxyCapacity,int costUpgrade,float malusDexterity,float malusStrenght,int boughtPrice)
	{
		this.oxyCapacity = oxyCapacity;
		this.costUpgrade = costUpgrade;
		this.malusDexterity = malusDexterity;
		this.malusStrenght = malusStrenght;
		this.level = 1;
		this.goldPrice = boughtPrice;
	}
	
	public override int upgrade(float gold)
	{
		if (gold >= this.costUpgrade)
		{
			
			this.level += 1;
			this.oxyCapacity += this.level / 5;
			this.malusDexterity += 0.025f;
			this.malusStrenght += 0.05f;
			return this.costUpgrade;
		}
		//this.level += 1;
		return -1;
	}
	
	public override string ParseToString()
	{
		return this.goldPrice + "," + this.gemPrice + "," + this.level + "," + this.costUpgrade + "," + this.costGold + "," + this.isCarried + "," + this.oxyCapacity + "," + this.malusStrenght + "," + this.malusDexterity+ "," + this.isBougth;
	}
	
	public string getDescription()
	{
		return "this tank is adding oxygen up to: " + this.oxyCapacity + "but its also adding a malus in dexterity and strength  (" + this.malusDexterity + "," + this.malusStrenght + ")";
	}
}
