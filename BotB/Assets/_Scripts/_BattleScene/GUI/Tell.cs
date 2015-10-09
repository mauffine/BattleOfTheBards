using UnityEngine;
using System.Collections;

public class Tell : MonoBehaviour 
{
    [SerializeField]
    public Sprite[] spriteList;

    static Tell instance;
	public void Start() 
    {
	    instance = this;    
	}
	
	void Update() 
    {
	
	}
    static public Sprite Offencive
    {
        get { return instance.spriteList[0];}
    }

    static public Sprite Defensive
    {
        get { return instance.spriteList[1];}
    }

    static public Sprite Effect
    {
        get { return instance.spriteList[2];}
    }
}
