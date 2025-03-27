using MelonLoader;
using UnityEngine;
using Il2CppInterop;
using Il2CppInterop.Runtime.Injection; 
using System.Collections;
using Il2Cpp;
using Il2CppTLD.Gear;

namespace TorchTweaker
{
    public class Main : MelonMod
    {
        public int layerMask = 0;
        public static RaycastHit hit;

        public override void OnInitializeMelon()
        {
            Debug.Log($"[{Info.Name}] Version {Info.Version} loaded!");
            Settings.OnLoad();
            layerMask |= 1 << 17;
        }

        public override void OnSceneWasInitialized(int buildIndex, string sceneName)
        {
            if (sceneName != "Boot" && sceneName != "Empty")
            {
                GearItem.LoadGearItemPrefab("GEAR_Torch").GetComponent<TorchItem>().m_BurnLifetimeMinutes = Settings.settings.burnTime;
            }
        }

        public override void OnUpdate()
        {
            if (InputManager.GetKeyDown(InputManager.m_CurrentContext, Settings.settings.interactButton))
            {
                
                if (Physics.Raycast(GameManager.GetMainCamera().transform.position, GameManager.GetMainCamera().transform.TransformDirection(Vector3.forward), out hit, 2.5f, layerMask))
                {
                    GameObject hitObject = hit.collider.gameObject;
                    string hitObjectName = hitObject.name;
                    
                    if (hitObjectName.StartsWith("GEAR_KeroseneLamp"))
                    {
                        KeroseneLampItem thisLamp;

                        thisLamp = hitObject.transform.GetComponent<KeroseneLampItem>();

                        if (thisLamp != null)
                        {
                            PlayerManager currentPlayerManager = GameManager.GetPlayerManagerComponent();

                            if (thisLamp.m_On)
                            {
                                if (currentPlayerManager.m_ItemInHands && currentPlayerManager.m_ItemInHands.m_TorchItem && !currentPlayerManager.m_ItemInHands.IsLitTorch())
                                {
                                    currentPlayerManager.m_ItemInHands.m_TorchItem.IgniteFromSource(thisLamp.gameObject);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}