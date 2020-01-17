using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndSceneMusic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var obj = GameObject.FindGameObjectWithTag("Music");
        if(obj)
            obj.GetComponent<MusicPlayer>().StopMusic();
    }
}
