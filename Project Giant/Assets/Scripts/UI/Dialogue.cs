using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    //This script is used to control the narrator which gives the player tips and hints.
    public GameObject panel;
    private GameObject giant;
    public GameObject musicControl;
    public GameObject endPanel;
    private string[] text;
    private Text txt;
    private float timer;
    private float dayTimer;
    public int natureScore;
    private int villagerScore;

    private bool[] used;




    // Start is called before the first frame update
    void Start()
    {
        endPanel.SetActive(false);
        dayTimer = Time.time;
        giant = GameObject.Find("Giant");
        text = new string[15];

        text[0] = "As another day starts at Snow-Peak village, the residents awake hoping that the Giant will grace them with good deeds and a helping hand.";
        text[1] = "Villagers give out stars when the Giant is nice to them. With enough stars, the Giant will grow.";
        text[2] = "Villagers give out tears when the Giant is mean to them. Collecting enough tears will allow the Giant to grow";
        text[3] = "If someone wants to build something, they can only build when nothing is in the way, perhaps the Giant can help?";
        text[4] = "Villagers don't like being trodden on.";
        text[5] = "When the Giant grows by collecting stars, it will become more mobile";
        text[6] = "When the Giant grows by collecting tears, it will gain more destructive abilities. ";
        text[7] = "The greater the Giant becomes in size, the stronger it gets, allowing it to lift almost anything!";
        text[8] = "Keeping nature on your side is always useful in a harsh enviroment, try putting 2 trees next to eachother.";
        text[9] = "As the sun starts it's desent into the horizon, the villagers say goodbye to the Giant, as they have to rest, so too does the Giant.";
        text[10] = "The Giant has grown by collecting tears from the villagers, now it can attack by pressing the (X) key!";
        text[11] = "The Giant has grown by collecting stars from the villagers, the Giant's speed has increased!";
        text[12] = "Sometimes it's nessasary to be harmfull in order to help.";
        text[13] = "In such a vast world, we get so fixated on what's nearby, we sometimes forget to explore what's further afield. I Wonder if there is anything over those mountains?";
        text[14] = "When all that exists is happiness, we all may as-well be constantly sad.";

        //Set to start of day text.
        txt = gameObject.GetComponent<Text>();
        txt.text = text[0];
        txt.color = new Color(1, 1, 1, 1);
        timer = Time.time;
        panel.SetActive(true);

        used = new bool[text.Length];
        for (int i = 0; i < used.Length; i++) //All texts can be used (once said once they can't be repeated)
        {
            used[i] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - timer > 10f) //If no new text has appeared in 10secs, hide the text box.
        {
            txt.text = "";
            panel.SetActive(false);
        }
        if (giant.GetComponent<PlayerControl>().stars > 0 && used[1] == false) //When the player has collected their first star
        {
            txt.text = text[1];
            used[1] = true;
            panel.SetActive(true);
            timer = Time.time;
        }

        if (giant.GetComponent<PlayerControl>().tears > 0 && used[2] == false) //When the player has collected their first star
        {
            txt.text = text[2];
            used[2] = true;
            panel.SetActive(true);
            timer = Time.time;
        }


        if (giant.GetComponent<PlayerControl>().tears > 1 && used[4] == false) //When the player has collected a secound tear
        {
            txt.text = text[4];
            used[4] = true;
            panel.SetActive(true);
            timer = Time.time;
        }

        if (giant.GetComponent<PlayerControl>().stars > 4 && used[5] == false) //When the player has grown using stars
        {
            txt.text = text[5];
            used[5] = true;
            panel.SetActive(true);
            timer = Time.time;
        }

        if (giant.GetComponent<PlayerControl>().tears > 4 && used[6] == false) //When the player has grown using tears
        {
            txt.text = text[6];
            used[6] = true;
            panel.SetActive(true);
            timer = Time.time;
        }
        //When the player has collected either 7 stars or tears.
        if ((giant.GetComponent<PlayerControl>().stars > 6|| giant.GetComponent<PlayerControl>().tears > 6) && used[7] == false) 
        {
            txt.text = text[7];
            used[7] = true;
            panel.SetActive(true);
            timer = Time.time;
        }

        if (Time.time - timer >= 13f && used[8] == false) //When 13secs has passed without any narration
        {
            txt.text = text[8];
            used[8] = true;
            panel.SetActive(true);
            timer = Time.time;
        }

        if (Time.time - dayTimer >= 274f) //When the day is about to end.
        {
            musicControl.GetComponent<MusicControl>().endOfDayAlert();
            txt.text = text[9];
            used[9] = true;
            panel.SetActive(true);
            timer = Time.time;
        }

        if (giant.GetComponent<PlayerControl>().tears > 9 && used[10] == false) //When the player grew for a secound time using tears
        {
            txt.text = text[10];
            used[10] = true;
            panel.SetActive(true);
            timer = Time.time;
        }

        if (giant.GetComponent<PlayerControl>().stars > 9 && used[11] == false) //When the player grew for a secound time using stars
        {
            txt.text = text[11];
            used[11] = true;
            panel.SetActive(true);
            timer = Time.time;
        }

        if (Time.time - timer >= 20f && used[12] == false) //When 20secs has passed without any narration
        {
            txt.text = text[12];
            used[12] = true;
            panel.SetActive(true);
            timer = Time.time;
        }

        if (Time.time - timer >= 25f && used[13] == false) //When 25secs has passed without any narration
        {
            txt.text = text[13];
            used[13] = true;
            panel.SetActive(true);
            timer = Time.time;
        }

        if (Time.time - timer >= 45f && used[14] == false) //When 45secs has passed without any narration
        {
            txt.text = text[14];
            used[14] = true;
            panel.SetActive(true);
            timer = Time.time;
        }

        if (Time.time - dayTimer >= 300f) //When the day has ended
        {
            villagerScore = giant.GetComponent<PlayerControl>().stars - giant.GetComponent<PlayerControl>().tears;
            txt.text = text[9];
            used[9] = true;
            endPanel.SetActive(true);
            endPanel.GetComponent<EndStats>().SetStats(villagerScore, natureScore);
            gameObject.SetActive(false);
        }
    }

    private void BuildHelp() //When a Villager has been trying to build in a taken up space for 5secs
    {
        if (used[3] == false)
        {
            txt.text = text[3];
            used[3] = true;
            panel.SetActive(true);
            timer = Time.time;
        }
    }
}
