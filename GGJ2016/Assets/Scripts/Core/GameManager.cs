using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class GameManager : MonoBehaviour {

    private static Dictionary<System.Type, Component> objectContainer = new Dictionary<System.Type, Component>();
    private static bool awaked, started = false;

    private static bool isDebugMode;

    private static bool sceneChanged = false;

    public static bool IsPaused = false;

    public static bool DebugMode {
        get { return isDebugMode; }
        set { isDebugMode = value; }
    }

    void Awake() {
        awaked = true;
    }

    void Start() {
        started = true;
    }


    public static T getBean<T>() where T : Component {
        return (T)getBean(typeof(T));
    }

    private static Component getBean(string componentName) {
        return getBean(System.Type.GetType(componentName));
    }

    public static void WaitTime(float time, Action callback){
        GameManager instance = GameManager.getInstance();
        instance.StartCoroutine(KeepWaiting(time, callback));
    }

    private static IEnumerator KeepWaiting(float time, Action callback) {
        yield return new WaitForSeconds(time);
        callback();
    }

    private static Component getBean(string objectPath, System.Type componentName) {
        if (!awaked) {
            throw new System.Exception("Game Manager has not awaked and someone has invoked get bean.. Ensure GameManager is the First Initialized Object on Hierarchy");
        }
        if (objectContainer.ContainsKey(componentName)) {
            return objectContainer[componentName];
        }
        if (!started) {
            throw new System.Exception("Game Manager cant Guarantee if Object Exists while in awakeingn.. since other objects stills have to Awake");
        }
        GameObject instance = GameObject.Find(objectPath);
        objectContainer[componentName] = instance.GetComponent(componentName);
        return objectContainer[componentName];
    }

    private static Component getBean(System.Type compType) {
        if (!awaked) {
            throw new System.Exception("Game Manager has not awaked and someone has invoked get bean.. Ensure GameManager is the First Initialized Object on Hierarchy");
        }

        if (objectContainer.ContainsKey(compType)) {
            return objectContainer[compType];
        }

        if (!started) {
            throw new System.Exception("Game Manager cant Guarantee if Object Exists while in awakeingn.. since other objects stills have to Awake. \n Try Obtain your reference in Start()");
        }

        objectContainer[compType] = (Component)GameObject.FindObjectOfType(compType);
        return objectContainer[compType];
    }

    private void OnDestroy() {
        objectContainer.Clear();
        GameManager.IsPaused = false;
    }

    public static GameManager getInstance() {
        return getBean<GameManager>();
    }

}