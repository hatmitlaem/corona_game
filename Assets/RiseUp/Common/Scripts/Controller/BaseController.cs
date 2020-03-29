using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using SimpleJSON;

public class BaseController : MonoBehaviour {
    public GameObject gameMaster;
    public string sceneName;
    public Music.Type music = Music.Type.None;
    protected int numofEnterScene;

    protected virtual void Awake()
    {
        if (GameMaster.instance == null && gameMaster != null)
            Instantiate(gameMaster);
        
        iTween.dimensionMode = CommonConst.ITWEEN_MODE;
        CPlayerPrefs.useRijndael(CommonConst.ENCRYPTION_PREFS);

        numofEnterScene = CUtils.IncreaseNumofEnterScene(sceneName);
    }

    protected virtual void Start()
    {
        CPlayerPrefs.Save();
        Music.instance.Play(music);

    }

    public virtual void OnApplicationPause(bool pause)
    {
        Debug.Log("On Application Pause");
        CPlayerPrefs.Save();
        if (pause == false)
        {
            Timer.Schedule(this, 0.2f, () =>
            {
            });
        }
    }
}
