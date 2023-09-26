using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbySceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Create();
        ObjectManager.Create();
        BackGroundManager.Create();
        CSVManager.Create();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
