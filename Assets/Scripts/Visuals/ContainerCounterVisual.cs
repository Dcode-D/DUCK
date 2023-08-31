using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerVisualCounter : MonoBehaviour
{
    private Animator _animator;
    [SerializeField] private ContainerCounter _counter;
    [SerializeField] private GameObject objectSprite;
    private const string OPEN_CLOSE = "OpenClose";
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    private void Start()
    {
        _counter.OnGetKitchenObject += _counter_OnGetKitchenObject;
        Sprite sprite = _counter.GetKitchenObjectSOSprite();
        objectSprite.GetComponent<SpriteRenderer>().sprite = sprite;
    }


    private void _counter_OnGetKitchenObject(object sender, System.EventArgs e)
    {
        _animator.SetTrigger(OPEN_CLOSE);
    }
}
