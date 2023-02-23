using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInGame : MonoBehaviour
{
	[HideInInspector]
	public DataPlayer player;
	
	public TextMeshProUGUI textMoney;
	public Slider sliderApnee;
	public Slider sliderOxy;
	public float money;
	public float speed;
	
	private float OxyMax;
	private float OxyCurent;
	private bool atSurface;
	
	private float maxApnee;
	private float curentApnee;
	private bool Die;
	
	private KeyCode left;
	private KeyCode right;
	private KeyCode rise;
	private KeyCode dive;	
	private KeyCode run;
    // Start is called before the first frame update
    void Start()
    {		
		GameObject temp = GameObject.Find("Balade");
		if (temp == null)
		{
			this.OxyMax = 24f/12f*60f;    //= capacity tank / 12 L * 60 => capacity tank by default will be 24, 12 L = we consum 12 L of oxy per minute when diving, * 60 convert minute to second
			this.maxApnee = 45f;           //default apnee time;
			this.speed = 10f;
			this.right = KeyCode.RightArrow;
			this.left = KeyCode.LeftArrow;
			this.dive = KeyCode.DownArrow;
			this.rise = KeyCode.UpArrow;
			this.run = KeyCode.LeftShift;
		}
		else
		{
			player = temp.GetComponent<goToGame>().player;
			this.OxyMax = (24f+player.bonusEquipement("oxy"))/16f*60f;  
			this.maxApnee = player.maxApneeTime;
			this.speed = player.strenght/2f + player.bonusEquipement("speed");
			getKey(temp.GetComponent<goToGame>().UIdata);
		}
		Destroy(temp);
		this.OxyCurent = OxyMax;
		this.curentApnee = maxApnee;
		this.money = 0f;
		
		this.sliderApnee.minValue = 0;
		this.sliderOxy.minValue = 0;
		this.sliderApnee.maxValue = maxApnee;
		this.sliderOxy.maxValue = OxyMax;
    }

    // Update is called once per frame
    void Update()
    {
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
    }
	
	
	public void getKey(int[] key)
	{
		this.right = (KeyCode) key[0];
		this.left = (KeyCode) key[1];
		this.dive = (KeyCode) key[2];
		this.rise = (KeyCode) key[3];
		this.run = (KeyCode) key[4];
	}
}
