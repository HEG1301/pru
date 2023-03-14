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
	
	
	public GameObject PanelWeapon;
	public GameObject PanelTank;
	public GameObject PanelSuit;
	public GameObject PanelPalm;
	public Image imageWeapon;
	public Image imageTank;
	public Image imagePalm;
	public Image imageSuit;
	public TextMeshProUGUI textWeapon;
	public TextMeshProUGUI textTank;
	public TextMeshProUGUI textPalm;
	public TextMeshProUGUI textSuit;
	public TextMeshProUGUI textCostUpWeapon;
	public TextMeshProUGUI textCostUpTank;
	public TextMeshProUGUI textCostUpPalm;
	public TextMeshProUGUI textCostUpSuit;
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
			{
				player = DataPlayer.loadData(Application.persistentDataPath + "/" + temp.GetComponent<BetweenSceneInfo>()._name + "_data.json");
				player.assignEquipment();
			}
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
		Image[] tmpI = new Image[]{imageWeapon,imageTank,imageSuit,imagePalm};
		for (int i = 1;i<5;i++)
		{
			Transform tmp = PanelWeapon.transform.Find("Object " + i);
			Button[] tmpB = tmp.gameObject.GetComponentsInChildren<Button>();
			//Debug.Log(tmpB.Length + "|" + i);
			if (player.weapons[i-1].isBougth)
			{
				tmpB[0].gameObject.SetActive(false);
				if (player.weapons[i-1].isCarried)
				{
					tmpI[i-1].sprite = tmp.Find("Image").gameObject.GetComponent<Image>().sprite;
				}
			}
			else
			{
				tmpB[0].gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Bougth for: " + player.weapons[i-1].goldPrice;
				tmpB[1].gameObject.SetActive(false);
			}
			tmp.Find("description").GetComponent<TextMeshProUGUI>().text = player.weapons[i-1].getDescription();
		}
		for (int i = 1;i<5;i++)
		{
			Transform tmp = PanelTank.transform.Find("Object " + i);
			Button[] tmpB = tmp.gameObject.GetComponentsInChildren<Button>();
			//Debug.Log(tmpB.Length + "|" + i);
			if (player.tanks[i-1].isBougth)
			{
				tmpB[0].gameObject.SetActive(false);
				if (player.tanks[i-1].isCarried)
				{
					tmpI[i-1].sprite = tmp.Find("Image").gameObject.GetComponent<Image>().sprite;
				}
			}
			else
			{
				tmpB[0].gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Bougth for: " + player.tanks[i-1].goldPrice;
				tmpB[1].gameObject.SetActive(false);
			}
			tmp.Find("description").GetComponent<TextMeshProUGUI>().text = player.weapons[i-1].getDescription();
		}
		for (int i = 1;i<5;i++)
		{
			Transform tmp = PanelSuit.transform.Find("Object " + i);
			Button[] tmpB = tmp.gameObject.GetComponentsInChildren<Button>();
			//Debug.Log(tmpB.Length + "|" + i);
			if (player.suits[i-1].isBougth)
			{
				tmpB[0].gameObject.SetActive(false);
				if (player.suits[i-1].isCarried)
				{
					tmpI[i-1].sprite = tmp.Find("Image").gameObject.GetComponent<Image>().sprite;
				}
			}
			else
			{
				tmpB[0].gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Bougth for: " + player.suits[i-1].goldPrice;
				tmpB[1].gameObject.SetActive(false);
			}
			tmp.Find("description").GetComponent<TextMeshProUGUI>().text = player.weapons[i-1].getDescription();
		}
		for (int i = 1;i<5;i++)
		{
			Transform tmp = PanelPalm.transform.Find("Object " + i);
			Button[] tmpB = tmp.gameObject.GetComponentsInChildren<Button>();
			//Debug.Log(tmpB.Length + "|" + i);
			if (player.palms[i-1].isBougth)
			{
				tmpB[0].gameObject.SetActive(false);
				if (player.palms[i-1].isCarried)
				{
					tmpI[i-1].sprite = tmp.Find("Image").gameObject.GetComponent<Image>().sprite;
				}
			}
			else
			{
				tmpB[0].gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Bougth for: " + player.palms[i-1].goldPrice;
				tmpB[1].gameObject.SetActive(false);
			}
			tmp.Find("description").GetComponent<TextMeshProUGUI>().text = player.palms[i-1].getDescription();
		}
		
    }

    // Update is called once per frame
    void Update()
    {
		for (int i = 1;i<5;i++)
		{
			Transform tmp = PanelWeapon.transform.Find("Object " + i);
			//Button[] tmpB = tmp.gameObject.GetComponentsInChildren<Button>();
			//Debug.Log(tmpB.Length + "|" + i);
			//tmpB[0].gameObject.SetActive(!player.weapons[i-1].isCarried);
			tmp.Find("description").GetComponent<TextMeshProUGUI>().text = player.weapons[i-1].getDescription();
		}
		
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
	
		this.textWeapon.text = (this.player.equipments[3] != null)?"Weapon    Level" + this.player.equipments[3].level:"No Weapon Select";
		this.textTank.text = (this.player.equipments[0] != null)?"Tank    Level" + this.player.equipments[0].level:"No Tank Select";
		this.textPalm.text = (this.player.equipments[1] != null)?"Suit    Level" + this.player.equipments[1].level:"No Suit Select";
		this.textSuit.text = (this.player.equipments[2] != null)?"Palm    Level" + this.player.equipments[2].level:"No Palm Select";
		this.textCostUpWeapon.text = "cost:\n" + ((this.player.equipments[3] != null)?this.player.equipments[3].costUpgrade +"":0+"");
		this.textCostUpTank.text = "cost:\n" + ((this.player.equipments[0] != null)?this.player.equipments[0].costUpgrade +"":0+"");
		this.textCostUpPalm.text = "cost:\n" + ((this.player.equipments[1] != null)?this.player.equipments[1].costUpgrade +"":0+"");
		this.textCostUpSuit.text = "cost:\n" + ((this.player.equipments[2] != null)?this.player.equipments[2].costUpgrade +"":0+"");
		
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
	
	public void Bougth(int index)
	{
		switch (index/10)
		{
			case 1:
				player.bought(player.weapons[index%10-1],1);
				break;
			case 2:
				player.bought(player.palms[index%10-1],2);
				break;
			case 3:
				player.bought(player.suits[index%10-1],3);
				break;
			case 4:				
				player.bought(player.palms[index%10-1],4);
				break;
			default:
				Debug.LogWarning("wrong index");
				return;
		}
		Debug.Log("successfull transaction");
	}
	
	public void Change(int index)
	{
		if (player.changeCarried(index%10-1,index/10))
			Debug.Log("Change SuccessFull");
		else
			Debug.Log("Change failed");
	}
	
	public void switchImageW(Image newImg)
	{
		this.imageWeapon.sprite = newImg.sprite;
	}
	public void switchImageT(Image newImg)
	{
		this.imageTank.sprite = newImg.sprite;
	}
	public void switchImageP(Image newImg)
	{
		this.imagePalm.sprite = newImg.sprite;
	}
	public void switchImageS(Image newImg)
	{
		this.imageSuit.sprite = newImg.sprite;
	}
	
	
	public void upgrade(int index)
	{
		if (index <= 3)
			Debug.Log(this.player.upgradeSkill(index));
		else
			Debug.Log(this.player.upgradeEquipment(index-4));
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
