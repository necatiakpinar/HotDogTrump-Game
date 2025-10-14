using System;
using Data.PersistentData;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        private async void Awake()
        {
            await PersistentDataManager.LoadSaveDataFromDisk();
        }
    }
}