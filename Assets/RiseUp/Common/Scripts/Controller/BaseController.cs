using JetBrains.Annotations;
using Superpow;
using UnityEngine;
using UnityEngine.UI;

public class BaseController : MonoBehaviour
{
    public GameObject gameMaster;
    public string sceneName;
    public Music.Type music = Music.Type.None;
    protected int numofEnterScene;
    public Sprite offSoundSprite;
    public Sprite onSoundSprite;
    public Button btnMusic;
    protected virtual void Awake()
    {
        if (GameMaster.instance == null && gameMaster != null)
            Instantiate(gameMaster);

        iTween.dimensionMode = CommonConst.ITWEEN_MODE;
        CPlayerPrefs.useRijndael(CommonConst.ENCRYPTION_PREFS);

        numofEnterScene = CUtils.IncreaseNumofEnterScene(sceneName);
        btnMusic.onClick.AddListener(OnChangeMusic);
    }

    protected virtual void Start()
    {
        music = (Music.Type)Utils.GetMusic();
        CPlayerPrefs.Save();
        if (music == Music.Type.MainMusic)
        {
            btnMusic.gameObject.GetComponent<Image>().sprite = onSoundSprite;
        }
        else
        {
            btnMusic.gameObject.GetComponent<Image>().sprite = offSoundSprite;
        }
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

    public void OnChangeMusic()
    {
        if (music == Music.Type.MainMusic)
        {
            music = Music.Type.None;
            btnMusic.gameObject.GetComponent<Image>().sprite = offSoundSprite;
        }
        else
        {
            music = Music.Type.MainMusic;
            btnMusic.gameObject.GetComponent<Image>().sprite = onSoundSprite;
        }
        CPlayerPrefs.Save();
        Music.instance.Play(music);
        Utils.SetMusic((int)music);
    }

}
