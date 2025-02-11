﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;
using static GameEnums;

[RequireComponent(typeof(ARTrackedImageManager))]
public class TrackedImageInfo : MonoBehaviour
{
    [SerializeField] GameObject[] _placeablePrefabs;

    Dictionary<string, GameObject> _dictSpawnPrefabs;
    ARTrackedImageManager _trackedImageManager;

    private void Awake()
    {
        _dictSpawnPrefabs = new Dictionary<string, GameObject>();
        _trackedImageManager = GetComponent<ARTrackedImageManager>();
        EventsManager.Subscribe(EventID.OnTrackedImageSuccess, RemovePrefab);

        foreach (var prefab in _placeablePrefabs)
        {
            GameObject newPrefab = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            newPrefab.name = prefab.name;
            _dictSpawnPrefabs.Add(prefab.name, newPrefab);
            newPrefab.SetActive(false);
            Debug.Log("name: " + prefab.name);
        }
    }

    private void OnDestroy()
    {
        EventsManager.Unsubscribe(EventID.OnTrackedImageSuccess, RemovePrefab);
    }

    private void RemovePrefab(object obj)
    {
        Question questInfo = obj as Question;
        Destroy(_dictSpawnPrefabs[questInfo.ImageName]);
        _dictSpawnPrefabs.Remove(questInfo.ImageName);
        //Debug.Log("remove: " +  questInfo.ImageName);
    }

    private void OnEnable()
    {
        _trackedImageManager.trackedImagesChanged += ImageChanged;
    }

    private void OnDisable()
    {
        _trackedImageManager.trackedImagesChanged -= ImageChanged;
    }

    private void ImageChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (ARTrackedImage trackImage in eventArgs.added)
        {
            UpdateImage(trackImage);
        }

        foreach (ARTrackedImage trackImage in eventArgs.updated)
        {
            UpdateImage(trackImage);
        }

        foreach (ARTrackedImage trackImage in eventArgs.removed)
        {
            _dictSpawnPrefabs[trackImage.name].SetActive(false);
            //Debug.Log("active false, image name: " + trackImage.name);
        }
    }

    private void UpdateImage(ARTrackedImage trackImage)
    {
        string name = trackImage.referenceImage.name;
        Vector3 position = trackImage.transform.position;

        if (name == null)
            Debug.Log("name null");
        if (_dictSpawnPrefabs.ContainsKey(name))
        {
            Question quest = QuestManager.Instance.ListQuest.Find(x => x.ImageName == name);
            if (!quest)
            {
                Debug.Log("quest image " + name + " at round " + QuestManager.Instance.CurrentRound + " get null");
            }
            if (quest && QuestManager.Instance.CurrentRound == quest.Round)
            {
                GameObject prefab = _dictSpawnPrefabs[name];
                prefab.transform.position = position;
                prefab.SetActive(QuestManager.Instance.IsRestRound ? false : true);

                if (!quest) Debug.Log("Question of image: " + name + " get null");
                else EventsManager.Notify(EventID.OnReceiveQuestInfo, quest);

                //Debug.Log("active true: " + prefab);

                foreach (GameObject go in _dictSpawnPrefabs.Values)
                {
                    if (go.name != name && go.activeSelf)
                    {
                        go.SetActive(false);
                        //Debug.Log("active false, goName, name: " + go.name + ", " + name);
                    }
                }
            }
        }
    }
}
