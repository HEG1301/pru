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
	private bool atSurface = false;
	public bool inCave = false;
	
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
		//Debug.Log(temp);
		if (temp == null)
		{
			this.OxyMax = 24f/12f*60f;    //= capacity tank / 12 L * 60 => capacity tank by default will be 24, 12 L = we consum 12 L of oxy per minute when diving, * 60 convert minute to second
			this.maxApnee = 45f; 			//default apnee time;
			this.lifeMax = 100f;
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
		float perteOxy = 1.66f;
		bool isRunning = false;
		if (Input.GetKey(KeyCode.LeftShift))
		{
			isRunning = true;
			perteOxy = 2;
		}
		if (this.Die)
			return;
		if (!this.atSurface && !this.inCave)
			if (this.OxyCurent <= 0)
			{	
				if (this.curentApnee > 0)
				{
					this.curentApnee -= perteOxy * Time.deltaTime;
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
				this.OxyCurent -= perteOxy * Time.deltaTime;
			}
		else if (this.atSurface)
		{
			if (this.OxyCurent < OxyMax)
			{
				this.OxyCurent += 1 * Time.deltaTime;
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
		float[] t = getAngle(dir);
		Vector3 test = new Vector3(90,0,t[0]);
		this.transform.eulerAngles = test;
		Vector3 test2 = this.gameObject.GetComponentInChildren<Camera>().gameObject.transform.eulerAngles;
		this.gameObject.GetComponentInChildren<Camera>().gameObject.transform.eulerAngles = new Vector3(test2.x,t[1],test2.z);
		if ((dir.z + dir.x)%2!=0)
			dir *= 1.5f;
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
	
	public float[] getAngle(Vector3 dir)
	{
		if (dir.x != 0 && dir.z != 0)
		{
			if (dir.x == dir.z)
			{
				if (dir.x == -1)
					return new float[]{135,0};
				else
					return new float[]{315,0};
			}
			else
				if (dir.x == -1)
					return new float[]{45,0};
				else
					return new float[]{225,0};
		}
		else if (dir.x != 0)
		{
			if (dir.x == -1)
				return new float[]{90,0};
			else
				return new float[]{270,0};
		}
		else if (dir.z != 0)
		{
			if (dir.z == -1)
				return new float[]{180,0};
			else
				return new float[]{0,0};
		}
		else
			return new float[]{0,0};
	}
}




