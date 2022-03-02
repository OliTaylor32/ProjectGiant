using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private bool cooldown;
    public TextMeshProUGUI[] modes;
    public TextMeshProUGUI[] freeplayModes;
    public GameObject freeplayLocation;
    public TextMeshProUGUI[] challenges;
    public GameObject settings;
    public GameObject freeplayMapSS;
    public Image background;

    public Sprite[] backgroundSprites;
    public int selected;
    public int selected2;
    public int menuLayer;

    public Fade fade;
    // Start is called before the first frame update
    void Start()
    {
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
        for (int i = 0; i < challenges.Length; i++)
        {
            challenges[i].gameObject.SetActive(false);
        }
        settings.SetActive(false);
    }

    // Update is called once per frame
    void Update()
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
            else if (selected2 < 1)
            {
                selected2++;
            }
            StartCoroutine(TriggerCoolDown());
        }

        if (Input.GetButtonDown("PickUp"))
        {
            if (menuLayer == 0)
            {
                if (selected == 3)
                {
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

                if (selected == 2)
                {
                    Screen.fullScreen = !Screen.fullScreen;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TopMenu();
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
        yield return new WaitForSeconds(0.2f);
        cooldown = false;
    }

    private void SubMenu()
    {
        background.sprite = backgroundSprites[1];
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
            settings.SetActive(true);
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
        settings.SetActive(false);
    }

    private IEnumerator LoadLevel(string level)
    {
        fade.StartFadeOut();
        yield return new WaitForSeconds(2f);
        Application.LoadLevel(level);
    }
}
