using Core;
using Core.Events;
using Core.Property;
using Game.Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneStartBehaviourTest : MonoBehaviour
{

    [SerializeField] private CharacterPropertyDescription _characterPropertyDescription;
    [SerializeField] private CharacterControllerView _characterControllerView;

    private Core.UI.GameScreenView _gameScreenView;
    private Core.CharacterController _characterController;
    private int damage;
    
    private void Start()
    {
        _characterController = new Core.CharacterController(_characterPropertyDescription);
        _characterControllerView.SetCharacterController(_characterController);
        _gameScreenView.SetHPMaxValue(_characterController.MaxHealth);
        _gameScreenView.BindHealthPointProperty(_characterController.Health);
    }


    [ContextMenu("Deal Damage")]
    private void TestDealDemege()
    {
        var damage = UnityEngine.Random.Range(5, 30);
        _characterController.DealDamage(damage);
    }
}
