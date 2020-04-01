using DapperDino.Tutorials.Lobby.Inputs;
using System.Collections.Generic;
using UnityEngine;

namespace DapperDino.Tutorials.Lobby
{
    public class InputManager : MonoBehaviour
    {
        private static readonly IDictionary<string, int> mapStates = new Dictionary<string, int>();

        private static Controls controls;
        public static Controls Controls
        {
            get
            {
                if (controls != null) { return controls; }
                return controls = new Controls();
            }
        }

        private void Awake()
        {
            if (controls != null) { return; }
            controls = new Controls();
        }

        private void OnEnable() => Controls.Enable();
        private void OnDisable() => Controls.Disable();
        private void OnDestroy() => controls = null;

        public static void Add(string mapName)
        {
            mapStates.TryGetValue(mapName, out int value);
            mapStates[mapName] = value + 1;

            UpdateMapState(mapName);
        }

        public static void Remove(string mapName)
        {
            mapStates.TryGetValue(mapName, out int value);
            mapStates[mapName] = Mathf.Max(value - 1, 0);

            UpdateMapState(mapName);
        }

        private static void UpdateMapState(string mapName)
        {
            int value = mapStates[mapName];

            if (value > 0)
            {
                Controls.asset.FindActionMap(mapName).Disable();

                return;
            }

            Controls.asset.FindActionMap(mapName).Enable();
        }
    }
}
