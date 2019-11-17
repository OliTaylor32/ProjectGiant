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

    private GameObject[] buildings;

    public GameObject snowMan;
    public GameObject smallHouse;
    public GameObject torch;
    public GameObject totem;


    // Start is called before the first frame update
    void Start()
    {
        townCenter = GameObject.Find("TownCenter").transform;
        StartCoroutine(Move());

        actions = new string[2];
        actions[0] = "Nothing";
        actions[1] = "Build";

        buildings = new GameObject[4];
        buildings[0] = snowMan;
        buildings[1] = smallHouse;
        buildings[2] = torch;
        buildings[3] = totem;
    }

    // Update is called once per frame
    void Update()
    {
        if (life == 0)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator Move()
    {
        gameObject.GetComponent<Animator>().Play("VillagerWalk");
        target = new Vector3((townCenter.position.x + Random.Range(-25, 25)), transform.position.y, (townCenter.position.z + Random.Range(-25, 25)));
        transform.LookAt(target);
        transform.Rotate(0, -90, 0);
        while (Vector3.Distance(transform.position, target) > 1)
        {
            //print("step");
            transform.position = Vector3.MoveTowards(transform.position, target, ((speed * 0.1f) * Time.deltaTime));
            yield return new WaitForSeconds(0.01f);
        }
        StartCoroutine(Action());

    }

    private void lifeDown()
    {
        life--;
    }

    private void GetWeight(GameObject sender)
    {
        sender.SendMessage("ReturnWeight", weight, SendMessageOptions.DontRequireReceiver);
        print("Sent Weight");
    }

    private IEnumerator Action()
    {
        print("Action start");
        int action = Random.Range(0, 2);
        switch (actions[action])
        {
            case "Nothing":
                print("No Action Taken");
                break;
            case "Build":
                print("Building");
                action = Random.Range(0, buildings.Length);;
                print("About to create buildArea");
                GameObject newObject = Instantiate(buildArea, new Vector3(transform.position.x + 3, transform.position.y, transform.position.z), Quaternion.identity);
                print("buildArea spawned");
                yield return new WaitForSeconds(0.5f);
                newObject.SendMessage("Check", gameObject, SendMessageOptions.DontRequireReceiver);
                print("Check request sent");
                newObject = null;
                print("call for help");
                timer = Time.time;
                yield return new WaitUntil(() => canBuild == true || Time.time - timer > 60f);
                if (canBuild == true)
                {
                    print("Start building");

                    if (action >= 2)
                    {
                        //    StartCoroutine(HelpTree());
                    }
                    newObject = Instantiate(buildings[action], new Vector3(transform.position.x + 1, transform.position.y + 0.5f, transform.position.z), Quaternion.identity);
                    newObject.transform.Rotate(0, Random.Range(0, 360), 0);
                    newObject.SendMessage("Built", SendMessageOptions.DontRequireReceiver);
                    canBuild = false;
                    yield return new WaitForSeconds(5);
                }
                    break;
                

        }


        yield return new WaitForSeconds(3);
        print("Action end");
        StartCoroutine(Move());

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Giant" && Input.anyKey)
        {
            //play kicked animation
            transform.Rotate(transform.rotation.x, collision.gameObject.transform.rotation.y, transform.rotation.z);
            //play running animation
            if (collision.gameObject.transform.rotation.y >= -45 && collision.gameObject.transform.rotation.y < 45)
            {
                target = new Vector3(target.x, target.y, transform.position.z + 10);
            }

            if (collision.gameObject.transform.rotation.y >= 45 && collision.gameObject.transform.rotation.y < 135)
            {
                target = new Vector3(target.x + 10, target.y, transform.position.z);
            }

            if (collision.gameObject.transform.rotation.y >= 135 || collision.gameObject.transform.rotation.y < -135)
            {
                target = new Vector3(target.x, target.y, transform.position.z - 10);
            }

            if (collision.gameObject.transform.rotation.y >= -135 && collision.gameObject.transform.rotation.y < -45)
            {
                target = new Vector3(target.x, target.y, transform.position.z - 10);
            }

        }
    }




}
