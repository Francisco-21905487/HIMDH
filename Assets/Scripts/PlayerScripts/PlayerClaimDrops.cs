using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class PlayerClaimDrops : MonoBehaviour
{
    private GameObject Player;

    private Transform weapons;

    public float Distance;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, Player.transform.position) < Distance && Input.GetKeyDown("e"))
        {
            Debug.Log("You claim the " + gameObject.name);
            Destroy(gameObject);
            //switch this weapon to the one equiped and drop the equiped weapon

        }
    }
}
