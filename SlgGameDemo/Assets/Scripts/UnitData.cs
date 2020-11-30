using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace slg
{
    public enum UnitType
    {
        Player,
        Enemy,
        Boss
    }
    public enum UnitState
    {
        Wait,
        Attack,
        Defend
    }
    public class UnitData
    {
        //单位类型
        public UnitType unitType;
        //单位状态
        public UnitState unitState;
        //单位所在行列
        public Vector2Int unitPosition;
        //单位所在空间位置
        public Vector3 localPosition;
        //
        public int atk;
        public int def;
        public int hp;
        public int spd;
        public int moverange;
        //
        //计算两单位之间的距离
        public int Distance(UnitData target)
        {
            //计算行移动量
            int rowGap = Mathf.Abs(target.unitPosition.x - unitPosition.x);
            //计算列移动量
            int colGap = Mathf.Abs(target.unitPosition.y - unitPosition.y);

            return rowGap + colGap;
        }
    }
}