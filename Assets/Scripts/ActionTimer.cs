using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionTimer
{
    private static List<ActionTimer> activeTimerList;
    private static GameObject initGameObject;

    private static void InitIfNeeded(){
        if (initGameObject == null){
            initGameObject = new GameObject("ActionTimer_InitGameObject");
            activeTimerList = new List<ActionTimer>();
        }
    }
    public static ActionTimer CreateInterval(Action action, float interval, string timerName = null){
        InitIfNeeded();
        List<Action> intervalActionList = new List<Action>();
        GameObject gameObject = new GameObject("Action Interval Timer", typeof(MonoBehaviourHook));
        ActionTimer actionTimer = new ActionTimer(action, interval, gameObject, timerName, intervalActionList);
        activeTimerList.Add(actionTimer);
        //Debug.Log(Time.time / interval);
       //actionTimer.PopulateIntervalList(interval);
        MonoBehaviourHook monoBehaviourHook = gameObject.GetComponent<MonoBehaviourHook>();
        actionTimer.AppendAction(interval);
        monoBehaviourHook.interval = interval;
        monoBehaviourHook.onIntervalUpdate = actionTimer.Update;

        for (int i = 0; i < intervalActionList.Count; i++)
        {
            Debug.Log(intervalActionList[i]);
        }

     
        return actionTimer;
    }
    public static ActionTimer Create(Action action, float timer, string timerName = null){  
        InitIfNeeded();
        GameObject gameObject = new GameObject("Action Timer", typeof(MonoBehaviourHook));
        ActionTimer actionTimer = new ActionTimer(action , timer, gameObject, timerName);
        gameObject.GetComponent<MonoBehaviourHook>().onUpdate = actionTimer.Update;

        activeTimerList.Add(actionTimer);

        return actionTimer;
    }
    private static void RemoveTimer(ActionTimer actionTimer){
        InitIfNeeded();
        activeTimerList.Remove(actionTimer);
    }
    private static void StopTimer(string timerName){
        for (int i = 0; i < activeTimerList.Count; i++){
            if (activeTimerList[i].timerName == timerName){
                activeTimerList[i].MarkComplete();
                i--;
            }
        }
    }
    private class MonoBehaviourHook : MonoBehaviour {
        public Action onUpdate;
        public Action<float> onIntervalUpdate;
        public float interval;
        private void Update(){
            if (onUpdate != null)
                onUpdate();
                if(interval != 0 && onIntervalUpdate != null){
                    onIntervalUpdate(this.interval);
                }          
        }
    }
    Action OnTimerComplete;
    bool isComplete;
    string timerName;
    List<Action> intervalActionList;
    float timer;
    private GameObject gameObject;

    private ActionTimer(Action action, float interval,  GameObject gameObject, string timerName = null, List<Action> intervalActionList = null){
        this.OnTimerComplete = action;
        this.timer = interval;
        this.timerName = timerName;
        this.isComplete = false;
        this.gameObject = gameObject;
        if(intervalActionList != null)
            this.intervalActionList = intervalActionList;
    }

    void Update(){
        if (!isComplete){
            timer -= Time.deltaTime;
            if (timer < 0){
                OnTimerComplete();
                MarkComplete();
            }
        }
    }
    void Update(float interval){
        if(intervalActionList != null && intervalActionList.Count != -1){
            for (int i = 0; i < intervalActionList.Count; i++)
            {
                intervalActionList[i].Invoke();
            }
        }
    } 
    private void AppendAction(float interval){
        if(!isComplete){
            float intervalTimer = interval -= Time.time;
            if (intervalTimer < 0){
                intervalActionList.Add(OnTimerComplete);
                intervalTimer = interval -= Time.time;
            }
            
        }
    }
    private void MarkComplete(){
        this.isComplete = true;
        UnityEngine.Object.Destroy(gameObject);
        RemoveTimer(this);
    }

}
