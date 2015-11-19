using UnityEngine;
using System.Collections;

public class Queen : Musician
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
    }
    public override void Animate(short a_animationNum)
    {
        switch (a_animationNum)
        {
            case 1:
                GetComponentInChildren<Animator>().SetTrigger("PlayBite01");
                break;
            case 2:
                GetComponentInChildren<Animator>().SetTrigger("PlayBite02");
                break;
            case 3:
                GetComponentInChildren<Animator>().SetTrigger("PlayBite03");
                break;
            case 4:
                GetComponentInChildren<Animator>().SetTrigger("Fire");
                break;
            case 5:
                GetComponentInChildren<Animator>().SetTrigger("Fire");
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
    protected override void Die()
    {
    }
}
