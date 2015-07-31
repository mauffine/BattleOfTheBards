using UnityEngine;
using System.Collections;

public class Spell : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.Translate(.03f, 0, 0);
	}
}
