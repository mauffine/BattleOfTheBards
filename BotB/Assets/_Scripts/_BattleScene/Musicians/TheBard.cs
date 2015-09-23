using UnityEngine;
using System.Collections;

public class TheBard : Musician
{
	// Use this for initialization
	void Start() 
    {
        base.Start();
	}
	// Update is called once per frame
	void Update() 
    {
	}
    //
    protected override void Die()
    {
    }
    public override void Animate(short a_animationNum)
    {
        switch (a_animationNum)
        {
            case 1:
                GetComponentInChildren<Animator>().SetTrigger("PlayChord1");
                break;
            case 2:
                GetComponentInChildren<Animator>().SetTrigger("PlayChord2");
                break;
            case 3:
                GetComponentInChildren<Animator>().SetTrigger("PlayChord3");
                break;
            case 4:
                GetComponentInChildren<Animator>().SetTrigger("PlayChord4");
                break;
            case 5:
                GetComponentInChildren<Animator>().SetTrigger("PlayHurt");
                break;
            default:
                break;
        }
    }
}
