using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suit : Equipment
{
	public float bonusDexterity;
	
	public Suit()
	{
		this.costUpgrade = 50;
		this.bonusDexterity = 1.25f;
		//this.sprite = Resources.Load("linkOfSpriteForSuit");
	}
	public Suit(int costUpgrade,float bonusDexterity)
	{
		this.costUpgrade = costUpgrade;
		this.bonusDexterity = bonusDexterity;
		this.level = 1; 
	}
	
	public Suit(int costUpgrade,float bonusDexterity,int boughtPrice)
	{
		this.costUpgrade = costUpgrade;
		this.bonusDexterity = bonusDexterity;
		this.level = 1; 
		this.goldPrice = boughtPrice;
	}
	
	public override int upgrade(float gold)
	{
		if (gold >= this.costUpgrade)
		{
			this.level += 1;
			this.bonusDexterity += 0.025f;
			return this.costUpgrade;
		}
		
		return -1;
	}
	
	public override string ParseToString()
	{
		return this.goldPrice + "," + this.gemPrice + "," + this.level + "," + this.costUpgrade + "," + this.costGold + "," + this.isCarried + "," + this.bonusDexterity + "," + this.isBougth;
	}
	
	public string getDescription()
	{
		return "this Suit is adding a bonus in dexterity up to: " + this.bonusDexterity;
	}
}
