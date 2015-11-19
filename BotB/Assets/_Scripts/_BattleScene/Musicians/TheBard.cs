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
        m_lifeBar.value = m_health;
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
                GetComponentInChildren<Animator>().SetTrigger("PlayNote01");
                break;
            case 2:
                GetComponentInChildren<Animator>().SetTrigger("PlayNote02");
                break;
            case 3:
                GetComponentInChildren<Animator>().SetTrigger("PlayNote03");
                break;
            case 4:
                GetComponentInChildren<Animator>().SetTrigger("PlayNote04");
                break;
            case 5:
                GetComponentInChildren<Animator>().SetTrigger("PlayNote05");
                break;
            case 6:
                GetComponentInChildren<Animator>().SetTrigger("PlayNote06");
                break;
            case 7:
                GetComponentInChildren<Animator>().SetTrigger("PlayNote07");
                break;
            case 8:
                GetComponentInChildren<Animator>().SetTrigger("Fire01");
                break;
            case 9:
                GetComponentInChildren<Animator>().SetTrigger("Fire02");
                break;
            case 10:
                GetComponentInChildren<Animator>().SetTrigger("Fire03");
                break;
            case 11:
                GetComponentInChildren<Animator>().SetTrigger("Win01");
                break;
            case 12:
                GetComponentInChildren<Animator>().SetTrigger("Win02");
                break;
            case 13:
                GetComponentInChildren<Animator>().SetTrigger("Win03");
                break;
            case 14:
                GetComponentInChildren<Animator>().SetTrigger("Lose01");
                break;
            case 15:
                GetComponentInChildren<Animator>().SetTrigger("Lose02");
                break;
            case 16:
                GetComponentInChildren<Animator>().SetTrigger("Lose03");
                break;
            case 17:
                GetComponentInChildren<Animator>().SetTrigger("Hit01");
                break;
            case 18:
                GetComponentInChildren<Animator>().SetTrigger("Hit02");
                break;
            default:
                break;
        }
    }
}
