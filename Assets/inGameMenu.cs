using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inGameMenu : MonoBehaviour {

	void OnGUI()
	{
		if(GUI.Button(new Rect(20,20,100,50), "Menu")) 
		{
			Application.LoadLevel(0);
		}
	}
}
