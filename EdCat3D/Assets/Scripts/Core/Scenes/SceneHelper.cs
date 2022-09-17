using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.Scenes
{

    public enum SceneType
    {
        Load,
        Worldmap
    }

    public static class SceneHelper
    {
        public static void LoadScene(SceneType scene)
        {
            SceneManager.LoadSceneAsync((int)scene);
        }
    }
}