using UnityEngine;
using System.Collections;

public class MusicianBase : MonoBehaviour
{
    [SerializeField]
    uint m_health;
    [SerializeField]
    string m_name;
    [SerializeField]
    Spell[] m_spellList;//Cap the spell list for now to make it easier to work with
    [SerializeField]
    uint m_spellNum;
	// Use this for initialization
	void Start() 
    {
	
	}
	// Update is called once per frame
	void Update() 
    {
	
	}
    //
    void PlaySpell(string a_spellKey)
    {
        for (uint I = 0; I < m_spellNum; ++I)
        {
            if(m_spellList[I].Key == a_spellKey)
            {

            }
        }
    }
}