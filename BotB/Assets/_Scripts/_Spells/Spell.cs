using UnityEngine;
using System.Collections;

public class Spell : MonoBehaviour 
{
    public float m_speedModifier;//modifier for deltaTime
    public Vector3 m_velocity;//The velocity
    protected uint m_damage; //Damage that the spell deals
    protected string m_key;//A key to show what key
	// Use this for initialization
	protected void Start() 
    {
        m_speedModifier = 80;
        m_damage = 20;
	}
	// Update is called once per frame
	protected void Update() 
    {
        transform.Translate(m_velocity * Time.deltaTime * m_speedModifier);
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
