using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPlayer : MonoBehaviour
{
	private float bestDepth;
	private float numberOfMeterDive;
	private float numberOfPredatorHunt;
	private float numberOfDeathByAsphixy;
	private float numberOfDeathByPredator;
	private float numberOfDeathByObstacle;
	private float numberOfDivingDone;
	
	private List<(Equipment,bool)> equipments; //taille max de 4 (palm,suit,oxygen equipment,defense equipment)

	private float maxLife;
	private float maxApneeTime;
	private float strenght;
	
	private int level;
	private float curentExp;
	private float nextMax;
	private float gold;
	private float gem;
		
	public DataPlayer()
	{
		this.bestDepth = 0;
		this.numberOfMeterDive = 0;
		this.numberOfDeathByAsphixy = 0;
		this.numberOfDeathByObstacle = 0;
		this.numberOfDeathByPredator = 0;
		this.numberOfPredatorHunt = 0;
		this.numberOfDivingDone = 0;
		this.equipments = new List<(Equipment,bool)>{};
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
			this.equipments.Add((equipment,false));
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
		string path = Application.persistentDataPath + "/player.json";
		string data = JsonUtility.ToJson(this,prettyPrint:true);
		File.WriteAllText(path,data);
	}
	
	public DataPlayer loadData(string path)
	{
		string data = File.ReadAllText(path);
		return JsonUtility.FromJson<DataPlayer>(data);
	}
}
