using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Core
{
    public class CharacterControllerView : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private Animator _animator;

        private CharacterController _characterController;

        public int damage = 5;
        private void Start()
        {
            
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
               
                if (Physics.Raycast(ray, out hit))
                {
                    _agent.SetDestination(hit.point);
                    _animator.SetBool("run", true);
                }
                else  
                {
                    _animator.SetBool("run", false);
                    _animator.SetTrigger("idle");
                }
                                                                     
            }

        }

        public void SetCharacterController(CharacterController characterController)
        {
            _characterController = characterController;
        }

        public void OnCollisionEnter(Collision collision)
        {
            //_characterController.IntractionWithCollider(collision);
        }

        public void OnTriggerEnter(Collider collider)
        {
            //_characterController.InteractionWithTrigger(collider);
        }


       
    }
}

