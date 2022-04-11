using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public GameObject[] buildings;

    private Animator anim;

    private bool builtToday;
    private bool wood;
    private bool stone;
    private int happyness;
    public GameObject materialChecker;

    private bool fixedMove;

    public GameObject callBox;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AudioPlay());
        builtToday = false;
        anim = gameObject.GetComponent<Animator>();
        //MAke sure they walk around their own village
        if (colour == "mRed" || colour == "fRed")
        {
            townCenter = GameObject.Find("TownCentre").transform;
        }
        else if (colour == "Black")
        {
            townCenter = GameObject.Find("TownCentreB").transform;
        }
        //Start moving
        StartCoroutine(Move());
        if (SceneManager.GetActiveScene().name == "TaddiportLoad")
        {
            actions = new string[4];
            actions[0] = "Nothing";
            actions[1] = "Build";
            actions[2] = "Build";
            actions[3] = "Play";
        }
        else
        {
            actions = new string[4];
            actions[0] = "Build";
            actions[1] = "Build";
            actions[2] = "Build";
            actions[3] = "Play";
        }

        stop = false;

        fixedMove = false;
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
        target = new Vector3((transform.position.x + Random.Range(-10, 10)), transform.position.y, (transform.position.z + Random.Range(-10, 10)));
        while (target.x > townCenter.position.x + 25 || target.x < townCenter.position.x - 25 || target.z > townCenter.position.z + 25 || target.z < townCenter.position.z - 25)
        {
            target = new Vector3((transform.position.x + Random.Range(-10, 10)), transform.position.y, (transform.position.z + Random.Range(-10, 10)));
        }
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

    private IEnumerator FixedMove(Vector3 destination)
    {
        fixedMove = true;
        anim.Play("VillagerWalk");
        target = destination;

        while (Vector3.Distance(transform.position, target) > 0.1f)
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
    }

    public void lifeDown() //When damaged, give out a tear
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
                if (wood || stone)
                {
                    print("Get Materials");
                    if (builtToday == false)
                    {
                        //builtToday = true;

                        if (wood == false)
                        {
                            GameObject check = Instantiate(materialChecker, transform.position, Quaternion.identity);
                            yield return new WaitForSeconds(1f);
                            //if nearby wood
                            if (check.GetComponent<MaterialArea>().tree != null)
                            {
                                print("Getting tree");
                                StartCoroutine(FixedMove(check.GetComponent<MaterialArea>().tree.transform.position));
                            }
                            else
                            {
                                print("Call for tree");
                                GameObject call = Instantiate(callBox, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity);//Call for tree
                                call.GetComponent<CallBox>().setType(0);
                                timer = Time.time; //Start a timer
                                yield return new WaitUntil(() => check.GetComponent<MaterialArea>().tree != null || Time.time - timer > 60f || stop == true);
                                Destroy(call);
                                if (check.GetComponent<MaterialArea>().tree != null)
                                {
                                    yield return new WaitUntil(() => GameObject.Find("Giant").GetComponent<PlayerControl>().carrying != check.GetComponent<MaterialArea>().tree || stop == true);
                                    StartCoroutine(FixedMove(check.GetComponent<MaterialArea>().tree.transform.position));
                                    Instantiate(star, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                                    GameObject.Find("Canvas").transform.Find("Narrator").GetComponent<Dialogue>().GiveTree();
                                }
                            }
                            Destroy(check);
                            break;
                        }
                        else if (stone == false)
                        {
                            GameObject check = Instantiate(materialChecker, transform.position, Quaternion.identity);
                            yield return new WaitForSeconds(1f);
                            //if nearby stone
                            if (check.GetComponent<MaterialArea>().stone != null)
                            {
                                print("Getting stone");
                                StartCoroutine(FixedMove(check.GetComponent<MaterialArea>().stone.transform.position));
                            }
                            else
                            {
                                print("Call for stone");
                                GameObject call = Instantiate(callBox, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity);//Call for stone
                                call.GetComponent<CallBox>().setType(1);
                                timer = Time.time; //Start a timer
                                yield return new WaitUntil(() => check.GetComponent<MaterialArea>().stone != null || Time.time - timer > 60f || stop == true);
                                Destroy(call);
                                if (check.GetComponent<MaterialArea>().stone != null)
                                {
                                    yield return new WaitUntil(() => GameObject.Find("Giant").GetComponent<PlayerControl>().carrying != check.GetComponent<MaterialArea>().stone || stop == true);
                                    StartCoroutine(FixedMove(check.GetComponent<MaterialArea>().stone.transform.position));
                                    Instantiate(star, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                                    GameObject.Find("Canvas").transform.Find("Narrator").GetComponent<Dialogue>().GiveStone();
                                }
                            }
                            Destroy(check);
                            break;
                        }
                        else 
                        {
                            action = Random.Range(0, buildings.Length);
                            //if there is enough space build
                            GameObject build = Instantiate(buildArea, new Vector3(transform.position.x + 2, transform.position.y, transform.position.z), Quaternion.identity);
                            yield return new WaitForSeconds(0.5f);
                            build.SendMessage("Check", gameObject, SendMessageOptions.DontRequireReceiver); //Start checking whether it can build there.
                            timer = Time.time; //Start a timer
                            GameObject call = Instantiate(callBox, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity);//Start Asking for help
                            call.GetComponent<CallBox>().setType(2);
                            yield return new WaitUntil(() => canBuild == true || Time.time - timer > 60f || stop == true); //Wait until it can build or 60secs pass.
                            Destroy(call);//Stop calling for help
                            if (canBuild == true)
                            {
                                if (Time.time - timer >3f)
                                {
                                    GameObject.Find("Canvas").transform.Find("Narrator").GetComponent<Dialogue>().ClearedArea();
                                }
                                GameObject newObject = Instantiate(buildings[action], new Vector3(build.transform.position.x, build.transform.position.y, build.transform.position.z), Quaternion.identity);
                                Destroy(build);
                                FixedMove(build.transform.position); // move into the scaffolding to avoid being hit by the giant.
                                Instantiate(star, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                                canBuild = false;
                            }
                            else //If villager gets kicked, picked up or the time runs out, cancel build.
                            {
                                Destroy(build);
                            }


                        }


                        ////////////////Old Building style.////////////////////////

                        //GameObject build = Instantiate(buildArea, new Vector3(transform.position.x + 2, transform.position.y, transform.position.z), Quaternion.identity);
                        //yield return new WaitForSeconds(0.5f);
                        //build.SendMessage("Check", gameObject, SendMessageOptions.DontRequireReceiver); //Start checking whether it can build there.

                        //timer = Time.time; //Start a timer
                        //anim.Play("VillagerWave");
                        //yield return new WaitUntil(() => canBuild == true || Time.time - timer > 60f); //Wait until it can build or 60secs pass.
                        //if (canBuild == true) //if it can, Build the object and give out a star 
                        //{
                        //    GameObject newObject = Instantiate(buildings[action], new Vector3(build.transform.position.x, build.transform.position.y + 3, build.transform.position.z), Quaternion.identity);
                        //    build = null;
                        //    newObject.transform.Rotate(0, Random.Range(0, 360), 0);
                        //    newObject.SendMessage("Built", SendMessageOptions.DontRequireReceiver);
                        //    canBuild = false;
                        //    Instantiate(star, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                        //    yield return new WaitForSeconds(5);
                        //}
                    }
                }
                else
                {
                    print("No current Materials");
                    GameObject check = Instantiate(materialChecker, transform);
                    yield return new WaitForSeconds(1f);
                    if (check.GetComponent<MaterialArea>().stone != null)
                    {
                        print("Getting stone");
                        StartCoroutine(FixedMove(check.GetComponent<MaterialArea>().stone.transform.position));
                    }
                    else if(check.GetComponent<MaterialArea>().tree != null)
                    {
                        print("Getting wood");
                        StartCoroutine(FixedMove(check.GetComponent<MaterialArea>().tree.transform.position));
                    }
                    Destroy(check);
                }
                break;
            case "Play":
                anim.Play("VillagerPlay1");
                for (int i = 0; i < 14; i++)
                {
                    yield return new WaitForSeconds(1f);
                    if (stop == true)
                    {
                        break;
                    }
                }
                break;
                

        }


        yield return new WaitForSeconds(1);
        //print("Action end");
        //SHOULD NOT CALL FOR MOVE IF FIXEDMOVE HAS BEEN CALLED!!!!!!
        if (fixedMove == false)
        {
            StartCoroutine(Move()); //Once the action is complete, start walking again.
        }
        else
        {
            fixedMove = false;
        }

    }

    private void OnTriggerEnter(Collider collision) //If it comes into contact with something
    {
        //print("collision Entered");
        if (collision.GetComponent<PlayerControl>() != null && (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)) //If it's the player and its moving
        {
            if (collision.GetComponent<PlayerControl>().carrying != gameObject) //If it isn't being carried around by the player
            {
                //It has been kicked by the player, play damage sound effect
                gameObject.GetComponent<AudioSource>().Play(0);
                GameObject.Find("Canvas").transform.Find("Narrator").GetComponent<Dialogue>().VillagerKicked();
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
                if (life == 1)
                {
                    GameObject.Find("Canvas").transform.Find("Narrator").GetComponent<Dialogue>().VillagerKickedDead();
                }
                lifeDown();

            }

        }
        else if (collision.GetComponent<Object>() != null)
        {
            if (collision.gameObject.GetComponent<Object>().item == "scaffolding") //if it is scaffolding, help build
            {
                stop = true;
                anim.Play("VillagerBuild");
                StartCoroutine(TimedEvent(5f));
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
                wood = true;
                
            }
            if (collision.gameObject.GetComponent<Object>().item == "stone") //if it's a stone, mine it
            {
                anim.Play("VillagerMine");
                yield return new WaitForSeconds(5);
                collision.gameObject.GetComponent<Object>().lifeDown();
                stone = true;
            }
            if (collision.gameObject.GetComponent<Object>().item == "sheep") //if it's a sheep, kill it
            {
                GameObject.Find("Canvas").transform.Find("Narrator").GetComponent<Dialogue>().Hunting();
                anim.Play("VillagerMine");
                yield return new WaitForSeconds(2f);
                collision.gameObject.GetComponent<Object>().lifeDown();
                anim.Play("VillagerEat");
                yield return new WaitForSeconds(10f);
                Instantiate(star, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            }
            if (collision.gameObject.GetComponent<Object>().item == "farm") //if it's a farm, Check if it can be harvested. 
            {
                if (collision.gameObject.GetComponent<Farm>().harvest == true)
                {
                    anim.Play("VillagerChop");
                    collision.gameObject.GetComponent<Farm>().Harvest();
                    yield return new WaitForSeconds(8f);
                    Instantiate(star, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);

                }
                else
                {
                    anim.Play("VillagerBuild");
                    yield return new WaitForSeconds(5f);
                }

            }
            stop = false;
            StartCoroutine(Move()); //Start walking again

        }
    }

    private IEnumerator TimedEvent(float time)
    {
        stop = true;
        yield return new WaitForSeconds(time);
        stop = false;
        StartCoroutine(Move());
    }

    private IEnumerator AudioPlay()
    {
        while (life > 0)
        {
            yield return new WaitForSeconds(Random.Range(10f, 30f));
            GetComponent<AudioSource>().Stop();
            GetComponent<AudioSource>().Play();
        }
    }



}
