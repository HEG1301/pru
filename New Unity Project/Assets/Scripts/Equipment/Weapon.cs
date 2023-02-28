using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Equipment
{
    public float damage;
	
	public Weapon()
	{
		this.costUpgrade = 50;
		this.damage = 20;
	}
	public Weapon(int costUpgrade,float bonusStrenght)
	{
		this.costUpgrade = (costUpgrade<50)?50:costUpgrade;
		this.damage = (damage < 20)?20:damage;
		this.level = 1; 
	}
	
	public override int upgrade(float gold)
	{
		if (gold >= this.costUpgrade)
		{
			this.damage += 5;
			return this.costUpgrade;
		}
		return -1;
	}
	
	public override string ParseToString()
	{
		return this.goldPrice + "," + this.gemPrice + "," + this.level + "," + this.costUpgrade + "," + this.costGold + "," + this.isCarried + "," + this.damage;
	}
}
