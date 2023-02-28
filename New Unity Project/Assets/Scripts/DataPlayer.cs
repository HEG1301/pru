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
	
	public Equipment[] equipments;   //[tank,suit,palm,weapon]
	public List<Weapon> weapons;
	public List<Suit> suits;
	public List<Tank> tanks;
	public List<Palm> palms;
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
		this.equipments = new Equipment[4];       //[tank,suit,palm,weapon]
		this.weapons = new List<Weapon>();
		this.palms = new List<Palm>();
		this.tanks = new List<Tank>();
		this.suits = new List<Suit>();
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
	
	public void assignEquipment()
	{
		int i = 0;
		foreach (Tank tk in tanks)
		{
			if (tk.isCarried)
			{
				equipments[i] = tk;
				break;
			}
		}
		i = 1;
		foreach (Suit st in suits)
		{
			if (st.isCarried)
			{
				equipments[i] = st;
				break;
			}
		}
		i = 2;
		foreach (Palm pm in palms)
		{
			if (pm.isCarried)
			{
				equipments[i] = pm;
				break;
			}
		}
		i = 3;
		foreach (Weapon wp in weapons)
		{
			if (wp.isCarried)
			{
				equipments[i] = wp;
				break;
			}
		}
	}
	
	public void changeCarried(int newIndex,int indexType)
	{
		switch(indexType)
		{
			case 1:
				if (newIndex >= tanks.Count)
					return;
				foreach (Tank tk in tanks)
				{
					if (tk.isCarried)
					{
						tk.isCarried = false;
						break;
					}
				}
				tanks[newIndex].isCarried = true;
				equipments[0] = tanks[newIndex];
				return;
			case 2:
				if (newIndex >= suits.Count)
					return;
				foreach (Suit st in suits)
				{
					if (st.isCarried)
					{
						st.isCarried = false;
						break;
					}
				}
				suits[newIndex].isCarried = true;
				equipments[1] = suits[newIndex];
				return;
			case 3:
				if (newIndex >= palms.Count)
					return;
				foreach (Palm pm in palms)
				{
					if (pm.isCarried)
					{
						pm.isCarried = false;
						break;
					}
				}
				palms[newIndex].isCarried = true;
				equipments[2] = palms[newIndex];
				return;
			case 4:
				if (newIndex >= weapons.Count)
					return;
				foreach (Weapon wp in weapons)
				{
					if (wp.isCarried)
					{
						wp.isCarried = false;
						break;
					}
				}
				weapons[newIndex].isCarried = true;
				equipments[3] = weapons[newIndex];
				return;
			default:
				break;
		}
	}
	
	public bool bought(Equipment equipment,int index)
	{
		if (equipment.goldPrice <= this.gold && equipment.gemPrice <= this.gem)
		{
			switch (index)
			{
				case 1:
					this.tanks.Add((Tank)equipment);
					break;
				case 2:
					this.suits.Add((Suit)equipment);
					break;
				case 3:
					this.palms.Add((Palm)equipment);
					break;
				case 4:
					this.weapons.Add((Weapon)equipment);
					break;
				default:
					Debug.Log("wrong index");
					break;
			}
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
		Tank tank = (equipments.Length > 0)?(Tank)this.equipments[0]:null;
		Suit suit = (equipments.Length > 0)?(Suit)this.equipments[1]:null;
		Palm palm = (equipments.Length > 0)?(Palm)this.equipments[2]:null;
		Weapon weapon = (equipments.Length > 0)?(Weapon)this.equipments[3]:null;
		
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
