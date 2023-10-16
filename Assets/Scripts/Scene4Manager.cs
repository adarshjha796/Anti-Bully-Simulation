using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene4Manager : MonoBehaviour
{
    public static Scene4Manager Instance;

    [Header("List")]
    public List<SpriteRenderer> belt;
    public List<SpriteRenderer> bully;
    public List<GameObject> beltColliders;
    public List<AudioClip> bullySounds;

    [Header("AudioSource")]
    public AudioSource bullyAudioSource;


    [Header("int")]
    public int count = 1;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }

        else
        {
            Instance = this;
        }
    }
}