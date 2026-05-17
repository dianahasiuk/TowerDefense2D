using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    public GameObject menuPanel;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            menuPanel.SetActive(false);
            GameManager.Instance.SetState(GameState.Preparation);
        });
    }
}