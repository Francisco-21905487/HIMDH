using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    private GameObject Player;
    public GameObject[] Weapons;
    public GameObject Potions;

    public float Distance;

    private bool ChestOpen = false;


    private void Start()
    {
        Player = GameObject.Find("Player");
    }
    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Vector2.Distance(transform.position, Player.transform.position));
        if (ChestOpen == false)
        {
            if (Vector2.Distance(transform.position, Player.transform.position) < Distance && Input.GetKeyDown("e"))
            {
                Debug.Log("Chest Opened");
                ChestOpen = true;
                GetComponent<Renderer>().material.color = Color.red;

                int percentage = Random.Range(1, 100);
                Vector3 WOffset = new Vector3(0, 0.817f, 0);
                Vector3 POffset = new Vector3(0.50f, 0.817f, 0);

                Instantiate(Potions, transform.position - POffset, Quaternion.identity);

                if (percentage > 0 && percentage < 50)
                {
                    Instantiate(Weapons[0], transform.position - WOffset, Quaternion.identity);
                }
                else if (percentage >= 50 && percentage <= 100)
                {
                    Instantiate(Weapons[1], transform.position - WOffset, Quaternion.identity);
                }
            }
        }
    }
}
