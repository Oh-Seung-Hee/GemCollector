using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public CharacterInputAction InputAction {  get; private set; }

    public CharacterInputAction.PlayerActions PlayerActions { get; private set; }

    private void Awake()
    {
        InputAction = new CharacterInputAction();

        PlayerActions = InputAction.Player;
    }

    private void OnEnable()
    {
        InputAction.Player.Enable();
    }
    private void OnDisable()
    {
        InputAction.Player.Disable();
    }
}
