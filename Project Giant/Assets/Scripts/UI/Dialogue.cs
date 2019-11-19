using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public GameObject panel;
    private GameObject giant;
    private string[] text;
    private Text txt;

    private bool[] used;




    // Start is called before the first frame update
    void Start()
    {
        giant = GameObject.Find("Giant");
        text = new string[10];

        text[0] = "As another day starts at Snow-Peak village, the residents awake hoping that the Giant will grace them with good deeds and a helping hand.";
        text[1] = "Villagers give out stars when the Giant is nice to them. With enough stars, the Giant will grow.";
        text[2] = "Villagers give out tears when the Giant is mean to them. Collecting enough tears will allow the Giant to grow";
        text[3] = "It looks like someone wants to build something, however they can only build when nothing is in the way, perhaps the Giant can help?";
        text[4] = "Villagers don't like being trodden on.";
        text[5] = "When the Giant grows by collecting stars, it will become more mobile";
        text[6] = "When the Giant grows by collecting tears, it will gain more destructive abilities. ";
        text[7] = "The greater the Giant becomes in size, the stronger it gets, allowing it to lift almost anything!";
        text[8] = "Keeping nature on your side is always useful in a harsh enviroment";
        text[9] = "As the sun starts it's desent into the horizon, the villagers say goodbye to the Giant, as they have to rest, so too does the Giant.";

        txt = gameObject.GetComponent<Text>();
        txt.text = text[0];

        used = new bool[text.Length];
        for (int i = 0; i < used.Length; i++)
        {
            used[i] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (giant.GetComponent<PlayerControl>().stars > 0 && used[1] == false)
        {
            txt.text = text[1];
            used[1] = true;
        }

        if (giant.GetComponent<PlayerControl>().tears > 0 && used[2] == false)
        {
            txt.text = text[2];
            used[2] = true;
        }
    }
}
