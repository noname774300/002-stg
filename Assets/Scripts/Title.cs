#nullable enable
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public void OnTouched() => SceneManager.LoadScene("BattleScene");
}
