using CustomInput;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    [SerializeField] private AudioClipRefsSO audioRefs;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        Player.Instance.OnPick += Player_OnPick;
        BaseCounter.OnDrop += BaseCounter_OnDrop;
        TrashCounter.OnAnyTrashed += TrashCounter_OnAnyTrashed;
    }

    private void TrashCounter_OnAnyTrashed(object sender, System.EventArgs e)
    {
        PlaySound(audioRefs.trash, Camera.main.transform.position);
    }

    private void BaseCounter_OnDrop(object sender, System.EventArgs e)
    {
        PlaySound(audioRefs.itemDroped, Camera.main.transform.position);
    }

    private void Player_OnPick(object sender, System.EventArgs e)
    {
        PlaySound(audioRefs.itemPicked, Camera.main.transform.position);
    }

    private void CuttingCounter_OnAnyCut(object sender, System.EventArgs e)
    {
        PlaySound(audioRefs.chop, Camera.main.transform.position);
    }

    private void DeliveryManager_OnRecipeFailed(object sender, System.EventArgs e)
    {
        PlaySound(audioRefs.deliverFailed, Camera.main.transform.position);
    }

    private void DeliveryManager_OnRecipeSuccess(object sender, System.EventArgs e)
    {
        PlaySound(audioRefs.deliverSuccess, Camera.main.transform.position);
    }

    private void PlaySound(AudioClip[] audioClips, Vector3 point, float volume = .5f)
    {
        AudioSource.PlayClipAtPoint(audioClips[Random.Range(0, audioClips.Length)], point, volume);
    }

    public void PlayFootstep(Vector3 point, float volume = .5f)
    {
        PlaySound(audioRefs.footstep, Camera.main.transform.position, volume);
    }
}
