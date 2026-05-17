using UnityEngine;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
            GameManager.Instance.RestartGame());
    }
}