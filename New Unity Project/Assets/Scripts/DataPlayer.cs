using System.IO;
using System.Collections;
using System;
using UnityEngine;
using System.Collections.Generic;


//[System.Serializable]
public class DataPlayer //: MonoBehaviour
{
	public float bestDepth;
	public float bestScore;
	public float numberOfMeterDive;
	public float numberOfPredatorHunt;
	public float numberOfDeathByAsphixy;
	public float numberOfDeathByPredator;
	public float numberOfDeathByObstacle;
	public float numberOfDivingDone;
	
	public List<Equipment> equipments;   //[tank,suit,palm,weapon]

	public float maxLife;
	public float dexterity;
	public float strenght;

	public float costLife;
	public float costDexterity;
	public float costStrenght;
	
	public float maxApneeTime;
	
	public int level;
	public float curentExp;
	public float nextMax;
	public float gold;
	public float gem;
	
	public string name;
	
	public string Name
	{
		get {return this.name;}
		set {this.name = value;}
	}
		
	public DataPlayer()
	{
		this.name = "";
		this.costLife = 25;
		this.costStrenght = 50;
		this.costDexterity = 75;
		this.bestDepth = 0;
		this.bestScore = 0;
		this.numberOfMeterDive = 0;
		this.numberOfDeathByAsphixy = 0;
		this.numberOfDeathByObstacle = 0;
		this.numberOfDeathByPredator = 0;
		this.numberOfPredatorHunt = 0;
		this.numberOfDivingDone = 0;
		this.equipments = new List<Equipment>();
		this.maxLife = 100;
		this.dexterity = 25;
		this.maxApneeTime = 45f; //temps moyen d'une personne de 45 seconde
		this.strenght = 10;
		this.level = 1;
		this.curentExp = 0;
		this.nextMax = 50;
		this.gold = 1500;
		this.gem = 100;
	}
	
	
	
	public bool bought(Equipment equipment)
	{
		if (equipment.goldPrice <= this.gold && equipment.gemPrice <= this.gem)
		{
			this.equipments.Add(equipment);
			this.gold -= equipment.goldPrice;
			this.gem -= equipment.gemPrice;
			return true;
		}
		else if (equipment.goldPrice <= this.gold)
		{
			Debug.Log("Not enough gem to buy the equipment");
		}
		else
		{
			Debug.Log("Not enough gold to buy the equipment");
		}
		return false;
	}
	
	public bool checkLevel()
	{
		if (this.curentExp >= nextMax)
		{
			this.curentExp -= nextMax;
			nextMax *= 1.5f;
			this.level += 1;
			this.gem += 15;
			return true;
		}
		return false;
	}
	
	public void saveData()
	{
		string path = Application.persistentDataPath + "/" + this.name+ "_data.json";
		string data = JsonUtility.ToJson(this,prettyPrint:true);
		File.WriteAllText(path,data);
	}
	
	public static DataPlayer loadData(string path)
	{
		string data = File.ReadAllText(path);
		return JsonUtility.FromJson<DataPlayer>(data);
	}
	
	public bool upgradeSkill(int index)
	{
		switch (index)
		{
			case 1:
				if (this.gold > this.costLife)
				{
					this.gold -= this.costLife;
					this.costLife *= 2;
					this.maxLife += 50;
					return true;
				}
				return false;
			case 2:
				if (this.gold > this.costStrenght)
				{
					this.gold -= this.costStrenght;
					this.costStrenght *= 2.5f;
					this.strenght += 5f;
					return true;
				}
				return false;
			case 3:
				if (this.gold > this.costDexterity)
				{
					this.gold -= this.costDexterity;
					this.costDexterity *= 3;
					this.dexterity += 2.5f;
					return true;
				}
				return false;
			default:
				return false;
		}
	}
	
	public bool upgradeEquipment(int index)
	{
		int tmp = equipments[index].upgrade(this.gold);
		if (tmp != -1)
		{
			this.gold -= tmp;
			return true;
		}
		return false;
	}
	
	public float statAndBonusEquipement(string stat)
	{
		Tank tank = (Tank)this.equipments[0];
		Suit suit = (Suit)this.equipments[1];
		Palm palm = (Palm)this.equipments[2];
		Weapon weapon = (Weapon)this.equipments[3];
		
		float tmpStrength = this.strenght*(((palm != null)?palm.bonusStrenght:1f) - ((tank != null)?tank.malusStrenght:0f));
		float tmpDext = this.dexterity*(((suit != null)?suit.bonusDexterity:1f) - ((tank != null)?tank.malusDexterity:0f));
		switch (stat)
		{
			case "damage":
				if (weapon != null)
					return weapon.damage;
				return 10;
			case "oxy":
				if (tank != null)
					return tank.oxyCapacity * 60f / (float)(20-(int)(this.level / 5));
				return 0f;
			case "speed":
				return (tmpStrength/4f + tmpDext/5f);
			case "life":
				return this.maxLife;
			case "apne":
				return Mathf.Clamp(tmpStrength + this.maxLife/3f,0,300);
			default:
				return 0f;
		}
	}
}
