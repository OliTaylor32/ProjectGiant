using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class MainMenu : MonoBehaviour
{
    private bool cooldown;
    public TextMeshProUGUI[] modes;

    public TextMeshProUGUI[] freeplayModes;
    public GameObject freeplayLocation;
    public string[] freeplayMaps;
    public Sprite[] freeplayMapImages;

    public TextMeshProUGUI[] challenges;

    public TextMeshProUGUI[] encyclopediaEntries;
    public TextMeshProUGUI encyclopediaTxt;
    private string[] articles;
    public GameObject ePanel;

    public TextMeshProUGUI[] settings;

    public GameObject freeplayMapSS;
    public GameObject[] freeplayIcons;
    public Image background;

    public Sprite[] backgroundSprites;
    public int selected;
    public int selected2;
    public int menuLayer;
    public int mapSelected;

    public Fade fade;
    public GameObject logo;

    private InputManager inputManager;
    private bool rebinding;
    public TextMeshProUGUI instructionTxt;
    // Start is called before the first frame update
    void Start()
    {
        rebinding = false;
        inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
        Cursor.visible = false;

        if (Steamworks.SteamClient.AppId != 1903330)
        {
            try
            {
                Steamworks.SteamClient.Init(1903330);
            }
            catch (System.Exception)
            {
                print("Steamworks error");
                throw;
            }
        }

        print(Steamworks.SteamClient.Name);

        //Top Menu
        selected = 0;
        selected2 = 0;
        mapSelected = 0;
        for (int i = 0; i < modes.Length; i++)
        {
            modes[i].color = new Color (0.5f, 0.5f, 0.5f, 1f);
        }
        modes[0].color = new Color(1f, 1f, 1f, 1f);

        //FreePlay Menu
        freeplayModes[0].color = new Color(1f, 1f, 1f, 1f);
        freeplayModes[1].color = new Color(0.5f, 0.5f, 0.5f, 1f);

        for (int i = 0; i < freeplayModes.Length; i++)
        {
            freeplayModes[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < freeplayIcons.Length; i++)
        {
            freeplayIcons[i].gameObject.SetActive(false);
        }

        freeplayLocation.SetActive(false);
        freeplayMapSS.SetActive(false);

        //Challenges Menu
        for (int i = 0; i < challenges.Length; i++)
        {
            challenges[i].gameObject.SetActive(false);
        }

        //Encyclopedia Menu
        for (int i = 0; i < encyclopediaEntries.Length; i++)
        {
            encyclopediaEntries[i].gameObject.SetActive(false);
        }
        encyclopediaTxt.gameObject.SetActive(false);
        ePanel.SetActive(false);

        //Settings Menu
        for (int i = 0; i < settings.Length; i++)
        {
            settings[i].gameObject.SetActive(false);
        }
        instructionTxt.gameObject.SetActive(false);

        if (inputManager.invertY == true)
        {
            settings[2].text = "Invert Y axis: On";
        }
        else
        {
            settings[2].text = "Invert Y axis: Off";
        }

        SetEncyclopediaText();
    }

    // Update is called once per frame
    void Update()
    {
        if (rebinding == false)
        {
            if (Input.GetAxis("Horizontal") > 0.1f && cooldown == false && mapSelected < 0)
            {
                mapSelected++;
                StartCoroutine(TriggerCoolDown());
            }

            if (Input.GetAxis("Horizontal") < -0.1f && cooldown == false && mapSelected != 0)
            {
                mapSelected--;
                StartCoroutine(TriggerCoolDown());
            }

            if (Input.GetAxis("Vertical") > 0.1f && cooldown == false)
            {
                if (menuLayer == 0)
                {
                    if (selected > 0)
                    {
                        selected--;
                    }
                }
                else if (selected2 > 0)
                {
                    selected2--;
                }
                StartCoroutine(TriggerCoolDown());
            }

            if (Input.GetAxis("Vertical") < -0.1f && cooldown == false)
            {
                if (menuLayer == 0)
                {
                    if (selected < modes.Length - 1)
                    {
                        selected++;
                    }
                }
                else if (selected2 < 1 || (selected == 3 && selected2 < settings.Length - 1) || (selected == 2 && selected2 < encyclopediaEntries.Length - 1))
                {
                    selected2++;
                }
                StartCoroutine(TriggerCoolDown());
            }

            if (Input.GetButtonDown("Confirm"))
            {
                if (menuLayer == 0)
                {
                    if (selected == 4)
                    {
                        Steamworks.SteamClient.Shutdown();
                        Application.Quit();
                    }
                    SubMenu();
                }
                else
                {
                    if (selected == 0)
                    {
                        if (selected2 == 0)//New FreePlay
                        {
                            if (mapSelected == 0)
                            {
                                StartCoroutine(LoadLevel("Bideford"));
                            }
                            if (mapSelected == 1)
                            {
                                StartCoroutine(LoadLevel("Shebbear"));
                            }
                        }
                        if (selected2 == 1)
                        {
                            if (mapSelected == 0)
                            {
                                StartCoroutine(LoadLevel("BidefordLoad"));
                            }
                            if (mapSelected == 1)
                            {
                                StartCoroutine(LoadLevel("ShebbearLoad"));
                            }
                        }

                    }

                    if (selected == 1)
                    {
                        if (selected2 == 0)
                        {
                            StartCoroutine(LoadLevel("TornadoChallenge1"));
                        }
                        else
                        {
                            StartCoroutine(LoadLevel("PresentChallenge1"));
                        }
                    }

                    if (selected == 3)
                    {
                        if (selected2 == 0)
                        {
                            Screen.fullScreen = !Screen.fullScreen;
                        }
                        else if (selected2 == 1)
                        {
                            //Start Rebinding Controls process
                            fade.HalfFade();
                            rebinding = true;
                            StartCoroutine(RebindKeyboard());
                        }
                        else
                        {
                            inputManager.invertY = !inputManager.invertY;
                            if (inputManager.invertY == true)
                            {
                                settings[2].text = "Invert Y axis: On";
                            }
                            else
                            {
                                settings[2].text = "Invert Y axis: Off";
                            }
                            inputManager.SaveBindings();
                        }
                    }
                }
            }

            if (Input.GetButtonDown("Back"))
            {
                TopMenu();
            }
        }
    }

    private IEnumerator TriggerCoolDown()
    {
        cooldown = true;
        for (int i = 0; i < modes.Length; i++)
        {
            modes[i].color = new Color(0.5f, 0.5f, 0.5f, 1f);
            if (i == selected)
            {
                modes[i].color = new Color(1f, 1f, 1f, 1f);
            }
        }

        for (int i = 0; i < freeplayModes.Length; i++)
        {
            freeplayModes[i].color = new Color(0.5f, 0.5f, 0.5f, 1f);
            if (i == selected2)
            {
                freeplayModes[i].color = new Color(1f, 1f, 1f, 1f);
            }
        }

        freeplayLocation.GetComponent<TextMeshProUGUI>().text = freeplayMaps[mapSelected];
        freeplayMapSS.GetComponent<Image>().sprite = freeplayMapImages[mapSelected];

        for (int i = 0; i < challenges.Length; i++)
        {
            challenges[i].color = new Color(0.5f, 0.5f, 0.5f, 1f);
            if (i == selected2)
            {
                challenges[i].color = new Color(1f, 1f, 1f, 1f);
            }
        }

        for (int i = 0; i < encyclopediaEntries.Length; i++)
        {
            encyclopediaEntries[i].color = new Color(0.5f, 0.5f, 0.5f, 1f);
            if (i == selected2)
            {
                encyclopediaEntries[i].color = new Color(1f, 1f, 1f, 1f);
            }
            encyclopediaTxt.text = articles[selected2];
        }

        for (int i = 0; i < settings.Length; i++)
        {
            settings[i].color = new Color(0.5f, 0.5f, 0.5f, 1f);
            if (i == selected2)
            {
                settings[i].color = new Color(1f, 1f, 1f, 1f);
            }
        }

        yield return new WaitForSeconds(0.2f);
        cooldown = false;
    }

    private void SubMenu()
    {
        if (selected != 2)
        {
            background.sprite = backgroundSprites[1];
        }
        else
        {
            background.sprite = backgroundSprites[2];
        }
        menuLayer = 1;
        if (selected == 0)
        {
            for (int i = 0; i < freeplayModes.Length; i++)
            {
                freeplayModes[i].gameObject.SetActive(true);
            }
            freeplayLocation.SetActive(true);
            freeplayMapSS.SetActive(true);
            for (int i = 0; i < freeplayIcons.Length; i++)
            {
                freeplayIcons[i].gameObject.SetActive(true);
            }
        }
        if (selected == 1)
        {
            for (int i = 0; i < challenges.Length; i++)
            {
                challenges[i].gameObject.SetActive(true);
            }

        }
        if (selected == 2)
        {
            for (int i = 0; i < encyclopediaEntries.Length; i++)
            {
                encyclopediaEntries[i].gameObject.SetActive(true);
            }
            encyclopediaTxt.gameObject.SetActive(true);
            logo.SetActive(false);
            ePanel.SetActive(true);

        }
        if (selected == 3)
        {
            for (int i = 0; i < settings.Length; i++)
            {
                settings[i].gameObject.SetActive(true);
            }
        }
        StartCoroutine(TriggerCoolDown());
    }

    private void TopMenu()
    {
        background.sprite = backgroundSprites[0];
        menuLayer = 0;
        selected2 = 0;
        StartCoroutine(TriggerCoolDown());

        if (selected == 0)
        {
            for (int i = 0; i < freeplayModes.Length; i++)
            {
                freeplayModes[i].gameObject.SetActive(false);
            }
            freeplayLocation.SetActive(false);
            freeplayMapSS.SetActive(false);
            for (int i = 0; i < freeplayIcons.Length; i++)
            {
                freeplayIcons[i].gameObject.SetActive(false);
            }
        }
        if (selected == 1)
        {
            for (int i = 0; i < challenges.Length; i++)
            {
                challenges[i].gameObject.SetActive(false);
            }
        }
        if (selected == 2)
        {
            for (int i = 0; i < encyclopediaEntries.Length; i++)
            {
                encyclopediaEntries[i].gameObject.SetActive(false);
            }
            encyclopediaTxt.gameObject.SetActive(false);
            logo.SetActive(true);
            ePanel.SetActive(false);
        }
        if (selected == 3)
        {
            for (int i = 0; i < settings.Length; i++)
            {
                settings[i].gameObject.SetActive(false);
            }
        }
    }

    private IEnumerator LoadLevel(string level)
    {
        fade.StartFadeOut();
        yield return new WaitForSeconds(2f);
        Application.LoadLevel(level);
    }

    private void SetEncyclopediaText()
    {
        articles = new string[8];
        articles[0] = "Controls (Gamepad/Keyboard):" + "\n" + "Look around: RS or Mouse" + "\n" + "Movement: LS or WASD keys" + "\n" + "Pick Up / Drop Objects: A or Space key" + "\n" + "Attack (Must grow via tears): X or X key" + "\n" + "Camera Zoom: D-Pad or Scroll Wheel" + "\n" + "\n" + "Keyboard controls can be changed in the settings menu.";
        articles[1] = "Welcome to The Giant of Torridge Island! This article will go over the basics of the game and your abilities as the Giant." + "\n" + "The Goal of the game is to shape the island to your liking. As the Giant you have the power to pick up anything on the island (as long as you grow big enough). In order to grow you need to interact with the villagers of the island. The villagers will give out stars for good deeds and tears for bad ones." + "\n" + "Good deeds include completing requests from the villagers such as collecting materials or making space for new buildings." + "\n" + "Some bad deeds include hurting the villagers or destroying their buildings" + "\n" + "The deeds you complete will then be collected to create a villager score at the end of the day. The other score you are given is a nature score." + "\n" + "Much like the villager score, the nature score can be changed via good and bad deeds towards the enviroment. Good deeds include planting trees and breeding animals, while bad deeds include chopping down trees and hunting animals";
        articles[2] = "Trees and Stones are vital to the island residents as they are materials needed to create any new buildings or structures. Both aren't unlimited resources however, so it is crucial that The Giant manages them effectivly." + "\n" + "Trees aren't only critical to the villagers, but also to the nature of the island so be sure to keep the tree population healthy. The Giant can create seeds for trees by placing 2 trees next to eachother While this will kill the 2 trees they will leave behind 4 seeds which will grow into trees the following day." + "\n" + "After a couple days trees will begin to wilt and their leaves will turn orange. When this happens that specific tree will die by the following morning." + "\n" + "Stones on the otherhand will stay on the island forever. They also don't affect the nature score in anyway. However, the only way the number of stones can be replenished is by either destroying buildings or villagers passing away. Only the Giant can decide if that's a worthy trade.";
        articles[3] = "These inhabitants live in small communities and have their own cultures and way of live which the giant can decide to either help flurish or destroy. Currently there are two tribes, the red tribe which can be naturally found on grasslands, and the blue tribe which live in snowfields" + "\n" + "Regardless of the tribe, villagers have two main functions: They can build and they can make the Giant grow." + "\n" + "In order to build, the villager needs to gather materials. Sometimes they are capible of doing this on their own, however sometimes they will call for help from the giant, who can carry over the correct matierals for them to collect. Then once they find appropriate space, they will begin to build (and again they may require the giants help to make space for the new structure)." + "\n" + "When good or evil deeds are performed by the Giant to the villagers, they will grant the Giant with a star or tear respectivly. Collect enough of either and the Giant will grow, becoming stronger and learning new abilities to influence the island further.";
        articles[4] = "Wildlife can be found in the skies, on the ground and in water. Each type of wildlife can be harnest to influence the island in different ways." + "\n" + "Livestock found on the island can be hunted by villagers. However they are not an infinite resource, so ensure that the population stays healthy by breeding them. This can be done by putting two of the same species of livestock next to eachother (as long as they haven't already mated during that day)." + "\n" + "Currently fish and birds can't be interacted with by the villagers, however they are still crucial to keeping the nature of the island healthy." + "\n" + "Fish are the only wildlife that's population replenishes everyday. The Giant can catch fish and feed them to the birds and increase the bird population, as well as improving the overall standing of nature on the island.";
        articles[5] = "Farms are found exclusivly in grassland villages, and are the key source of food for the inhabitants. The crops usually take around half a day in order to grow. Once fully grown it is ready to be harvested, you can tell whether the crop is fully grown by seeing if the crop is still growing upwards." + "\n" + "Villagers can harvest the crops on their own intuition, however if The Giant wishes, they can drop a villager into the wheatfield to speed up the process." +"\n"+ "Whether the Giant was involved or not, a star will be given to The Giant once the harvest is complete, and the cycle will repeat." + "\n" + "Crops will not maintain their growth over night, so it's vital that they are harvested before sundown.";
        articles[6] = "Penguins are a species of bird which are native to the snow biomes on the island." + "\n" + "Unlike other bird species, penguins can be interacted with by both the Giant and the villagers due to their flightless nature." + "\n" + "As with any other species of bird the Giant can grow the population by fishing. Their population will also naturally decrease day on day." + "\n" + "Villagers will not hunt the penguins as they are rather useful to them. penguins can hunt for fish in the sea and then pass them on to the villagers to eat.";
        articles[7] = "Villagers aren't stuck in the village they are born in, they can be moved freely between villages regardless of the biome they are found in. However, a villager moving to a new biome will take a day to adjust to the new enviroment and will not build until they have adapted to their new home." + "\n" +"\n" + "The Giant can also create new villages by bringing a male and female villager from any tribe and bringing them together (as long as they are far away from pre-established villages).";
        encyclopediaTxt.text = articles[0];
    }

    private IEnumerator RebindKeyboard()
    {
        instructionTxt.gameObject.SetActive(true);
        instructionTxt.text = "Press the key you wish to assign to picking up/dropping objects";
        while (Input.anyKey)
        {
            yield return new WaitForEndOfFrame();
        }
        while (!Input.anyKey)
        {
            yield return new WaitForEndOfFrame();
        }
        foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKey(kcode))
                inputManager.pickUp = kcode.ToString().ToLower();
        }
        instructionTxt.text = "Press the key you wish to assign to Attacking objects";
        while (Input.anyKey)
        {
            yield return new WaitForEndOfFrame();
        }
        while (!Input.anyKey)
        {
            yield return new WaitForEndOfFrame();
        }
        foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKey(kcode))
                inputManager.attack = kcode.ToString().ToLower();
        }
        fade.StartFadeIn();
        instructionTxt.gameObject.SetActive(false);
        rebinding = false;
        inputManager.SaveBindings();
    }
}
