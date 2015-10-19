using UnityEngine;
using System.Collections;

public class LifeBar : MonoBehaviour 
{
    [SerializeField]
    private Sprite m_sprite = null;
    float m_barLen = -1.0f;
    uint m_health;
	// Use this for initialization
	void Start(uint a_health) 
    {
        m_health = a_health;
        m_barLen = m_sprite.textureRect.width;
	}
	
	// Update is called once per frame
	void Update()
    {
        m_barLen -= (m_sprite.textureRect.width / m_health);
       // m_sprite.textureRect = new Rect(0,0,m_barLen,m_sprite.textureRect.height);
	}
}
