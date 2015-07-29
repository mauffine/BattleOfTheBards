using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpellSystem : MonoBehaviour {
    //Attributes
    Dictionary<string, Note[]> m_spellList = new Dictionary<string,Note[]>();


    //Behaviours
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    /// <summary> Receive's note from Battle.cs as a_note </summary>
    /// <param name="a_note"></param>
    void ReceiveNote (Note a_note)
    {

    }
}
