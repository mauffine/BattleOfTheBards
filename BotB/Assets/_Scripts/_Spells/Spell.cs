using UnityEngine;
using System.Collections;

public class Spell : MonoBehaviour 
{
    public uint m_damage;
    public Vector3 m_velocity;//The velocity

    float m_lifetime;//How long the spell particles lasts= for
    string m_key;//A key to show what key

	// Use this for initialization
	void Start() 
    {
        m_lifetime = 2.0f;
	}
	// Update is called once per frame
	void Update() 
    {
        transform.Translate(m_velocity);
       
	}
    /// <summary></summary>
    public string Key 
    {
        get {return m_key;}
    }
}
