using UnityEngine;
using System.Collections;

public class AssetSwitch : MonoBehaviour {
    
    public int m_switchOnEnemy;
    public GameObject m_turnAssetOff;
    public GameObject m_turnAssetOn;
    bool m_assetsSwitched;

	// Use this for initialization
	void Start () {
        m_assetsSwitched = false;
	}
	
	// Update is called once per frame
	void Update () {
	    
        if(!m_assetsSwitched && GetComponent<Battle>().m_enemyListIndex == m_switchOnEnemy)
        {
            m_turnAssetOff.SetActive(false);
            m_turnAssetOn.SetActive(true);
            m_assetsSwitched = true;
        }
	}
}
