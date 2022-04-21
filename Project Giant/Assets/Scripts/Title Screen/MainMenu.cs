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
    public TextMeshProUGUI[] challenges;
    public TextMeshProUGUI[] encyclopediaEntries;
    public TextMeshProUGUI encyclopediaTxt;
    private string[] articles;
    public GameObject ePanel;
    public TextMeshProUGUI[] settings;
    public GameObject freeplayMapSS;
    public Image background;

    public Sprite[] backgroundSprites;
    public int selected;
    public int selected2;
    public int menuLayer;

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
                else if (selected2 < 1 || (selected == 3 && selected2 < 2))
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
                        if (selected2 == 0)//New FreePlay Taddiport
                        {
                            StartCoroutine(LoadLevel("Taddiport"));
                        }
                        if (selected2 == 1)
                        {
                            StartCoroutine(LoadLevel("TaddiportLoad"));
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
        articles = new string[6];
        articles[0] = "Controls (Gamepad/Keyboard):" + "\n" + "Look around: RS or Mouse" + "\n" + "Movement: LS or WASD keys" + "\n" + "Pick Up / Drop Objects: A or Space key" + "\n" + "Attack (Must grow via tears): X or X key" + "\n" + "Camera Zoom: D-Pad or Scroll Wheel" + "\n" + "\n" + "Keyboard controls can be changed in the settings menu.";
        articles[1] = "Welcome to The Giant of Torridge Island! This article will go over the basics of the game and your abilities as the Giant." + "\n" + "The Goal of the game is to shape the island to your liking. As the Giant you have the power to pick up anything on the island (as long as you grow big enough). In order to grow you need to interact with the villagers of the island. The villagers will give out stars for good deeds and tears for bad ones." + "\n" + "Good deeds include completing requests from the villagers such as collecting materials or making space for new buildings." + "\n" + "Some bad deeds include hurting the villagers or destroying their buildings" + "\n" + "The deeds you complete will then be collected to create a villager score at the end of the day. The other score you are given is a nature score." + "\n" + "Much like the villager score, the nature score can be changed via good and bad deeds towards the enviroment. Good deeds include planting trees and breeding animals, while bad deeds include chopping down trees and hunting animals";
        articles[2] = "Trees and Stones";
        articles[3] = "Villagers";
        articles[4] = "Wildlife";
        articles[5] = "Farms";
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
