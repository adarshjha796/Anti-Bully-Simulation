using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scene3Manager : MonoBehaviour
{
    public static Scene3Manager Instance;

    [Header("Image")]
    public Image PlayerHealthBar;
    public Image EnemyHealthBar;

    [Header("RawImage")]
    public RawImage Belt;
    [SerializeField] private RawImage PlayerImage;

    [Header("List")]
    public List<Sprite> BeltList;
    [SerializeField] private List<Sprite> PlayerImageList;
    public List<Sprite> Labels;


    private void Awake()
    {
        if(Instance != null) 
        {
            Destroy(gameObject);
        }

        else
        {
            Instance = this;
        }
    }


    private void Update()
    {
        for(var i = 0; i < PlayerImageList.Count; i++) 
        {
            if (LoadPlayer.Instance.SelectedPlayer == PlayerImageList[i].texture.name)  
            {
                PlayerImage.texture = PlayerImageList[i].texture;
                break;
            }
        }
    }
}