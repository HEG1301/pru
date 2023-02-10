using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class BetweenSceneInfo : MonoBehaviour
{
	
	public static BetweenSceneInfo instance;
	
	
	public string name;
	public GameObject panel;
	public TMP_InputField inputName;
	public TMP_InputField inputAnswer;
    // Start is called before the first frame update
    void Start()
    {
    }
	
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

    // Update is called once per frame
    void Update()
    {
        
    }
	
	
	private bool isYesOrNo(string s,string sceneName)
	{
		s.ToLower();
		//Debug.Log(inputAnswer.GetComponent<TMP_InputField>().text);
		switch (s)
		{
			case "y":
			case "yes":
			case "oui":
			case "o":
			case "t":
			case "true":
				DontDestroyOnLoad(this.gameObject);
				DataPlayer temp = new DataPlayer();
				Debug.Log(temp);
				temp.Name = name;
				temp.saveData();
				StopAllCoroutines();
				SceneManager.LoadScene(sceneName);
				return true;
			case "n":
			case "no":
			case "non":
			case "f":
			case "false":
				inputName.GetComponent<TMP_InputField>().text = "";
				inputAnswer.GetComponent<TMP_InputField>().text = "";
				this.name = "";
				return true;
			default:
				return false;
		}
	}
	
	public void GoToMenu()
	{
		string sceneName = "Menu";
		this.name = inputName.GetComponent<TMP_InputField>().text;
		Debug.Log(File.Exists(Application.persistentDataPath + "/" + this.name + "_data.json"));
        if (!File.Exists(Application.persistentDataPath + "/" + this.name + "_data.json"))
		{
			Debug.Log("Creation nouveau compte");
			this.panel.SetActive(true);
			StartCoroutine(cor(sceneName));
		
		}
		else
		{
			inputName.GetComponent<TMP_InputField>().text = "";
			DontDestroyOnLoad(this.gameObject);
			SceneManager.LoadScene(sceneName);
		}
	}
	
	IEnumerator cor(string sceneName)
	{
		Debug.Log("startCoroutine");
		int i = 0;
		while (!isYesOrNo(inputAnswer.GetComponent<TMP_InputField>().text,sceneName) && i < 1200)
		{
			
			i += 1;
			yield return new WaitForSeconds(0.1f);
		}
		this.panel.SetActive(false);
		if (i == 1200)
		{
			isYesOrNo("no",sceneName);
		}
	}
}
