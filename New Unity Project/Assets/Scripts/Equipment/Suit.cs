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
	}
	public Suit(int costUpgrade,float bonusStrenght)
	{
		this.costUpgrade = (costUpgrade<50)?50:costUpgrade;
		this.bonusDexterity = (bonusDexterity < 1)?1.25f:bonusDexterity;
		this.level = 1; 
	}
	
	public override int upgrade(float gold)
	{
		if (gold >= this.costUpgrade)
		{
			this.bonusDexterity += 0.025f;
			return this.costUpgrade;
		}
		return -1;
	}
	
	public override string ParseToString()
	{
		return this.goldPrice + "," + this.gemPrice + "," + this.level + "," + this.costUpgrade + "," + this.costGold + "," + this.isCarried + "," + this.bonusDexterity;
	}
}
