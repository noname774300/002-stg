#nullable enable
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleScene : MonoBehaviour
{
    public Input? Input => input;
    [SerializeReference] private Input? input;
    public Bullet? BulletPrefab => bulletPrefab;
    [SerializeField] private Bullet? bulletPrefab;
    [SerializeField] private Enemy? enemyPrefab;
    [SerializeField] private Image? hpBar;
    [SerializeField] private TextMeshProUGUI? hpText;
    [SerializeField] private TextMeshProUGUI? weaponText;
    [SerializeField] private TextMeshProUGUI? nextTargetAttributeText;
    [SerializeField] private TextMeshProUGUI? triggerText;
    [SerializeField] private Image? lockOnFrame;
    [SerializeField] private DynamicJoystick? moveJoystick;
    [SerializeField] private DynamicJoystick? lookJoystick;
    private Player? player;

    public void Start()
    {
        player = FindObjectOfType<Player>();
        input = new Input(moveJoystick!, lookJoystick!);
        Instantiate(enemyPrefab!, new Vector3(-3, 7, 0), Quaternion.identity).Initialize(
            maxHp: 10,
            movingForce: 5);
        Instantiate(enemyPrefab!, new Vector3(0, 7, 0), Quaternion.identity).Initialize(
            maxHp: 20,
            movingForce: 5);
        Instantiate(enemyPrefab!, new Vector3(3, 7, 0), Quaternion.identity).Initialize(
            maxHp: 30,
            movingForce: 5);
    }

    public void Update()
    {
        hpBar!.fillAmount = (float)player!.Hp / player.MaxHp;
        hpText!.text = player.Hp + " / " + player.MaxHp;
        var loadedWeapon = player.WeaponsHolder.GetLoadedWeapon();
        if (loadedWeapon == null)
        {
            weaponText!.text = "-";
            nextTargetAttributeText!.text = "-";
            triggerText!.text = "-";
            lockOnFrame!.gameObject.SetActive(false);
        }
        else
        {
            weaponText!.text = loadedWeapon.Name;
            nextTargetAttributeText!.text = loadedWeapon.TargetingMode;
            triggerText!.text = "トリガー";
            var targets = loadedWeapon.Targets;
            if (targets.Count == 0)
            {
                lockOnFrame!.gameObject.SetActive(false);
            }
            else
            {
                lockOnFrame!.gameObject.SetActive(true);
                var target = targets[0];
                if (target != null)
                {
                    var targetPositionOnScreen = Camera.main.WorldToScreenPoint(target.transform.position);
                    var _ = RectTransformUtility.ScreenPointToLocalPointInRectangle(
                        lockOnFrame.transform.parent.GetComponent<RectTransform>(),
                        targetPositionOnScreen,
                        null,
                        out var lockOnFramePosition);
                    lockOnFrame.rectTransform.localPosition = lockOnFramePosition;
                }
            }
        }
        Input!.Update();
    }
}
