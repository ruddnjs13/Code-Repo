using Entities;
using UnityEngine;

namespace Entities
{
    public class EntityRenderer : MonoBehaviour, IEntityComponent
    {
        [SerializeField] private Animator animator;
        
        private Entity _entity;
        public void Initialize(Entity entity)
        {
            _entity = entity;
        }
        
        public void SetParam(int hash, float value) => animator.SetFloat(hash, value);
        public void SetParam(int hash, bool value) => animator.SetBool(hash, value);
        public void SetParam(int hash, int  value) => animator.SetInteger(hash, value);
        public void SetParam(int hash) => animator.SetTrigger(hash);
    }
}