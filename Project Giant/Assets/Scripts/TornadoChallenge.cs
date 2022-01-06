using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoChallenge : MonoBehaviour
{
    public Dialogue stats;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartChallenge());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator StartChallenge()
    {
        for (int i = 0; i < 100; i++)
        {
            stats.natureScore = stats.natureScore - 1;
            yield return new WaitForSeconds(5f);

        }
    }
}
