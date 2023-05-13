using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void StartGame()
    {
        animator.SetTrigger("StartGame");
    }

    private void ChangeScenes(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
