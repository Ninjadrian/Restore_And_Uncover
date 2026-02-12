//using UnityEngine;
//using System.Collections.Generic;
//using System;

//public class Messages : MonoBehaviour
//{
//    public static Messages Instance;

//    public event Action OnChanged;

//    private readonly List<string> _messages = new();
//    private int unreadCount = 0;

//    private void Awake()
//    {
//        if (Instance != null && Instance != this)
//        {
//            Destroy(gameObject);
//            return;
//        }
//        Instance = this;
//        DontDestroyOnLoad(gameObject);
//    }

//    public void Push(string message)
//    {
//        _messages.Add(message);
//        unreadCount++;

//        OnChanged?.Invoke();
//    }

//    public IReadOnlyList<string> GetAll()
//    {
//        return _messages;
//    }

//    public string GetLatestOrEmpty()
//    {
//        if (_messages.Count == 0) return "";
//        return _messages[_messages.Count - 1];
//    }

//    public int GetUnreadCount()
//    {
//        return unreadCount;
//    }

//    public void MarkAllAsRead()
//    {
//        unreadCount = 0;
//        OnChanged?.Invoke();
//    }
//}
