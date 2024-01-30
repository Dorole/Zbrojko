using System;
using System.Collections;
using UnityEngine;

public class NumberAnimator : MonoBehaviour
{
    [SerializeField] private RuntimeAnimatorController _moveController;
    [SerializeField] private RuntimeAnimatorController _positionsController;

    [SerializeField] private string _idleAnimation = "idle";
    [SerializeField] private string _dragAnimation = "tension";
    [SerializeField] private string _victoryAnimation = "victory";
    [SerializeField] private float _celebrationPeriod = 3f; //should be the same as other stuff in victory

    private Action _idleDelegate; //
    private Action _draggingDelegate;
    private Action _victoryDelegate;
    private DragObject _dragObject;
    private Animator _animator;
    private Coroutine _currentAnimationCoroutine;
    private AnimationState _currentState;
    private int _randomInt;


    // ************************************ UNITY CALLBACKS ************************************
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _animator.applyRootMotion = false;

        _dragObject = GetComponent<DragObject>();

        _idleDelegate = () => ChangeState(AnimationState.IDLE);
        _draggingDelegate = () => ChangeState(AnimationState.DRAGGING);
        _victoryDelegate = () => ChangeState(AnimationState.VICTORY);
    }

    private void OnEnable()
    {
        ResetAnimator();
        
        _dragObject.OnObjectPicked += _draggingDelegate;
        MathTeacher.OnCalculationEqual += _victoryDelegate;
    }

    private void Update()
    {
        _animator.SetInteger("randomN", _randomInt);
    }

    private void OnDisable()
    {
        StopAllCoroutines();

        _dragObject.OnObjectPicked -= _draggingDelegate;
        MathTeacher.OnCalculationEqual -= _victoryDelegate;
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

    private void ResetAnimator()
    {
        if (_animator.runtimeAnimatorController != _positionsController)
            _animator.runtimeAnimatorController = _positionsController;

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

    private void PlayExitAnimation()
    {
        //switch animator
        //switch state
        //trebalo bi raditi s ItemObjectMover :-/
    }
}

public enum AnimationState
{
    IDLE,
    VICTORY,
    DRAGGING,
    EXIT
}