using UnityEngine;
using System.Collections;

public class Dwarf : Musician
{
    // Use this for initialization
    void Start()
    {
        base.Start();
    }
    // Update is called once per frame
    void Update()
    {
        base.Update();
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
                GetComponentInChildren<Animator>().SetTrigger("Play1");
                break;
            case 2:
                GetComponentInChildren<Animator>().SetTrigger("Play2");
                break;
            case 3:
                GetComponentInChildren<Animator>().SetTrigger("Play2");
                break;
            case 4:
                GetComponentInChildren<Animator>().SetTrigger("Spell");
                break;
            case 5:
                GetComponentInChildren<Animator>().SetTrigger("Spell");
                break;
            case 6:
                GetComponentInChildren<Animator>().SetTrigger("Hurt");
                break;
            case 7:
                GetComponentInChildren<Animator>().SetTrigger("Lose");
                break;
            case 8:
                GetComponentInChildren<Animator>().SetTrigger("Win");
                break;
            default:
                break;
        }
    }
}
