using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class fromGame : MonoBehaviour
{
	public static fromGame instance;
	
	[HideInInspector]
	public DataPlayer player;
	
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

	public void quitGame(string s)
	{
		this.player = GameObject.Find("Player").GetComponent<PlayerInGame>().player;
		DontDestroyOnLoad(this.gameObject);
		SceneManager.LoadScene(s);
	}
}
