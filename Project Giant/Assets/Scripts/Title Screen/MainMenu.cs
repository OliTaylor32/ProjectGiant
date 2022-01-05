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
    public Image background;

    public Sprite[] backgroundSprites;
    private int selected;
    private int selected2;
    private int menuLayer;

    public Fade fade;
    // Start is called before the first frame update
    void Start()
    {
        //Top Menu
        selected = 1;
        selected2 = 0;
        for (int i = 1; i < modes.Length; i++)
        {
            modes[i].color = new Color (0.5f, 0.5f, 0.5f, 1f);
        }
        modes[1].color = new Color(1f, 1f, 1f, 1f);

        //FreePlay Menu
        freeplayModes[0].color = new Color(1f, 1f, 1f, 1f);
        freeplayModes[1].color = new Color(0.5f, 0.5f, 0.5f, 1f);

        for (int i = 0; i < freeplayModes.Length; i++)
        {
            freeplayModes[i].gameObject.SetActive(false);
        }

        freeplayLocation.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Vertical") > 0.1f && selected > 0 && cooldown == false)
        {
            if (menuLayer == 0)
            {
                if (selected > 1)
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

        if (Input.GetAxis("Vertical") < -0.1f && selected < modes.Length - 1 && cooldown == false)
        {
            if (menuLayer == 0)
            {
                selected++;
            }
            else if (selected2 < 1)
            {
                selected2++;
            }
            StartCoroutine(TriggerCoolDown());
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (menuLayer == 0)
            {
                SubMenu();
            }
            else
            {
                if (selected == 1)
                {
                    if (selected2 == 0)//New FreePlay Taddiport
                    {
                        StartCoroutine(LoadLevel("Taddiport"));
                    }
                    if (selected2 == 0)
                    {
                        StartCoroutine(LoadLevel("TaddiportLoad"));
                    }
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
        for (int i = 1; i < modes.Length; i++)
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
        yield return new WaitForSeconds(0.2f);
        cooldown = false;
    }

    private void SubMenu()
    {
        background.sprite = backgroundSprites[1];
        menuLayer = 1;
        if (selected == 1)
        {
            for (int i = 0; i < freeplayModes.Length; i++)
            {
                freeplayModes[i].gameObject.SetActive(true);
            }
            freeplayLocation.SetActive(true);
        }
        StartCoroutine(TriggerCoolDown());
    }

    private void TopMenu()
    {
        background.sprite = backgroundSprites[0];
        menuLayer = 0;
        selected2 = 0;
        StartCoroutine(TriggerCoolDown());

        if (selected == 1)
        {
            for (int i = 0; i < freeplayModes.Length; i++)
            {
                freeplayModes[i].gameObject.SetActive(false);
            }
            freeplayLocation.SetActive(false);
        }
    }

    private IEnumerator LoadLevel(string level)
    {
        fade.StartFadeOut();
        yield return new WaitForSeconds(2f);
        Application.LoadLevel(level);
    }
}
