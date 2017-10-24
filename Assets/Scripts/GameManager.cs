using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public GameObject StarterLight;
    public CameraFollow CameraFollow;
    public GameObject PlayerPrefab;
    public GameObject IndicatorMarker;
    public List<GameObject> LevelDataList;
    public PlayerController PlayerController;

    // Use this for initialization
    void Awake () {
        StarterLight.SetActive(false);
        QualitySettings.vSyncCount = 0;  // VSync must be disabled
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Start()
    {
        StartGame();
    }
    
    void StartGame()
    {
        LevelData lvl = Instantiate(LevelDataList[0]).GetComponent<LevelData>();


        GameObject player = TransformUtils.InstantiateAndAdd(PlayerPrefab, lvl.transform);
        GameObject indicator = TransformUtils.InstantiateAndAdd(IndicatorMarker, lvl.transform);


        PlayerController = player.GetComponent<PlayerController>();



        PlayerController.SpawnPoint = lvl.PlayerSpawnPosition.transform.position;



        CameraFollow.target = PlayerController;
        PlayerController.Indicator = indicator.GetComponent<IndicatorMarker>();

        PlayerController.Reset();
    }

    public void ChangeBulletTypeStandard()
    {
        PlayerController.SetBulletType(ShootType.STANDARD);
    }
    public void ChangeBulletTypeTeleporter()
    {
        PlayerController.SetBulletType(ShootType.TELEPORTER);
    }

    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
