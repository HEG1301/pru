using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class goToGame : MonoBehaviour
{
	public static goToGame instance;
	
	[HideInInspector]
	public DataPlayer player;
	
	public int[] UIdata = new int[6];
	
	void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(this);
		}
	}
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {   
    }
	
	
	public void startGame(string s)
	{
		Menu m = GameObject.Find("Canvas").GetComponent<Menu>();
		
		//need to be cast to keyCode and int 
		//this.UIdata = new int[6]{m.rigthinput.text,m.leftinput.text,m.divinginput.text,m.risinginput.text,m.speedinput.text}
		this.player = m.player;
		DontDestroyOnLoad(this.gameObject);
		SceneManager.LoadScene(s);
	}
}
