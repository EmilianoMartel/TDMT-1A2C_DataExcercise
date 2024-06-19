using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using DataSource;
using GameManager;

namespace Navigation
{
    public class NavigationManager : MonoBehaviour
    {
        [Tooltip("First menu in the list is the default one :)")]
        [SerializeField] private List<MenuDataSource> menusWithId;

        [SerializeField] private GameManagerDataSource gameManagerDataSource;

        [SerializeField] private BoolChannel _finalGame;
        [SerializeField] private MenuDataSource _finalScreenId;

        private int _currentMenuIndex = 0;

        private void OnEnable()
        {
            _finalGame.Sucription(FinalScreen);
        }

        private void OnDisable()
        {
            _finalGame.Unsuscribe(FinalScreen);
        }

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
            if (gameManagerDataSource != null && gameManagerDataSource.DataInstance != null)
            {
                gameManagerDataSource.DataInstance.HandleSpecialEvents(id);
            }
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

        private void FinalScreen(bool win)
        {
            for (var i = 0; i < menusWithId.Count; i++)
            {
                var menuWithId = menusWithId[i];
                if (menuWithId.menuId == _finalScreenId.menuId)
                {
                    menusWithId[_currentMenuIndex].DataInstance.gameObject.SetActive(false);
                    menuWithId.DataInstance.gameObject.SetActive(true);
                    _currentMenuIndex = i;
                    break;
                }
            }
        }
    }
}