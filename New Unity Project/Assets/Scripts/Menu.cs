using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Menu : MonoBehaviour
{
	[HideInInspector]
	public DataPlayer player;

	[HideInInspector]
	private Equipment test;
	public TextMeshProUGUI namePlayer;
	
	#region playerInformation
	public TextMeshProUGUI levelPlayer;
	public Slider sliderLevel;
	#endregion
	
	#region ressources
	public TextMeshProUGUI textGold;
	public TextMeshProUGUI textGem;
	#endregion
	
	#region statistique
	public TextMeshProUGUI bestDepthText;
	public TextMeshProUGUI numberOfDivingDoneText;	
	public TextMeshProUGUI bestScoreInGameText;
	public TextMeshProUGUI numberOfMeterDiveText;
	public TextMeshProUGUI numberOfPredatorHuntText;
	public TextMeshProUGUI numberTotalDeathText;
	public TextMeshProUGUI numberOfDeathByAsphixyText;
	public TextMeshProUGUI numberOfDeathByPredatorText;
	public TextMeshProUGUI numberOfDeathByObstacleText;
	#endregion
	
	#region skill_and_equipement
	public TextMeshProUGUI hpText;
	public TextMeshProUGUI dexText;
	public TextMeshProUGUI strText;
	
	public TextMeshProUGUI hpCostText;
	public TextMeshProUGUI dexCostText;
	public TextMeshProUGUI strCostText;
	
	/*
	public Image hpRessourceImg;
	public Image dexRessourceImg;
	public Image strRessourceImg;
	*/
	#endregion
	
	#region parameter
	public TMP_InputField rigthinput;
	public TMP_InputField leftinput;
	public TMP_InputField divinginput;
	public TMP_InputField risinginput;
	public TMP_InputField speedinput;
	#endregion
    // Start is called before the first frame update
    void Start()
    {
        GameObject temp = GameObject.Find("Balade");
		if (temp != null)
		{
			if (temp.GetComponent<BetweenSceneInfo>() != null)
				player = DataPlayer.loadData(Application.persistentDataPath + "/" + temp.GetComponent<BetweenSceneInfo>()._name + "_data.json");
			else if (temp.GetComponent<fromGame>() != null)
				player = temp.GetComponent<fromGame>().player;
		}
		else
		{
			player = new DataPlayer();
			player.name = "trashaccount";
		}
		//Debug.Log(player.Name);
		//Debug.Log(Application.persistentDataPath);
		Destroy(temp);
		test = new Equipment();
    }

    // Update is called once per frame
    void Update()
    {
        this.namePlayer.text = this.player.Name;
		this.textGem.text = this.player.gem + "";
		this.textGold.text = this.player.gold + "";
		
		this.bestDepthText.text = this.player.bestDepth + "";
		this.numberOfDivingDoneText.text = this.player.numberOfDivingDone + "";
		this.bestScoreInGameText.text = this.player.bestScore + "";
		this.numberOfMeterDiveText.text = this.player.numberOfMeterDive + "";
		this.numberOfPredatorHuntText.text = this.player.numberOfPredatorHunt + "";
		this.numberTotalDeathText.text = (this.player.numberOfDeathByAsphixy + this.player.numberOfDeathByObstacle + this.player.numberOfDeathByPredator) + "";
		this.numberOfDeathByAsphixyText.text = this.player.numberOfDeathByAsphixy + "";
		this.numberOfDeathByObstacleText.text = this.player.numberOfDeathByObstacle + "";
		this.numberOfDeathByPredatorText.text = this.player.numberOfDeathByPredator + "";
		
		
		this.hpText.text = "HP : " + this.player.maxLife;
		this.dexText.text = "Dex : " + this.player.dexterity;
		this.strText.text = "Str : " + this.player.strenght;
	
		this.hpCostText.text = "" + this.player.costLife;
		this.dexCostText.text = "" + this.player.costDexterity;
		this.strCostText.text = ""  + this.player.costStrenght;
	
		
		
		//Debug.Log("test");
		
		if(Input.GetKeyUp(KeyCode.Escape))
		{
			GameObject tmp = this.transform.Find("Statistique").gameObject;
			if (tmp == null)
			{
				Debug.Log("erreur stat not find");
				return;
			}
			Debug.Log(tmp.name); //+ " | " + tmp.activeInHierarchy + " | " + tmp.activeSelf);
			if (tmp.activeInHierarchy)
			{
				tmp.SetActive(false);
			}
			else
			{
				GameObject tmp2 = this.transform.Find("Filter").gameObject;
				Debug.Log(tmp2.name);
				tmp2.SetActive(true);
			}
			//Debug.Log(tmp.name + " | " + tmp.activeInHierarchy + " | " + tmp.activeSelf);
			GameObject tmp3 = this.transform.Find("Parameter").gameObject;
			Debug.Log(tmp3.name);
			tmp3.SetActive(true);
		}
    }
	
	public bool upgrade(int index)
	{
		if (index <= 3)
			return this.player.upgradeSkill(index);
		else
			return this.player.upgradeEquipment(index-4);
	}
	void OnDisable()
    {
        player.saveData();
    }
    void OnApplicationQuit()
    {
        player.saveData();
    }
}
