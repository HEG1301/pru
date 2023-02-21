using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInGame : MonoBehaviour
{
	public TextMeshProUGUI textMoney;
	public float money;
	public float speed;
    // Start is called before the first frame update
    void Start()
    {
        this.speed = 10;
		this.money = 0;
    }

    // Update is called once per frame
    void Update()
    {
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
		
		this.transform.position = pos;
    }
}
