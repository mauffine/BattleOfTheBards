using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class TextGen : MonoBehaviour 
{
    private List<GameObject> damageText;
    private float m_timer = 3;
    private static TextGen s_instance;
    [SerializeField]//A link to the GUIText prefab to generate the text
    private GameObject m_prefabLink;
	// Use this for initialization
	void Start() 
    {
        damageText = new List<GameObject>();
        s_instance = this;
	}
	
	// Update is called once per frame
	public void Update()
    {
        for (int I = 0; I < damageText.Count; ++I )
        {
            TextMesh currentText = damageText[I].GetComponent<TextMesh>();
            currentText.transform.position += new Vector3(0, Time.deltaTime, 0);
            Color textCol = currentText.color;
            currentText.color = Color.Lerp(textCol, Color.clear, Time.deltaTime * 9);
            if(textCol.a <= 0.075f)
            {
                Destroy(damageText[I]);
                damageText.RemoveAt(I);
            }
        }
	}
    ///<summary> Draws a damage number above the model</summary>
    ///<param name="a_damage">The ammount of damage to display</param>
    ///<param name="a_pos"> The charcters current position</param>
    ///<param name="a_length">The length of the character</param>
    public void TakeDamage(int a_damage, Vector3 a_pos, float a_length)
    {
        GameObject toConvert = Instantiate(m_prefabLink);
        TextMesh toWrite = toConvert.GetComponent<TextMesh>();
        toWrite.text = a_damage.ToString();
        Vector3 offset = new Vector3(0, a_pos.y + (a_length), 0);
        toWrite.transform.position = (a_pos + offset);
        toWrite.color = Color.red;
        damageText.Add(toConvert);
    }
    public void DisplayRating(int a_damage, Vector3 a_pos, float a_length)
    {
        GameObject toConvert = Instantiate(m_prefabLink);
        TextMesh toWrite = toConvert.GetComponent<TextMesh>();
        toWrite.text = a_damage.ToString();
        Vector3 offset = new Vector3(0, a_pos.y + (a_length), 0);
        toWrite.transform.position = (a_pos + offset);
        toWrite.color = Color.white;
        damageText.Add(toConvert);
    }
    public void YouWin()
    {
        GameObject toConvert = (GameObject)TextMesh.Instantiate(Resources.Load("_Prefabs/Enviroment/GUIText"), new Vector3(0, 0, 0), new Quaternion(0, 1, 0, 0));
        TextMesh toWrite = toConvert.GetComponent<TextMesh>();
        toWrite.text = "You Win!";
        toWrite.fontSize = 1000;
        toWrite.transform.localScale = new Vector3(0.015f,0.015f,0);
        toWrite.transform.localPosition = new Vector3(3.5f,1,0);
        toWrite.color = Color.white;
    }

    public static TextGen Instance
    {
        get { return s_instance; }
    }
}