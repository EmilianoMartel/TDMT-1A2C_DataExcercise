using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scenary;
using Core.DataSource;

namespace GameManager
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private string playId = "Play";
        [SerializeField] private string exitId = "Exit";
        [Header("Levels")]
        [SerializeField] private List<LevelsContainer> levels;
        [Header("Data Sources")]
        [SerializeField] private DataSource<GameManager> gameManagerDataSource;
        [SerializeField] private DataSource<SceneryManager> sceneryManagerDataSource;


        private int _currentLevel = 0;

        private void OnEnable()
        {
            if (gameManagerDataSource != null)
                gameManagerDataSource.DataInstance = this;
        }

        private void OnDisable()
        {
            if (gameManagerDataSource != null && gameManagerDataSource.DataInstance == this)
            {
                gameManagerDataSource.DataInstance = null;
            }
        }

        public bool HandleSpecialEvents(string id)
        {
            if (id == playId)
            {
                GameStart();
                return true;
            }
            else if (id == exitId)
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
                Application.Quit();
                return true;
            }
            return false;
        }

        private void GameStart()
        {
            if (sceneryManagerDataSource != null && sceneryManagerDataSource.DataInstance != null)
            {
                sceneryManagerDataSource.DataInstance.ChangeLevel(levels[_currentLevel].levels);
            }
        }
    }
}
