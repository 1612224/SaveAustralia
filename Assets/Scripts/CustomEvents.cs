﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class TileTouchEvent : UnityEvent<GameTile> { }

[System.Serializable]
public class IntEvent : UnityEvent<int> { }
