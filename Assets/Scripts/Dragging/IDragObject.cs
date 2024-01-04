using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDragObject 
{
    void OnStartDrag();
    void OnDrag();
    Action<bool> OnEndDrag { get; set; }
}
