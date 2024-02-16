using System;
using UnityEditor.Animations;
using UnityEngine;

public class DieRoller2D : MonoBehaviour
{
    public event Action<int> OnRoll;
    public int Result { get; private set; }
    
    [SerializeField] Vector2 _rollForceMin = new Vector2(0, 350);
    [SerializeField] Vector2 _rollForceMax = new Vector2(0, 450);
    [SerializeField] bool _usePhysics = true;
    [Tooltip("Roll time only applies when not using physics")]
    [SerializeField] float _rollTime = 2f;

    public void RollDie(int value = 0)
    {
        Result = value == 0 ? UnityEngine.Random.Range(1, ResultAnimations.Length) : value;
        if (_usePhysics)
        {
            RollWithPhysics();
        }
        else
        {
            RollWithoutPhysics();
        }
    }

    Rigidbody2D _rigidbody2D;
    Animator _animator;
    static readonly int RollingAnimation = Animator.StringToHash("Rolling");
    static readonly int[] ResultAnimations = new[]
    {
        Animator.StringToHash("LandOn1"), 
        Animator.StringToHash("LandOn2"),
        Animator.StringToHash("LandOn3"), 
        Animator.StringToHash("LandOn4"),
        Animator.StringToHash("LandOn5"),
        Animator.StringToHash("LandOn6")
    };

    bool _isRolling;
    float _timeRemaining;
    RandomAudioClipPlayer _audioClipPlayer;

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.isKinematic = true;
        _animator = GetComponent<Animator>();
        _audioClipPlayer = GetComponent<RandomAudioClipPlayer>();
    }

    void Update()
    {
        if (!_isRolling) return;
        _timeRemaining -= Time.deltaTime;
        if (_usePhysics || _timeRemaining > 0f) return;
        FinishRolling();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        _audioClipPlayer?.PlayRandomClip();
        if (_rigidbody2D.velocity.sqrMagnitude > 10f) return;
        FinishRolling();
    }

    void RollWithPhysics()
    {
        _rigidbody2D.isKinematic = false;
        _rigidbody2D.AddForce(GetRollForce());
        _animator.SetTrigger(RollingAnimation);
        _isRolling = true;
    }

    void RollWithoutPhysics()
    {
        _animator.SetTrigger(RollingAnimation);
        _isRolling = true;
        _timeRemaining = _rollTime;
    }
    
    void FinishRolling()
    {
        _isRolling = false;
        _rigidbody2D.isKinematic = true;
        _rigidbody2D.velocity = Vector2.zero;
        _animator.SetTrigger(ResultAnimations[Result - 1]);
        OnRoll?.Invoke(Result);
    }
    
    Vector2 GetRollForce()
    {
        return new Vector2(
            UnityEngine.Random.Range(_rollForceMin.x, _rollForceMax.x),
            UnityEngine.Random.Range(_rollForceMin.y, _rollForceMax.y));
    }
}
