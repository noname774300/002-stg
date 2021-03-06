#nullable enable

using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class BattleScene : MonoBehaviour
{
    public Input? Input => input;
    [SerializeReference] private Input? input;
    public Bullet? BulletPrefab => bulletPrefab;
    [SerializeField] private Bullet? bulletPrefab;
    [SerializeField] private Enemy? enemyPrefab;
    [SerializeField] private Canvas? canvas;
    [SerializeField] private Image? hpBar;
    [SerializeField] private TextMeshProUGUI? hpText;
    [SerializeField] private TextMeshProUGUI? weaponText;
    [SerializeField] private TextMeshProUGUI? nextTargetAttributeText;
    [SerializeField] private TextMeshProUGUI? triggerText;
    [SerializeField] private Image? lockOnFramePrefab;
    [SerializeField] private DynamicJoystick? moveJoystick;
    [SerializeField] private TextMeshProUGUI? missionIsOverText;
    [SerializeField] private Tilemap? tilemap;
    [SerializeField] private TileBase? wallTile;
    public bool MissionIsOver { get; private set; }
    private Player? player;
    private Image[]? lockOnFrames;

    protected void Start()
    {
        input = new Input(moveJoystick!);
        player = FindObjectOfType<Player>();
        lockOnFrames = new Image[] { Instantiate(lockOnFramePrefab!) };
        foreach (var lockOnFrame in lockOnFrames)
        {
            lockOnFrame.transform.SetParent(canvas!.transform);
        }
        Instantiate(enemyPrefab!, new Vector3(-3, 7, 0), Quaternion.identity).Initialize(
            maxHp: 10,
            movingForce: 5);
        Instantiate(enemyPrefab!, new Vector3(0, 7, 0), Quaternion.identity).Initialize(
            maxHp: 20,
            movingForce: 5);
        Instantiate(enemyPrefab!, new Vector3(3, 7, 0), Quaternion.identity).Initialize(
            maxHp: 30,
            movingForce: 5);
        InitializeTilemap();
    }

    private void InitializeTilemap()
    {
        var width = 200;
        var height = 200;
        var relief = 3f;
        var seedX = 0f;
        var seedY = 0f;
        var halfWidth = width / 2;
        var halfHeight = height / 2;
        tilemap!.ClearAllTiles();
        for (var x = 0; x < width; x++)
        {
            for (var y = 0; y < height; y++)
            {
                var noise = Mathf.PerlinNoise(
                    (x + seedX) * relief / 100f,
                    (y + seedY) * relief / 100f);
                Debug.Log($"{x}, {y}, {noise}");
                tilemap.SetTile(
                    new Vector3Int(x - halfWidth, y - halfHeight, 0),
                    noise > 0.5 ? wallTile : null);
            }
        }
    }

    protected void Update()
    {
        Input!.Update();
        UpdateUI();
        GoToNextSceneOrNot();
    }

    private void UpdateUI()
    {
        hpBar!.fillAmount = (float)player!.Hp / player.MaxHp;
        hpText!.text = player.Hp + "/" + player.MaxHp;
        var loadedWeapon = player.WeaponsHolder!.GetLoadedWeapon();
        if (loadedWeapon == null)
        {
            weaponText!.text = "-";
            nextTargetAttributeText!.text = "-";
            triggerText!.text = "-";
            foreach (var lockOnFrame in lockOnFrames!)
            {
                lockOnFrame.gameObject.SetActive(false);
            }
        }
        else
        {
            weaponText!.text = loadedWeapon.Name;
            nextTargetAttributeText!.text = loadedWeapon.TargetingMode;
            triggerText!.text = player.Triggered ? "?????????" : "?????????";
            var targets = loadedWeapon.Targets;
            for (var i = 0; i < lockOnFrames!.Length; i++)
            {
                var lockOnFrame = lockOnFrames[i];
                if (i >= targets.Count)
                {
                    lockOnFrame.gameObject.SetActive(false);
                    continue;
                }
                var target = targets[i];
                if (target == null)
                {
                    lockOnFrame.gameObject.SetActive(false);
                    continue;
                }
                lockOnFrame.gameObject.SetActive(true);
                var targetPositionOnScreen = Camera.main.WorldToScreenPoint(target.transform.position);
                _ = RectTransformUtility.ScreenPointToLocalPointInRectangle(
                   lockOnFrame.transform.parent.GetComponent<RectTransform>(),
                   targetPositionOnScreen,
                   null,
                   out var lockOnFramePosition);
                lockOnFrame.rectTransform.localPosition = lockOnFramePosition;
            }
        }
    }

    private void GoToNextSceneOrNot()
    {
        if (MissionIsOver)
        {
            return;
        }
        if (MissionHasBeenCleared())
        {
            MissionIsOver = true;
            missionIsOverText!.gameObject.SetActive(true);
            missionIsOverText.text = "Mission Cleared";
            _ = StartCoroutine(LoadTitleSceneAfter(seconds: 5));
        }
        if (MissionHasBeenFailed())
        {
            MissionIsOver = true;
            missionIsOverText!.gameObject.SetActive(true);
            missionIsOverText.text = "Mission Failed";
            _ = StartCoroutine(LoadTitleSceneAfter(seconds: 5));
        }
    }

    private bool MissionHasBeenCleared()
    {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        return enemies.Length == 0;
    }

    private bool MissionHasBeenFailed() => player!.Hp == 0;

    private IEnumerator LoadTitleSceneAfter(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene("TitleScene");
    }
}
