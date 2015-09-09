using UnityEngine;
using System.Collections;

public class NoteSpell : Spell
{
    float m_lifeTime = 1.2f;
    // Use this for initialization
    void Start()
    {
        base.Start();
        m_damage = 20;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_lifeTime < 0)
            Destroy(gameObject);
        else
            m_lifeTime -= Time.deltaTime;
        base.Update();
    }
    public override string Name
    {
        get { return "NoteSpell"; }
    }
}