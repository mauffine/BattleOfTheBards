using UnityEngine;
using System.Collections;

public class OutlineWidth : MonoBehaviour {

    public float m_outlineWidth;
    private Material m_material;

	// Use this for initialization
	void Start () {
        var meshRenderer = transform.GetComponent<SkinnedMeshRenderer>();
        if (meshRenderer.materials.Length > 1)
        {
            m_material = meshRenderer.materials[1];
            m_material.SetFloat("_Outline", m_outlineWidth);
        }
        
	}
	
	// Update is called once per frame
	void Update () 
    {
        
	}
}
