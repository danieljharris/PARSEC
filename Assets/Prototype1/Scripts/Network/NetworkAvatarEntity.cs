using UnityEngine;
using Oculus.Avatar2;

public class NetworkAvatarEntity : OvrAvatarEntity
{
    [SerializeField] public bool isLocal = true;
    [SerializeField] public string avatarZipID = "1";
    [SerializeField] private OvrAvatarBodyTrackingBehavior bodyTracking;
    
    void Start()
    {
        LoadAvatarFromZip(avatarZipID);
        if(isLocal) LocalStart();
        else RemoteStart();
    }

    private void LoadAvatarFromZip(string id)
    {
        string assetPostfix = "_"
            + OvrAvatarManager.Instance.GetPlatformGLBPostfix(true)
            + OvrAvatarManager.Instance.GetPlatformGLBVersion(_creationInfo.renderFilters.highQualityFlags != CAPI.ovrAvatar2EntityHighQualityFlags.None, true)
            + OvrAvatarManager.Instance.GetPlatformGLBExtension(true);

        var path = new string[1];
        path[0] = id + assetPostfix;
        LoadAssetsFromZipSource(path);
    }

    private void LocalStart()
    {
        SetIsLocal(true);
        // ActiveView = CAPI.ovrAvatar2EntityViewFlags.FirstPerson;
        SetActiveView(CAPI.ovrAvatar2EntityViewFlags.FirstPerson);
        _creationInfo.features = CAPI.ovrAvatar2EntityFeatures.Preset_Default;
        SampleInputManager sampleInputManager = OvrAvatarManager.Instance.gameObject.GetComponent<SampleInputManager>();
        SetBodyTracking(bodyTracking);
    }
    private void RemoteStart()
    {
        SetIsLocal(false);
        // ActiveView = CAPI.ovrAvatar2EntityViewFlags.ThirdPerson;
        SetActiveView(CAPI.ovrAvatar2EntityViewFlags.ThirdPerson);
        _creationInfo.features = CAPI.ovrAvatar2EntityFeatures.Preset_Remote;
    }
}