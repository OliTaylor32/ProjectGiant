using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Villager : MonoBehaviour
{
    public string item;
    public int weight;
    public Transform townCenter;
    public int speed = 1;
    public int life;
    private string[] actions;
    public GameObject buildArea;
    public bool canBuild;
    public float timer;
    Vector3 target;
    public GameObject star;
    public GameObject tear;
    public string colour;
    private bool stop;

    private GameObject[] buildings;

    public GameObject snowMan;
    public GameObject smallHouse;
    public GameObject torch;
    public GameObject totem;
    public GameObject basicWoodWorkshop;

    private Animator anim;

    private bool builtToday;


    // Start is called before the first frame update
    void Start()
    {
        builtToday = false;
        anim = gameObject.GetComponent<Animator>();
        //MAke sure they walk around their own village
        if (colour == "Blue")
        {
            townCenter = GameObject.Find("TownCentre").transform;
        }
        else if (colour == "Black")
        {
            townCenter = GameObject.Find("TownCentreB").transform;
        }
        //Start moving
        StartCoroutine(Move());

        actions = new string[2];
        actions[0] = "Nothing";
        actions[1] = "Build";

        buildings = new GameObject[5];
        buildings[0] = snowMan;
        buildings[1] = smallHouse;
        buildings[2] = torch;
        buildings[3] = totem;
        buildings[4] = basicWoodWorkshop;
        stop = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (life < 1) //When the villager dies, give out a tear before dying.
        {
            Instantiate(tear, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private IEnumerator Move() //Walk to a random point 
    {
        anim.Play("VillagerWalk");
        //Random area around the village
        target = new Vector3((townCenter.position.x + Random.Range(-25, 25)), transform.position.y, (townCenter.position.z + Random.Range(-25, 25)));
        while (Vector3.Distance(transform.position, target) > 1)
        {
            //print("step");
            transform.LookAt(new Vector3(target.x, transform.position.y, target.z)); //Look at the target and straight ahead
            transform.Rotate(0, -90, 0); //Fixes model (was faster than re-doing all the animation)
            transform.rotation.Set(0, transform.rotation.y, 0, 0);  //Make sure the villager is looking straight ahead
            transform.position = Vector3.MoveTowards(transform.position, target, ((speed * 0.1f) * Time.deltaTime)); //Move forwards (Towards the target)
            yield return new WaitForSeconds(0.01f);
            if (stop == true) //If the villager interacts with an object or the player, stop moving
            {
                break;
            }
        }
        if (stop == false) //If the player wasn't stopped before reaching its destination, perform an action.
        {
            StartCoroutine(Action());
        }

    }

    private void lifeDown() //When damaged, give out a tear
    {
        life--;
        Instantiate(tear, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
    }

    private void GetWeight(GameObject sender) //Return weight to the giant
    {
        sender.SendMessage("ReturnWeight", weight, SendMessageOptions.DontRequireReceiver);
        print("Sent Weight");
    }

    private IEnumerator Action() //When the destination is reached, build something or do nothing
    {
        anim.Play("VillagerIdle");
        print("Action start");
        int action = Random.Range(0, actions.Length); 
        switch (actions[action])
        {
            case "Nothing": //Do nothing
                print("No Action Taken");
                anim.Play("VillagerIdle");
                break;
            case "Build": //Create a random building from the buildings array
                print("Building");
                if (builtToday == false)
                {
                    builtToday = true;
                    action = Random.Range(0, buildings.Length); ;

                    GameObject build = Instantiate(buildArea, new Vector3(transform.position.x + 2, transform.position.y, transform.position.z), Quaternion.identity);
                    yield return new WaitForSeconds(0.5f);
                    build.SendMessage("Check", gameObject, SendMessageOptions.DontRequireReceiver); //Start checking whether it can build there.

                    timer = Time.time; //Start a timer
                    anim.Play("VillagerWave");
                    yield return new WaitUntil(() => canBuild == true || Time.time - timer > 60f); //Wait until it can build or 60secs pass.
                    if (canBuild == true) //if it can, Build the object and give out a star 
                    {
                        GameObject newObject = Instantiate(buildings[action], new Vector3(build.transform.position.x, build.transform.position.y, build.transform.position.z), Quaternion.identity);
                        build = null;
                        newObject.transform.Rotate(0, Random.Range(0, 360), 0);
                        newObject.SendMessage("Built", SendMessageOptions.DontRequireReceiver);
                        canBuild = false;
                        Instantiate(star, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                        yield return new WaitForSeconds(5);
                    }
                }
                    break;
                

        }


        yield return new WaitForSeconds(3);
        print("Action end");
        StartCoroutine(Move()); //Once the action is complete, start walking again.

    }

    private void OnTriggerEnter(Collider collision) //If it comes into contact with something
    {
        print("collision Entered");
        if (collision.GetComponent<PlayerControl>() != null && Input.anyKey) //If it's the player and its moving
        {
            if (collision.GetComponent<PlayerControl>().carrying != gameObject) //If it isn't being carried around by the player
            {
                //It has been kicked by the player, play damage sound effect
                gameObject.GetComponent<AudioSource>().Play(0);
                //face the way the player is facing
                transform.Rotate(transform.rotation.x, collision.gameObject.transform.rotation.y, transform.rotation.z); 
                //Depending on way way the giant was facing, change the target destination and move away from the Giant.
                if (collision.gameObject.transform.rotation.y >= -45 && collision.gameObject.transform.rotation.y < 45)
                {
                    target = new Vector3(transform.position.x, transform.position.y, transform.position.z + 10);
                    transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z + 2);
                }

                if (collision.gameObject.transform.rotation.y >= 45 && collision.gameObject.transform.rotation.y < 135)
                {
                    target = new Vector3(transform.position.x + 10, transform.position.y, transform.position.z);
                    transform.position = new Vector3(transform.position.x + 2, transform.position.y + 1, transform.position.z);
                }

                if (collision.gameObject.transform.rotation.y >= 135 || collision.gameObject.transform.rotation.y < -135)
                {
                    target = new Vector3(transform.position.x, transform.position.y, transform.position.z - 10);
                    transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z - 2);
                }

                if (collision.gameObject.transform.rotation.y >= -135 && collision.gameObject.transform.rotation.y < -45)
                {
                    target = new Vector3(transform.position.x - 10, transform.position.y, transform.position.z);
                    transform.position = new Vector3(transform.position.x - 2, transform.position.y + 1, transform.position.z);
                }
                //Give out a tear and get damaged.
                Instantiate(tear, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                lifeDown();
            }

        }
    }

    private IEnumerator OnCollisionEnter(Collision collision) //If it comes into contact with something
    {
        if (collision.gameObject.GetComponent<Object>() != null) //And it is an object
        {
            stop = true; //Stop the villager walking.
            if (collision.gameObject.GetComponent<Object>().item == "tree") //if it's a tree, cut it down
            {
                
                anim.Play("VillagerChop");
                yield return new WaitForSeconds(5);
                collision.gameObject.GetComponent<Object>().lifeDown();
                
            }
            stop = false;
            StartCoroutine(Move()); //Start walking again

        }
    }




}
