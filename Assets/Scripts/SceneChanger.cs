using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private int SceneBuildIndex;
    private void OnMouseDown()
    {
        SceneManager.LoadScene(SceneBuildIndex);
    }
}