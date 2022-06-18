using Steamworks.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    private string[] altText;
    private bool alt;




    // Start is called before the first frame update
    void Start()
    {
        endPanel.SetActive(false);
        dayTimer = Time.time;
        giant = GameObject.Find("Giant");
        text = new string[28];
        altText = new string[28];

        //try
        //{
        //    Steamworks.SteamClient.Init(1903330);
        //}
        //catch (System.Exception)
        //{
        //    print("Steamworks error");
        //    throw;
        //}

        print(Steamworks.SteamClient.Name);

        text[0] = "As the sun emerges from the horizon, the residents awaken, hoping that the Giant will grace them with good deeds and a helping hand.";
        text[1] = "Villagers give out stars when the Giant is nice to them. With enough stars, the Giant will grow.";
        text[2] = "Villagers give out tears when the Giant is mean to them. Collecting enough tears will allow the Giant to grow";
        text[3] = "If someone wants to build something, they can only build when nothing is in the way, perhaps the Giant can help?";
        text[5] = "When the Giant grows by collecting stars, it will become more mobile";
        text[6] = "When the Giant grows by collecting tears, it will gain more destructive abilities. ";
        text[7] = "The greater the Giant becomes in size, the stronger it gets, allowing it to lift almost anything!";
        text[8] = "The trees on the island will slowly wither and die if left alone, almost as if they long for companionship. Maybe you could give the trees a friend?";
        text[9] = "As the sun starts it's desent into the endless ocean, the villagers say goodbye to the Giant, as they have to rest, and so too does the Giant.";
        text[12] = "All life on the island can be helped or hindered by the Giant, from the birds in the sky to the fish in the sea. You wouldn't notice though, as they are unable to communicate with The Giant";
        text[13] = "Keeping nature on your side is always useful, if the islands delicate balance is disrupted, the island will grow more dangerous.";
        //Achievment Text
        text[4] = "Villagers don't like being trodden on.";
        text[10] = "The Giant has grown by collecting tears from the villagers, the giant can now attack larger structures";
        text[11] = "The Giant has grown by collecting stars from the villagers, and can now move at greater speeds";
        text[14] = "The villager is harvesting the crops now that it has finished growing. They will then ready the land to grow new crops in its' place.";
        text[15] = "Even on rainy days like this, the villagers lives must go on, and the same goes for the Giant. Will the Giant be the silver lining of the cloudy skies?";
        text[16] = "When trees are placed together, they wither and leave in their place seeds which will grow into trees when a new sun rises. Wilted trees left standing at sunset will be left to the mercy of the moon.";
        text[17] = "Oh dear. I guess the waters surface is the only thing protecting the sealife from the hunters in the sky, i'm sure there's a healthy balance to be found.";
        text[18] = "A villager has gone hunting, it would be wrong to not make the most out of the livestocks sacrifice no?";
        text[19] = "Now there is a clear area in which to build, the villager gives praise to the Giant and gets to work.";
        text[20] = "The villager praises the Giant for granting a tree to help grow the village.";
        text[21] = "The villager praises the Giant for bringing them stone, the foundation for all structures on the island.";
        text[22] = "The villagers have built a new house on the island, and new friends have joined them. The village grows.";
        text[23] = "oops, the Giant can appear gentle but sometimes a stumple can destroy fragile creations of the villagers. Farms are especially vunerable to being trodden on";
        text[24] = "If the Giant takes their shelter, their privacy and their comfort space, it will take their spirit as well.";
        text[25] = "Oh no, guess the Giant can't kick around the villagers forever.";
        text[26] = "Look out, a tornado is forming, the Giant can't stop it but it can move things away from the tornados path, or maybe it would like to use the tornado to it's full destructive potential.";
        text[27] = "Welcome to challenge mode, make the Giants presence known as much as possible in just one day.";

        //AltText
        altText[0] = "The sun rises over the island as a new day dawns, will The Giant grant the villagers their wishes or bring forth their worst fears.";
        altText[1] = "A villager has gifted the giant a star as thanks for a good deed.";
        altText[2] = "A villager has shed a tear over The Giants actions.";
        altText[3] = "A villager wants to build a new structure, however something appears to be in the way.";
        altText[5] = "When The Giant grows through stars granted by the villagers, it becomes easier for The Giant to help them further";
        altText[6] = "The tears of the villagers only make it easier for The Giant to worsen their misery";
        altText[7] = "No matter how The Giant grows, the ability to lift objects will always be improved.";
        altText[8] = "There's always a way for The Giant to influence the island. Consult the encyclopedia if it ever seems like The Giant is waiting for fate rather than creating it.";
        altText[9] = "And so another page in the tale of this island comes to a close. What lessons are to be learnt from today? What fate awaits the island tomorrow?";
        altText[12] = "Sometimes I wonder if The Giant has a plan for the island on a day-to-day basis, or are The Giants actions purely reactionary to the circumstances of which The Giant finds itself?";
        altText[13] = "Many things that are good for the inhabitants of the island are harmful to nature and vice versa. It's a balancing act The Giant must perform.";
        //Achievment Text
        altText[4] = "I don't think the villagers liked being trampled.";
        altText[10] = "The Giant has caused enough tears to be shed to grow, and can now demolish bigger structures.";
        altText[11] = "The villagers have blessed The Giant with enough stars to grow larger, the energy also allows The Giant to move at greater speeds";
        altText[14] = "The crops are fully grown and so the villager begins the harvest. The farmer praises The Giant with a star.";
        altText[15] = "As a new day dawns the sun tries desperatly to peirce through the clouds, The Giant and villagers rise from their slumber.";
        altText[16] = "As the trees wither and die four new seeds take their place. These seeds will grow to be as mighty as their parents soon enough.";
        altText[17] = "As The Giant helps in the birds hunt, their population will surely increase.";
        altText[18] = "The villager gives blessing to the giant for the animal it has hunted, perhaps they believe the Giant to be the god that created livestock?";
        altText[19] = "The villager gives thanks to The Giant for clearing the way by granting a star.";
        altText[20] = "As thanks for giving a tree, the villager gives The Giant a star.";
        altText[21] = "A stone for a star, a fair trade.";
        altText[22] = "Another house has successfully been constructed, and the population of the tribe has increased.";
        altText[23] = "A farm has been destroyed. The villagers shed a tear over their lost work.";
        altText[24] = "The villagers who lived in that house shed a tear as they beg for shelter from their neihbours";
        altText[25] = "I guess that villager was *booted* from the island, haha.";
        altText[26] = "A tornado has just been spotted off the shore, and is about to touchdown on the island.";
        altText[27] = "In challenge mode there are no days on the island after today, so make the most of it!";



        //Set to start of day text.
        int random = Random.Range(0, 2);
        if (random == 0)
            alt = true;
        else
            alt = false;

        txt = gameObject.GetComponent<Text>();

        if(alt == true)
            txt.text = altText[0];
        else
            txt.text = text[0];

        txt.color = new UnityEngine.Color(1, 1, 1, 1);
        timer = Time.time;
        panel.SetActive(true);
        alt = !alt;

        used = new bool[text.Length];
        for (int i = 0; i < used.Length; i++) //All texts can be used (once said once they can't be repeated)
        {
            used[i] = false;
        }

        if (Application.loadedLevel == 3 || Application.loadedLevel == 4)
        {
            Challenge();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - timer > 10f) //If no new text has appeared in 10secs, hide the text box.
        {
            txt.text = "";
            panel.SetActive(false);
            alt = !alt;
        }
        if (giant.GetComponent<PlayerControl>().stars > 0 && used[1] == false) //When the player has collected their first star
        {
            if (alt == true)
                txt.text = altText[1];
            else
                txt.text = text[1];
            used[1] = true;
            panel.SetActive(true);
            timer = Time.time;
        }

        if (giant.GetComponent<PlayerControl>().tears > 0 && used[2] == false) //When the player has collected their first star
        {
            if (alt == true)
                txt.text = altText[2];
            else
                txt.text = text[2];
            used[2] = true;
            panel.SetActive(true);
            timer = Time.time;
        }


        if (giant.GetComponent<PlayerControl>().stars > 9 && used[11] == false) //When the player has grown using stars
        {
            if (alt == true)
                txt.text = altText[11];
            else
                txt.text = text[11];
            used[11] = true;
            panel.SetActive(true);
            timer = Time.time;
            if (SceneManager.GetActiveScene().name != "TornadoChallenge1")
            {
                var ach = new Achievement("star");
                ach.Trigger();
                Steamworks.SteamClient.RunCallbacks();
            }
        }

        if (giant.GetComponent<PlayerControl>().tears > 9 && used[10] == false) //When the player has grown using tears
        {
            if (alt == true)
                txt.text = altText[10];
            else
                txt.text = text[10];
            used[10] = true;
            panel.SetActive(true);
            timer = Time.time;
            var ach = new Achievement("tear");
            ach.Trigger();
            Steamworks.SteamClient.RunCallbacks();
        }
        //When the player has collected either 11 stars or tears.
        if ((giant.GetComponent<PlayerControl>().stars > 11|| giant.GetComponent<PlayerControl>().tears > 11) && used[7] == false) 
        {
            if (alt == true)
                txt.text = altText[7];
            else
                txt.text = text[7];
            used[7] = true;
            panel.SetActive(true);
            timer = Time.time;
        }

        if (Time.time - timer >= 20f && used[8] == false) //When 20secs has passed without any narration
        {
            if (alt == true)
                txt.text = altText[8];
            else
                txt.text = text[8];
            used[8] = true;
            panel.SetActive(true);
            timer = Time.time;
        }

        if (Time.time - dayTimer >= 574f) //When the day is about to end.
        {
            musicControl.GetComponent<MusicControl>().endOfDayAlert();
            if (alt == true)
                txt.text = altText[9];
            else
                txt.text = text[9];
            used[9] = true;
            panel.SetActive(true);
            timer = Time.time;
        }

        if (giant.GetComponent<PlayerControl>().tears > 19 && used[6] == false) //When the player grew for a secound time using tears
        {
            if (alt == true)
                txt.text = altText[6];
            else
                txt.text = text[6];
            used[6] = true;
            panel.SetActive(true);
            timer = Time.time;
        }

        if (giant.GetComponent<PlayerControl>().stars > 19 && used[5] == false) //When the player grew for a secound time using stars
        {
            if (alt == true)
                txt.text = altText[5];
            else
                txt.text = text[5];
            used[5] = true;
            panel.SetActive(true);
            timer = Time.time;
        }

        if (Time.time - timer >= 40f && used[12] == false) //When 40secs has passed without any narration
        {
            if (alt == true)
                txt.text = altText[12];
            else
                txt.text = text[12];
            used[12] = true;
            panel.SetActive(true);
            timer = Time.time;
        }

        if (Time.time - timer >= 60f && used[13] == false) //When 60secs has passed without any narration
        {
            if (alt == true)
                txt.text = altText[13];
            else
                txt.text = text[13];
            used[13] = true;
            panel.SetActive(true);
            timer = Time.time;
        }

        //if (Time.time - timer >= 45f && used[14] == false) //When 45secs has passed without any narration
        //{
        //    txt.text = text[14];
        //    used[14] = true;
        //    panel.SetActive(true);
        //    timer = Time.time;
        //}

        if (Time.time - dayTimer >= 600f) //When the day has ended
        {
            villagerScore = giant.GetComponent<PlayerControl>().stars - giant.GetComponent<PlayerControl>().tears;
            txt.text = text[9];
            used[9] = true;
            endPanel.SetActive(true);
            endPanel.GetComponent<EndStats>().SetStats(villagerScore, natureScore);
            string endMessage = "";

            if (villagerScore > 9)
            {
                endMessage = "The villagers were very happy with how the Giant helped them make the island a better place, and are hoping to be blessed by the Gaint again tomorrow.";
            }
            else if (villagerScore > 4)
            {
                endMessage = "The villagers were happy with how they were treated by the Giant during the day.";
            }
            else if (villagerScore >= 0)
            {
                endMessage = "The villagers wished that the Giant would help and care for them more.";
            }
            else if (villagerScore > -5)
            {
                endMessage = "The villagers hope that the Giant will be kinder to them in the future.";
            }
            else if (villagerScore > -10)
            {
                endMessage = "The villagers wish that the Gaint wasn't so mean.";
            }
            else
            {
                endMessage = "The villagers pray into the night, hoping the Giant will not return tomorrow.";
            }

            if (natureScore > 9)
            {
                endMessage += " Nature on the island has flourished thanks to the helping hand of the Giant.";
            }
            else if (natureScore > 4)
            {
                endMessage += " Nature on the island is in a healthy state thanks to the Giant.";
            }
            else if (natureScore >= 0)
            {
                endMessage += " The Giant has neglected the nature of the island.";
            }
            else if (natureScore > -5)
            {
                endMessage += " The Giant seems unmoved by the degregation of nature on the island.";
            }
            else if (natureScore > -10)
            {
                endMessage += " The Giant is causing unsustanable harm to the islands nature.";
            }
            else
            {
                endMessage += " The Giant is activly causing the seemingly inevitable extinction of nature on the island.";
            }

            txt.text = endMessage;
            used[13] = true;
            panel.SetActive(true);
            timer = Time.time + 40f;
        }
    }

    public void BuildHelp() //When a Villager has been trying to build in a taken up space for 5secs
    {
        if (used[3] == false)
        {
            if (alt == true)
                txt.text = altText[3];
            else
                txt.text = text[3];
            used[3] = true;
            panel.SetActive(true);
            timer = Time.time;
        }
    }

    public void VillagerKicked()
    {
        if (used[4] == false)
        {
            if (alt == true)
                txt.text = altText[4];
            else
                txt.text = text[4];
            used[4] = true;
            panel.SetActive(true);
            timer = Time.time;
            var ach = new Achievement("tread");
            ach.Trigger();
            Steamworks.SteamClient.RunCallbacks();
        }
    }

    public void Harvest()
    {
        if (used[14] == false)
        {
            if (alt == true)
                txt.text = altText[14];
            else
                txt.text = text[14];
            used[14] = true;
            panel.SetActive(true);
            timer = Time.time;
            var ach = new Achievement("farmer");
            ach.Trigger();
            Steamworks.SteamClient.RunCallbacks();
        }
    }

    public void Rain()
    {
        if (used[15] == false)
        {
            print("Rain Recieved from Dialogue");
            if (alt == true)
                txt.text = altText[15];
            else
                txt.text = text[15];
            used[15] = true;
            panel.SetActive(true);
            timer = Time.time;
            var ach = new Achievement("rainy");
            ach.Trigger();
            Steamworks.SteamClient.RunCallbacks();
        }
    }

    public void TreePlanted()
    {
        if (used[16] == false)
        {
            if (alt == true)
                txt.text = altText[16];
            else
                txt.text = text[16];
            used[16] = true;
            panel.SetActive(true);
            timer = Time.time;
            var ach = new Achievement("gardener");
            ach.Trigger();
            Steamworks.SteamClient.RunCallbacks();
        }
    }

    public void FishTaken()
    {
        if (used[17] == false)
        {
            if (alt == true)
                txt.text = altText[17];
            else
                txt.text = text[17];
            used[17] = true;
            panel.SetActive(true);
            timer = Time.time;
            var ach = new Achievement("fish");
            ach.Trigger();
            Steamworks.SteamClient.RunCallbacks();
        }
    }

    public void Hunting()
    {
        if (used[18] == false)
        {
            if (alt == true)
                txt.text = altText[18];
            else
                txt.text = text[18];
            used[18] = true;
            panel.SetActive(true);
            timer = Time.time;
            var ach = new Achievement("dinner");
            ach.Trigger();
            Steamworks.SteamClient.RunCallbacks();
        }
    }

    public void ClearedArea()
    {
        if (used[19] == false)
        {
            if (alt == true)
                txt.text = altText[19];
            else
                txt.text = text[19];
            used[19] = true;
            panel.SetActive(true);
            timer = Time.time;
            var ach = new Achievement("space");
            ach.Trigger();
            Steamworks.SteamClient.RunCallbacks();
        }
    }

    public void GiveTree()
    {
        if (used[20] == false)
        {
            if (alt == true)
                txt.text = altText[20];
            else
                txt.text = text[20];
            used[20] = true;
            panel.SetActive(true);
            timer = Time.time;
            var ach = new Achievement("wood");
            ach.Trigger();
            Steamworks.SteamClient.RunCallbacks();
        }
    }

    public void GiveStone()
    {
        if (used[21] == false)
        {
            if (alt == true)
                txt.text = altText[21];
            else
                txt.text = text[21];
            used[21] = true;
            panel.SetActive(true);
            timer = Time.time;
            var ach = new Achievement("stone");
            ach.Trigger();
            Steamworks.SteamClient.RunCallbacks();
        }
    }

    public void HouseBuilt()
    {
        if (used[22] == false)
        {
            if (alt == true)
                txt.text = altText[22];
            else
                txt.text = text[22];
            used[22] = true;
            panel.SetActive(true);
            timer = Time.time;
            var ach = new Achievement("house");
            ach.Trigger();
            Steamworks.SteamClient.RunCallbacks();
        }
    }

    public void FarmDestroyed()
    {
        if (used[23] == false)
        {
            if (alt == true)
                txt.text = altText[23];
            else
                txt.text = text[23];
            used[23] = true;
            panel.SetActive(true);
            timer = Time.time;
            var ach = new Achievement("diet");
            ach.Trigger();
            Steamworks.SteamClient.RunCallbacks();
        }
    }

    public void HouseDestroyed()
    {
        if (used[24] == false)
        {
            if (alt == true)
                txt.text = altText[24];
            else
                txt.text = text[24];
            used[24] = true;
            panel.SetActive(true);
            timer = Time.time;
            var ach = new Achievement("eviction");
            ach.Trigger();
            Steamworks.SteamClient.RunCallbacks();
        }
    }

    public void VillagerKickedDead()
    {
        if (used[25] == false)
        {
            if (alt == true)
                txt.text = altText[25];
            else
                txt.text = text[25];
            used[25] = true;
            panel.SetActive(true);
            timer = Time.time;
            var ach = new Achievement("boot");
            ach.Trigger();
            Steamworks.SteamClient.RunCallbacks();
        }
    }

    public void TornadoFormed()
    {
        if (used[26] == false)
        {
            if (alt == true)
                txt.text = altText[26];
            else
                txt.text = text[26];
            used[26] = true;
            panel.SetActive(true);
            timer = Time.time;
            var ach = new Achievement("disaster");
            ach.Trigger();
            Steamworks.SteamClient.RunCallbacks();
        }
    }

    public void Challenge()
    {
        if (used[27] == false)
        {
            if (alt == true)
                txt.text = altText[27];
            else
                txt.text = text[27];
            used[27] = true;
            panel.SetActive(true);
            timer = Time.time;
            var ach = new Achievement("challenge");
            ach.Trigger();
            Steamworks.SteamClient.RunCallbacks();
        }
    }
}
