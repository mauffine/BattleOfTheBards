using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour 
{

	void Start() 
    {
	}
	
	void Update() 
    {
        if(Input.GetButtonDown("Cross"))
            SwitchToGame();
        else if(Input.GetButtonDown("Circle"))
            Application.Quit();
	}

    /// <summary>Switches to the main battle state </summary>
    public void SwitchToGame()
    {
        Application.LoadLevel("BattleScene");
    }

    /// <summary>Switches to the main menu state </summary
    public void SwitchToMenu()
    {
        Application.LoadLevel("MenuScene");
    }

    /// <summary>Closes the game </summary>
    public void Quit()
    {
        Application.Quit();
    }
}
