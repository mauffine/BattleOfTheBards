using UnityEngine;
using System.Collections;
using FMOD;
using FMODUnity;

public class BankTrigger : MonoBehaviour 
{
    [SerializeField]
    System.Collections.Generic.Dictionary<string, EventRef> m_banks;
    private static BankTrigger m_instance;
	//Use this for initialization
	void Start() 
    {
        m_instance = this;
	}
	
    public bool PlayTrigger(string a_eventKey)
    {
        EventRef toRun;
        bool isValidRef = m_banks.TryGetValue(a_eventKey, out toRun);
        if (isValidRef)
            RuntimeManager.CreateInstance(toRun).start();

        return isValidRef;
    }
	//Update is called once per frame
	void Update() 
    {
	}

    public BankTrigger Instance
    {
        get { return m_instance; }
    }
}
