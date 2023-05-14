using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void NextScreen()
    {
        animator.SetTrigger("Next");
    }

    public void PreviousScreen()
    {
        animator.SetTrigger("Back");
    }

    private void Return()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
