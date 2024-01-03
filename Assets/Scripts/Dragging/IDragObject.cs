using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDragObject 
{
    void OnStartDrag();
    void OnDrag();
    void OnEndDrag();
}
