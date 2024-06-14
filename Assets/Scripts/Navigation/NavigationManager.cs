using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NavigationManager : MonoBehaviour
{
    [Tooltip("First menu in the list is the default one :)")]
    [SerializeField] private List<MenuDataSource> menusWithId;

    //[SerializeField] private DataSource<GameManager> gameManagerDataSource;
    private int _currentMenuIndex = 0;

    private void Start()
    {
        foreach (var menu in menusWithId)
        {
            menu.DataInstance.Setup();
            menu.DataInstance.OnChangeMenu += HandleChangeMenu;
            menu.DataInstance.gameObject.SetActive(false);
        }

        if (menusWithId.Count > 0)
        {
            menusWithId[_currentMenuIndex].DataInstance.gameObject.SetActive(true);
        }
    }

    private void HandleChangeMenu(string id)
    {
        //if (idsToTellGameManager.Contains(id) && gameManagerDataSource != null && gameManagerDataSource.Value != null)
        //{
        //    gameManagerDataSource.Value.HandleSpecialEvents(id);
        //}
        for (var i = 0; i < menusWithId.Count; i++)
        {
            var menuWithId = menusWithId[i];
            if (menuWithId.menuId == id)
            {
                menusWithId[_currentMenuIndex].DataInstance.gameObject.SetActive(false);
                menuWithId.DataInstance.gameObject.SetActive(true);
                _currentMenuIndex = i;
                break;
            }
        }
    }
}