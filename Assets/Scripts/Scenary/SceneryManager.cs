using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneryManager : MonoBehaviour
{
    [SerializeField] private DataSource<SceneryManager> _sceneryManagerDataSource;
    [SerializeField] private List<LevelContainer> _defaultLevel;
    private List<Level> _currentLevel;

    public event Action onLoading = delegate { };
    /// <summary>
    /// The float given is always between 0 and 1
    /// </summary>
    public event Action<float> onLoadPercentage = delegate { };
    public event Action onLoaded = delegate { };

    private void OnEnable()
    {
        if (_sceneryManagerDataSource != null)
            _sceneryManagerDataSource.DataInstance = this;
    }

    private void Start()
    {
        //Load default level
        StartCoroutine(LoadFirstLevel(LevelContainerConverter(_defaultLevel)));
    }

    private void OnDisable()
    {
        if (_sceneryManagerDataSource != null && _sceneryManagerDataSource.DataInstance == this)
        {
            _sceneryManagerDataSource.DataInstance = null;
        }
    }

    public void ChangeLevel(List<Level> level)
    {
        StartCoroutine(ChangeLevel(_currentLevel, level));
    }

    private IEnumerator ChangeLevel(List<Level> currentLevel, List<Level> newLevel)
    {
        onLoading?.Invoke();
        onLoadPercentage?.Invoke(0);
        var unloadCount = 0;
        
        for (int i = 0; i < currentLevel.Count; i++)
        {
            if (currentLevel[i].IsUnloadable)
                unloadCount++;
        }

        var loadCount = newLevel.Count;
        var total = unloadCount + loadCount;
        yield return new WaitForSeconds(2);
        yield return Unload(currentLevel,
            currentIndex => onLoadPercentage?.Invoke((float)currentIndex / total));
        yield return new WaitForSeconds(2);
        yield return Load(newLevel,
            currentIndex => onLoadPercentage?.Invoke((float)(currentIndex + unloadCount) / total));
        yield return new WaitForSeconds(2);

        _currentLevel = newLevel;
        onLoaded?.Invoke();
    }

    private IEnumerator LoadFirstLevel(List<Level> level)
    {
        //This is a cheating value, do not use in production!
        var addedWeight = 5;

        onLoading?.Invoke();
        var total = level.Count + addedWeight;
        var current = 0;
        yield return Load(level,
            currentIndex => onLoadPercentage((float)currentIndex / total));

        //This is cheating so the screen is shown over a lot of time :)
        for (; current <= total; current++)
        {
            yield return new WaitForSeconds(1);
            onLoadPercentage?.Invoke((float)current / total);
        }
        _currentLevel = level;
        onLoaded?.Invoke();
    }

    private IEnumerator Load(List<Level> level, Action<int> onLoadedSceneQtyChanged)
    {
        var current = 0;
        foreach (var sceneName in level)
        {
            var loadOp = SceneManager.LoadSceneAsync(sceneName.SceneName, LoadSceneMode.Additive);
            yield return new WaitUntil(() => loadOp.isDone);
            current++;
            onLoadedSceneQtyChanged(current);
        }
    }

    private IEnumerator Unload(List<Level> level, Action<int> onUnloadedSceneQtyChanged)
    {
        var current = 0;
        foreach (var sceneName in level)
        {
            if (!sceneName.IsUnloadable)
                continue;

            var loadOp = SceneManager.UnloadSceneAsync(sceneName.SceneName);
            yield return new WaitUntil(() => loadOp.isDone);
            current++;
            onUnloadedSceneQtyChanged(current);
        }
    }

    private List<Level> LevelContainerConverter(List<LevelContainer> container)
    {
        List<Level> levels = new();
        for (int i = 0; i < container.Count; i++)
        {
            levels.Add(container[i].level);
        }

        return levels;
    }

    private void ValidateReference()
    {
        if (!_sceneryManagerDataSource)
        {
            Debug.LogError($"{name}: Data source is null.\nCheck and assigned one.\nDisabling component.");
            enabled = false;
            return;
        }
        if (_defaultLevel == null)
        {
            Debug.LogError($"{name}: Default Level is null.\nCheck and assigned one.\nDisabling component.");
            enabled = false;
            return;
        }
    }
}