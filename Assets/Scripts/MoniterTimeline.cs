using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


public class MoniterTimeline : MonoBehaviour
{
    private PlayableDirector timeline;
    public GameObject handleHUDgameObject;

    private void Start()
    {
        timeline = GameObject.Find("Waking up from first Dream TImeline").GetComponent<PlayableDirector>();
    }

    private void Update()
    {
        if (timeline.state != PlayState.Playing)
        {
            Debug.Log("timeline stopped");
        }
    }
}
