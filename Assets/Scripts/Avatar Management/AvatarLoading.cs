using UnityEngine;
using System.Collections.Generic;
using System.Threading.Tasks;
using ReadyPlayerMe.AvatarCreator;
using ReadyPlayerMe.Core;
using UnityEngine.Events;

namespace AvatarManagement
{
    /// <summary>
    /// This class is a simple <see cref="Monobehaviour"/>  to serve as an example on how to load Ready Player Me avatars and spawn as a <see cref="GameObject"/> into the scene.
    /// </summary>
    public class AvatarLoading : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Set this to the URL or shortcode of the Ready Player Me Avatar you want to load.")]
        private string avatarUrl = "https://models.readyplayer.me/638df693d72bffc6fa17943c.glb";

        [SerializeField] private Vector3 customPosition = new Vector3(1.4f, 1.8f, 2.3f);
        [SerializeField] private Vector3 customRotation = new Vector3(0, 180, 0);
        [SerializeField] private BodyType bodyType = BodyType.FullBody;
        [SerializeField] private GameObject createRPMAccount;
        private OutfitGender gender = OutfitGender.Masculine;
        private AvatarManager avatarManager;
        public UnityEvent<AvatarProperties> onAvatarCreated;
        private GameObject avatar;

        public static GameObject CurrentCharacter { get; set; }
    
        // This method is called after the character is instantiated
        public void SetCurrentCharacter(GameObject avatar)
        {
            CurrentCharacter = avatar;
        }

        private void Start()
        {
            // ApplicationData.Log();
            // Use the URL from the static class
            // string avatarUrl = AvatarData.AvatarUrl;
            LoadAvatar();
        }

        private void LoadAvatar()
        {
            // Destroy the existing avatar if it exists
            if (avatar != null)
            {
                Destroy(avatar);
            }
            if (!string.IsNullOrEmpty(AvatarData.AvatarUrl))
            {
                avatarUrl = AvatarData.AvatarUrl;
            }
            if (!string.IsNullOrEmpty(avatarUrl))
            {
                var avatarLoader = new AvatarObjectLoader();
                avatarLoader.OnCompleted += (_, args) =>
                {
                    avatar = args.Avatar;
                    avatar.transform.position = customPosition;
                    avatar.transform.rotation = Quaternion.Euler(customRotation);
                    // AvatarAnimatorHelper.SetupAnimator(args.Metadata.BodyType, avatar);
                    SetCurrentCharacter(avatar);
                };
                avatarLoader.LoadAvatar(avatarUrl);
                
            }
        }

        // public async void LoadAvatar()
        // {
        //     avatarUrl = AvatarData.AvatarUrl; 
        //     // loading.SetActive(true);
        //     var newAvatar = await avatarManager.GetAvatar(avatarUrl, BodyType.FullBody);
        //     // Destroy the old avatar and replace it with the new one.
        //     if (avatar != null)
        //     {
        //         Destroy(avatar);
        //     }
        //     avatar = newAvatar;
        //     var avatarProperties = await avatarManager.GetAvatarProperties(avatarUrl);

        //     // if (avatarProperties.Gender != previousGender)
        //     // {
        //     //     LoadAssets();
        //     // }

        //     SetupAvatar();

        //     onAvatarCreated?.Invoke(avatarProperties);
        // }

        // /// <summary>
        // /// Sets additional elements and components on the created avatar, such as mouse rotation and animation controller.
        // /// </summary>
        // private void SetupAvatar()
        // {
        //     avatar.AddComponent<MouseRotationHandler>();
        //     avatar.AddComponent<AvatarRotator>();
        //     var animator = avatar.GetComponent<Animator>();
        //     AvatarAnimationHelper.SetupAnimator(new AvatarMetadata() { BodyType = bodyType, OutfitGender = gender }, animator);
        //     animator.runtimeAnimatorController = animationController;
        // }

        
    }
}
