using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerVisualCounter : MonoBehaviour
{
    private Animator _animator;
    [SerializeField] private ContainerCounter _counter;
    private const string OPEN_CLOSE = "OpenClose";
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    private void Start()
    {
        _counter.OnGetKitchenObject += _counter_OnGetKitchenObject;
    }

    private void _counter_OnGetKitchenObject(object sender, System.EventArgs e)
    {
        _animator.SetTrigger(OPEN_CLOSE);
    }
}
