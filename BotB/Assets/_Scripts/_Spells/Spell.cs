using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spell : MonoBehaviour 
{
    public float m_speedModifier;//modifier for deltaTime
    public Vector3 m_velocity;//The velocity

    protected string m_key;//A key to show what key
    protected int m_damage;
    protected SpellType m_type;

    private ParticleSystem m_localParticleSystem; //for this object, possibly temporary
    private List<ParticleSystem> m_particleSystemList; //for children

	// Use this for initialization
	protected void Start() 
    {
        m_speedModifier = 80;
        m_damage = 20;
        m_type = SpellType.Offencive;

        m_localParticleSystem = GetComponent<ParticleSystem>();

        //m_particleSystemList = new ParticleSystem[8];
        m_particleSystemList = new List<ParticleSystem>();
        foreach(Transform child in transform)
        {
            m_particleSystemList.Add(child.GetComponent<ParticleSystem>());
        }
	}
	// Update is called once per frame
	protected void Update() 
    {
        transform.Translate(m_velocity * Time.deltaTime * m_speedModifier);
	}

    public void TurnOffEmission()
    {
        if(m_localParticleSystem != null)
        {
            m_localParticleSystem.enableEmission = false;
        }

        foreach(ParticleSystem system in m_particleSystemList)
        {
            system.enableEmission = false;
        }
    }

    public int Damage
    {
        get { return m_damage; }
    }
    public virtual Note[] Key
    {
        get { return null; }
    }
    public virtual string Name
    {
        get { return null; }
    }
    public virtual SpellType Type
    {
        get { return m_type; }
    }
}
