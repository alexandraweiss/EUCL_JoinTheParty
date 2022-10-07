using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    [SerializeField] float aggressiveness = 0.5f;
    public float aggressionDanceMin = 0.3f;
    public float aggresionMarchMin = 0.7f;
    public AudioSource globalAudio;
    public AudioClip dance;
    public AudioClip idle;
    public AudioClip march;
    protected string currentState = "";
    GameObject[] npcObjects;
    List<NPCBehaviour> npcs = new List<NPCBehaviour>();
    public static float START_AGGRESSION = 0.5f;

    void Start()
    {
        npcObjects = GameObject.FindGameObjectsWithTag("NPC");
        foreach (GameObject go in npcObjects)
        {
            npcs.Add(go.GetComponentInChildren<NPCBehaviour>());
        }

    }


    public void AddAggression(float delta)
    {
        aggressiveness += delta;
        aggressiveness = Mathf.Clamp01(aggressiveness);
        Debug.Log(aggressiveness);

        foreach (NPCBehaviour npc in npcs)
        {
            //Debug.Log(npc.gameObject.name);
            if (UnityEngine.Random.Range(0f, 60f) >= DateTime.Now.Second)
            {
                //Debug.LogWarning(npc.gameObject.name);
                npc.Aggression = aggressiveness;
                if (npc.Aggression < aggressionDanceMin)
                {
                    npc.PlayDance();
                    if (currentState != "dancing")
                    {
                        globalAudio.Stop();
                        globalAudio.clip = dance;
                        globalAudio.Play();
                        currentState = "dancing";
                    }
                }
                else if (npc.Aggression > aggresionMarchMin)
                {
                    npc.PlayMarch();
                    if (currentState != "marching")
                    {
                        globalAudio.Stop();
                        globalAudio.clip = march;
                        globalAudio.Play();
                        currentState = "marching";
                    }
                }
                else 
                {
                    if (currentState != "idle")
                    npc.PlayIdle();
                    {
                        globalAudio.Stop();
                        globalAudio.clip = idle;
                        globalAudio.Play();
                        currentState = "idle";
                    }
                }
            }
        }
    }
}