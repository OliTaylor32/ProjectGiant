using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    ControlsSave data;
    public string pickUp;
    public string attack;
    public bool invertY;
    // Start is called before the first frame update

    private void Awake()
    {

    }

    void Start()
    {
        data = new ControlsSave();
        data.Load();
        DontDestroyOnLoad(gameObject);
        LoadBindings();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LoadBindings()
    {
        if (data.bindings != null)
        {
            print("Controls data found");
            pickUp = data.bindings[0];
            attack = data.bindings[1];
            invertY = data.invertY;
        }
        else
        {
            print("No controls saved");
        }
    }

    public void SaveBindings()
    {
        string[] bindings = new string[2];
        bindings[0] = pickUp;
        bindings[1] = attack;
        ControlsSave save = new ControlsSave(bindings, invertY);
        data = save;
        print("Bindings Saved");
        LoadBindings();
    }
}
