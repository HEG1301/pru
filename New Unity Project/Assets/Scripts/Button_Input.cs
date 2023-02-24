using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Button_Input : MonoBehaviour
{
	private string oldLetter;
	
	public void loadScene(string s)
	{
		SceneManager.LoadScene(s);
	}
	
	public void QuitGame()
	{
		Application.Quit();
	}
	
	public void BackToStart(string s)
	{
		DataPlayer p = GameObject.Find("Canvas").GetComponent<Menu>().player;
		p.saveData();
		loadScene(s);
	}
	public void AddGold(GameObject canvas)
	{
		DataPlayer player = canvas.GetComponent<Menu>().player;
		player.gold += 100;
	}
	
	public void saveLetter(GameObject Input)
	{
		oldLetter = Input.GetComponent<TMP_InputField>().text;
		Input.GetComponent<TMP_InputField>().text = "";
	}
	
	public void verifEnter(GameObject Input)
	{
		string letter = Input.GetComponent<TMP_InputField>().text;
		if (letter.Length != 1)
		{
			Debug.Log("the letter is longer than one character");
			Input.GetComponent<TMP_InputField>().text = oldLetter;
		}
		else
		{
			GameObject temp = GameObject.Find("GameControl");
			Component[] tmpL = temp.GetComponentsInChildren(typeof(TMP_InputField));
			foreach (Component cpnt in tmpL)
			{
				if (cpnt.gameObject == Input.gameObject)
				{
					continue;
				}
				else
				{
					if (((TMP_InputField)cpnt).text == letter)
					{
						Debug.Log("the letter is already used");
						Input.GetComponent<TMP_InputField>().text = oldLetter;
						break;
					}
				}
				
			}
		}
	}
}
