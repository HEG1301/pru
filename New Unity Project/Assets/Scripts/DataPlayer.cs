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
	
	public List<Equipment> equipments;

	public float maxLife;
	public float maxApneeTime;
	public float strenght;
	
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
		this.bestDepth = 0;
		this.numberOfMeterDive = 0;
		this.numberOfDeathByAsphixy = 0;
		this.numberOfDeathByObstacle = 0;
		this.numberOfDeathByPredator = 0;
		this.numberOfPredatorHunt = 0;
		this.numberOfDivingDone = 0;
		this.equipments = new List<Equipment>();
		this.maxLife = 100;
		this.maxApneeTime = 0.45f; //temps moyen d'une personne de 45 seconde
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
}
