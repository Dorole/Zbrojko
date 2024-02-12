using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ItemObjectMover))]
public class NumberAnimator : MonoBehaviour
{
    [SerializeField] private RuntimeAnimatorController _numberAnimatorController; 

    [SerializeField] private string _idleAnimation = "idle";
    [SerializeField] private string _dragAnimation = "tension";
    [SerializeField] private string _victoryAnimation = "victory";
    [SerializeField] private string _exitAnimation = "exit";
    [SerializeField] private float _celebrationPeriod = 3f; //should be the same as other stuff in victory
    //MathTeacher aktivira neki state/bool i to traje dok je na vagi balans i toliki je i celebrationPeriod?

    private Animator _animator;
    private AnimationState _currentState;
    private Action _idleDelegate; 
    private Action _draggingDelegate;
    private Action _victoryDelegate;
    private Action _exitDelegate;
    private Coroutine _currentAnimationCoroutine;
    private DragObject _dragObject;
    private ItemObjectMover _itemObjectMover;
    private int _randomInt;

    // ************************************ UNITY CALLBACKS ************************************
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _animator.applyRootMotion = false;
        _animator.runtimeAnimatorController = _numberAnimatorController;

        _dragObject = GetComponent<DragObject>();
        _itemObjectMover = GetComponent<ItemObjectMover>();

        _idleDelegate = () => ChangeState(AnimationState.IDLE);
        _draggingDelegate = () => ChangeState(AnimationState.DRAGGING);
        _victoryDelegate = () => ChangeState(AnimationState.VICTORY);
        _exitDelegate = () => ChangeState(AnimationState.EXIT); 
    }

    private void OnEnable()
    {
        ResetState();
        
        _dragObject.OnObjectPicked += _draggingDelegate;
        _dragObject.OnObjectDropped += _idleDelegate;
        MathTeacher.OnCalculationEqual += _victoryDelegate;
        _itemObjectMover.OnObjectDropped += _exitDelegate;
    }

    private void Update()
    {
        _animator.SetInteger("randomN", _randomInt);
    }

    private void OnDisable()
    {
        StopAllCoroutines();

        _dragObject.OnObjectPicked -= _draggingDelegate;
        _dragObject.OnObjectDropped -= _idleDelegate;
        MathTeacher.OnCalculationEqual -= _victoryDelegate;
        _itemObjectMover.OnObjectDropped -= _exitDelegate;
    }

    // ************************************ PRIVATE FUNCTIONS ************************************
    private void ChangeState(AnimationState newState)
    {
        if (_currentState == newState) return;

        StopCurrentAnimation();
        _currentState = newState;

        switch (_currentState)
        {
            case AnimationState.IDLE:
                _currentAnimationCoroutine = StartCoroutine(CO_PlayIdleAnimation());
                break;
            case AnimationState.DRAGGING:
                _currentAnimationCoroutine = StartCoroutine(CO_PlayDragAnimation());
                break;
            case AnimationState.VICTORY:
                _currentAnimationCoroutine = StartCoroutine(CO_PlayVictoryAnimation());
                break;
            case AnimationState.EXIT:
                _currentAnimationCoroutine = StartCoroutine(CO_PlayExitAnimation());
                break;
            default:
                break;
        }
    }

    private void StopCurrentAnimation()
    {
        if (_currentAnimationCoroutine != null)
        {
            StopCoroutine(_currentAnimationCoroutine);
            _currentAnimationCoroutine = null;
        }
    }

    private void GetRandomAnimation(int max)
    {
        int currentInt = _randomInt;

        do
        {
            _randomInt = UnityEngine.Random.Range(0, max);
        }
        while (_randomInt == currentInt);
    }

    private void PlayRandomAnimation(string animation, int max)
    {
        GetRandomAnimation(max);
        _animator.Play(animation);
    }

    private void ResetState()
    {
        _currentState = AnimationState.IDLE;
        _currentAnimationCoroutine = StartCoroutine(CO_PlayIdleAnimation());
    }

    private IEnumerator CO_PlayIdleAnimation()
    {
        PlayRandomAnimation(_idleAnimation, 10);

        while (_currentState == AnimationState.IDLE)
        {
            yield return new WaitForSeconds(2f);
            GetRandomAnimation(10);
        }        
    }

    private IEnumerator CO_PlayDragAnimation()
    {
        PlayRandomAnimation(_dragAnimation, 4);

        while (_currentState == AnimationState.DRAGGING)
        {
            yield return new WaitForSeconds(2f);
            GetRandomAnimation(4);
        }
    }

    private IEnumerator CO_PlayVictoryAnimation()
    {
        PlayRandomAnimation(_victoryAnimation, 5);

        float timer = 0;
        while (timer < _celebrationPeriod) //beware, some anim end up in a pose - looks weird if longer time
        {
            timer += Time.deltaTime;
            yield return null;
        }

        ChangeState(AnimationState.IDLE);
    }

    private IEnumerator CO_PlayExitAnimation()
    {
        // Randomly sets the shouldTrip to true or false
        _animator.SetBool("shouldTrip", Random.Range(0, 2) == 1);

        GetRandomAnimation(10);
        _animator.Play(_exitAnimation);

        yield return null;
    }

}

public enum AnimationState
{
    IDLE,
    VICTORY,
    DRAGGING,
    EXIT
}