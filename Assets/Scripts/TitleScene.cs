#nullable enable
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
{
    public InputAction InputAction
    {
        get
        {
            if (inputAction == null)
            {
                inputAction = new InputAction(type: InputActionType.PassThrough, binding: "*/<Button>");
                inputAction.Enable();
            }
            return inputAction;
        }
    }
    private InputAction? inputAction;

    protected void Update()
    {
        if (InputAction.triggered)
        {
            LoadNextScene();
        }
    }

    public void LoadNextScene() => SceneManager.LoadScene("BattleScene");
}
