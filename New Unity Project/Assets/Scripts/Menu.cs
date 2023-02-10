using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Menu : MonoBehaviour
{
	[HideInInspector]
	private DataPlayer player;

	[HideInInspector]
	private Equipment test;
	public TextMeshProUGUI namePlayer;
    // Start is called before the first frame update
    void Start()
    {
        GameObject temp = GameObject.Find("Balade");
		player = DataPlayer.loadData(Application.persistentDataPath + "/" + temp.GetComponent<BetweenSceneInfo>().name + "_data.json");
		Debug.Log(player.Name);
		Debug.Log(Application.persistentDataPath);
		Destroy(temp);
		test = new Equipment();
    }

    // Update is called once per frame
    void Update()
    {
        this.namePlayer.text = this.player.Name;
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
