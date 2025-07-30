using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class SerializebleAction : UnityEvent { }
[Serializable]
public class GameObjectUnityEvent : UnityEvent<GameObject> {}
