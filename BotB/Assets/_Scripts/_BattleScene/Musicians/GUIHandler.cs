using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIHandler : MonoBehaviour 
{
    private static GUIHandler instance;
	// Use this for initialization
	void Start() 
    {
        instance = this;
	}
	
	// Update is called once per frame
	void Update() 
    {
	
	}

    public static GUIHandler Instance
    {
        get { return instance; }
    }

    public Slider EnemyLifeBar
    {
        get {return GetComponentsInChildren<Slider>()[1];}
    }
        
}
