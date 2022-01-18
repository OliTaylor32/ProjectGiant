using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm : MonoBehaviour
{
    public bool harvest;
    private Animator anim;
    private AnimatorClipInfo[] clip;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        harvest = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (harvest == false)
        {
            clip = anim.GetCurrentAnimatorClipInfo(0);
            if (clip[0].clip.name == "RedFarmGrown")
            {
                harvest = true;
            }

        }
    }

    public void Harvest()
    {
        anim.Play("RedFarmHarvest");
        harvest = false;
    }
}
