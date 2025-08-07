using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartButton : MonoBehaviour
{
    public void OnStart()
    {
        SceneManager.LoadScene(1);
    }
}
