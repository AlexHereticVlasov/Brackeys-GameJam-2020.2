using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Opening : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(LoadGame());   
    }

    private IEnumerator LoadGame()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(1);
    }
}
