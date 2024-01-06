using System;

public interface IDragObject 
{
    void OnStartDrag();
    void OnDrag();
    Action<bool> OnEndDrag { get; set; }
}
