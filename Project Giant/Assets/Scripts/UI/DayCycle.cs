using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCycle : MonoBehaviour
{
    private float timer;
    private bool saved;
    public bool day;
    public Fade fade;

    private GameObject rain;
    public Material rainSkybox;
    // Start is called before the first frame update
    void Start()
    {
        day = true;
        saved = false;
        timer = Time.time; //Start the timer
        rain = GameObject.Find("Rain");
        int random = Random.Range(0, 5);
        if (random == 0)
        {
            print("Rainy Day");
            RenderSettings.skybox = rainSkybox;
            GetComponent<Light>().intensity = 0.4f;
            RenderSettings.ambientSkyColor = new Color(0.3f, 0.3f, 0.3f);
        }
        else
        {
            Destroy(rain);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Time.time - timer > 1f && saved == false)
        {
            #pragma warning disable CS0618 // Type or member is obsolete
            if (Application.loadedLevel == 0)
            {
                Application.LoadLevel("TitleScreen");
            }
            #pragma warning restore CS0618 // Type or member is obsolete
            gameObject.GetComponent<Save>().save();
            saved = true;
            if (GameObject.Find("Giant") != null)
            {
                GameObject.Find("Giant").GetComponent<PlayerControl>().Freeze();
                day = false;
                GameObject.Find("Canvas").transform.Find("EndPanel").
                    GetComponent<EndStats>().SetPopulationStats(gameObject.GetComponent<Save>().mRedVillagers + gameObject.GetComponent<Save>().fRedVillagers,
                    gameObject.GetComponent<Save>().treeNo + gameObject.GetComponent<Save>().treeWiltNo, gameObject.GetComponent<Save>().livestock, gameObject.GetComponent<Save>().birds);
            }
        }
        if (Time.time - timer >= 640f) //After 5mins, boot the player back to the title screen
        {
            #pragma warning disable CS0618 // Type or member is obsolete
            Application.LoadLevel("MainMenu");
            #pragma warning restore CS0618 // Type or member is obsolete
        }
        if (Time.time - timer >= 635f)
        {
            GetComponent<Save>().save();
            fade.StartFadeOut();
        }
    }
}
