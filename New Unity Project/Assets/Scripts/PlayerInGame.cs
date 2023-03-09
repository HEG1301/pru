using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerInGame : MonoBehaviour
{
	[HideInInspector]
	public DataPlayer player;
	
	public TextMeshProUGUI textMoney;
	public Slider sliderApnee;
	public Slider sliderOxy;
	public Slider sliderLife;
	public float money;
	public float speed;
	public float damage;
	public float lifeMax;
	public float lifeCurent;
	public float OxyMax;
	public float OxyCurent;
	private bool atSurface;
	
	public float maxApnee;
	public float curentApnee;
	private bool Die;
	
	private KeyCode left;
	private KeyCode right;
	private KeyCode rise;
	private KeyCode dive;	
	private KeyCode run;
    // Start is called before the first frame update
    void Start()
    {		
		GameObject temp = GameObject.Find("ScriptsCarrier");
		Debug.Log(temp);
		if (temp == null)
		{
			this.OxyMax = 24f/12f*60f;    //= capacity tank / 12 L * 60 => capacity tank by default will be 24, 12 L = we consum 12 L of oxy per minute when diving, * 60 convert minute to second
			this.maxApnee = 45f;           //default apnee time;
			this.speed = 10f/3;
			this.damage = 20f;
			this.right = KeyCode.RightArrow;
			this.left = KeyCode.LeftArrow;
			this.dive = KeyCode.DownArrow;
			this.rise = KeyCode.UpArrow;
			this.run = KeyCode.LeftShift;
		}
		else
		{
			player = temp.GetComponent<goToGame>().player;
			this.OxyMax = this.player.statAndBonusEquipement("oxy");
			this.maxApnee = this.player.statAndBonusEquipement("apne");
			this.speed = this.player.statAndBonusEquipement("speed")/3;
			this.damage = this.player.statAndBonusEquipement("damage");
			this.lifeMax = this.player.statAndBonusEquipement("life");
			getKey(temp.GetComponent<goToGame>().UIdata);
		}
		Destroy(temp);
		this.OxyCurent = OxyMax;
		this.lifeCurent = lifeMax;
		this.curentApnee = maxApnee;
		this.money = 0f;
		
		this.sliderLife.minValue = 0;
		this.sliderApnee.minValue = 0;
		this.sliderOxy.minValue = 0;
		this.sliderLife.maxValue = lifeMax;
		this.sliderApnee.maxValue = maxApnee;
		this.sliderOxy.maxValue = OxyMax;
    }

    // Update is called once per frame
    void Update()
    {
		
		if (Input.GetKeyUp(KeyCode.Escape))
		{
			toGame();
		}
		this.sliderLife.value = lifeCurent;
		this.sliderApnee.value = curentApnee;
		this.sliderOxy.value = OxyCurent;
		if (this.Die)
			return;
		if (!this.atSurface)
			if (this.OxyCurent <= 0)
			{	
				if (this.curentApnee > 0)
				{
					this.curentApnee -= 1f * Time.deltaTime;
				}
				else
				{
					Debug.Log("player has die");
					this.Die = true;
					toGame();
				}
			}
			else
			{
				this.OxyCurent -= 1f * Time.deltaTime;
			}
		else
		{
			if (this.OxyCurent < OxyMax)
			{
				this.OxyCurent += 0.25f * Time.deltaTime;
			}
			if (this.OxyCurent > OxyMax)
			{
				this.OxyCurent = OxyMax;
			}
			if (this.curentApnee < maxApnee)
			{
				this.curentApnee = maxApnee;
			}
		}
		
		textMoney.text = "Money : " + money;
		
		/*
		Vector3 pos = this.transform.position;
        if (Input.GetKey(KeyCode.DownArrow))
		{
			pos.y -= speed * Time.deltaTime;
		}
		else if (Input.GetKey(KeyCode.UpArrow))
		{
			pos.y += speed * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.RightArrow))
		{
			pos.x += speed * Time.deltaTime;
		}
		else if (Input.GetKey(KeyCode.LeftArrow))
		{
			pos.x -= speed * Time.deltaTime;
		}
		//pos.Normalize();
		this.transform.position = pos;
		*/
		bool isRunning = false;
		if (Input.GetKey(KeyCode.LeftShift))
			isRunning = true;
		Vector3 dir = new Vector3(0,0,0);
		this.gameObject.GetComponent<Rigidbody>().velocity = dir;
		if (Input.GetKey(KeyCode.DownArrow))
		{
			Debug.Log("Down");
			dir.z = -1;
		}
		else if (Input.GetKey(KeyCode.UpArrow))
		{
			Debug.Log("Up");
			dir.z = 1;
		}
		if (Input.GetKey(KeyCode.RightArrow))
		{
			Debug.Log("right");
			dir.x = 1;
		}
		else if (Input.GetKey(KeyCode.LeftArrow))
		{
			Debug.Log("left");
			dir.x = -1;
		}
		//Debug.Log(dir);
		this.gameObject.GetComponent<Rigidbody>().AddForce(dir*speed*(1+((isRunning)?0.5f:0)),ForceMode.Impulse);
    }
	
	
	public void getKey(int[] key)
	{
		this.right = (KeyCode) key[0];
		this.left = (KeyCode) key[1];
		this.dive = (KeyCode) key[2];
		this.rise = (KeyCode) key[3];
		this.run = (KeyCode) key[4];
	}
	
	public void toGame()
	{
		GameObject tmp = GameObject.Find("Balade");
		player.gold += money;
		player.bestScore = (player.bestScore > money)?player.bestScore:money;
		player.saveData();
		Debug.Log(tmp);
		Debug.Log(tmp.GetComponent<fromGame>());
		tmp.GetComponent<fromGame>().player = this.player;
		DontDestroyOnLoad(tmp);
		SceneManager.LoadScene("Menu");
	}
}
