using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace slg
{
    public class Unit : MonoBehaviour
    {
        //[SerializeField] private Animator animator;
        public UnitData unitData;

        //todo 动画
        public void Refresh()
        {
            switch (unitData.unitState)
            {
                case UnitState.Wait:
                    //
                    break;
                case UnitState.Attack:
                    //
                    break;
                case UnitState.Defend:
                    //
                    break;
                default:
                    //
                    break;
            }
        }
    }
}