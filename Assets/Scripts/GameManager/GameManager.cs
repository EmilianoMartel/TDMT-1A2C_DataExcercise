using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scenary;
using DataSource;
using EventChannel;

namespace GameManager
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private string playId = "Play";
        [SerializeField] private string exitId = "Exit";
        [Header("Levels")]
        [SerializeField] private List<LevelsContainer> levels;
        [Header("Data Sources")]
        [SerializeField] private GameManagerDataSource gameManagerDataSource;
        [SerializeField] private DataSource<SceneryManager> sceneryManagerDataSource;
        [Header("Event Channels")]
        [SerializeField] private EmptyAction _endLevel;
        [SerializeField] private BoolChannel _finalGame;
        private int _currentLevel = 0;

        private void OnEnable()
        {
            if (gameManagerDataSource != null)
                gameManagerDataSource.DataInstance = this;
            if (_endLevel != null)
                _endLevel.Sucription(HandleNextLevel);
        }

        private void OnDisable()
        {
            if (gameManagerDataSource != null && gameManagerDataSource.DataInstance == this)
            {
                gameManagerDataSource.DataInstance = null;
            }
            if (_endLevel != null) 
                _endLevel.Unsuscribe(HandleNextLevel);
        }

        public bool HandleSpecialEvents(string id)
        {
            if (id == playId)
            {
                _currentLevel = 0;
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
            _currentLevel++;
        }

        private void HandleNextLevel()
        {
            if (_currentLevel >= levels.Count)
            {
                _finalGame?.InvokeEvent(true);
                return;
            }

            GameStart();
        }
    }
}
