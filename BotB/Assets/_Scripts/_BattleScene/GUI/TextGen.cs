using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class TextGen : MonoBehaviour 
{
    List<TextMesh> damageText;
    float m_timer = 3;
	// Use this for initialization
	void Start() 
    {
        damageText = new List<TextMesh>();
	}
	
	// Update is called once per frame
	public void Update() 
    {
        for (int I = 0; I < damageText.Count; ++I )
        {
            damageText[I].transform.position += new Vector3(0, Time.deltaTime, 0);
            Color textCol = damageText[I].color;
            damageText[I].color = Color.Lerp(textCol, Color.clear,Time.deltaTime * 9);
            if(textCol.a <= 0.05f)
                damageText.RemoveAt(I);
        }
	}

    public void TakeDamage(int a_damage, Vector3 a_pos, float a_ySize)
    {
        GameObject toConvert = (GameObject)TextMesh.Instantiate(Resources.Load("_Prefabs/Enviroment/GUIText"), new Vector3(0, 0, 0), new Quaternion(0, 1, 0, 0));
        TextMesh toWrite = toConvert.GetComponent<TextMesh>();
        toWrite.text = a_damage.ToString();
        Vector3 offset = new Vector3(0, a_pos.y + (a_ySize/ 2), 0);
        toWrite.transform.position = (a_pos + offset);
        toWrite.color = Color.red;
        damageText.Add(toWrite);
    }
}