using UnityEngine;
using System.Collections;

public class Spell : MonoBehaviour 
{
    public Vector3 m_velocity;//The velocity
    protected uint m_damage; //Damage that the spell deals
    protected string m_key;//A key to show what key
	// Use this for initialization
	void Start() 
    {
        m_damage = 20;
	}
	// Update is called once per frame
	void Update() 
    {
        this.transform.Translate(m_velocity);
	}
    public uint Damage
    {
        get { return m_damage; }
    }
    public virtual string Key
    {
        get { return m_key; }
    }
}
