using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AssetSwitch : MonoBehaviour {
    
    public int m_switchOnEnemy;
    public List<GameObject> m_turnAssetOff;
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
            for (int i = 0; i < m_turnAssetOff.Count; i++)
            {
                m_turnAssetOff[i].SetActive(false);
            }
            m_turnAssetOn.SetActive(false);
            m_assetsSwitched = true;
        }
	}
}
