using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace slg
{
    public enum GridType
    {
        Normal,
        Obstacle,
        Selected,
        Inrange
    }
    public class GridUnitData
    {
        //格子类型
        public GridType gridType;
        //格子所在行列
        public Vector2Int gridPosition;
        //格子所在空间位置
        public Vector3 localPosition;

        public int row;
        public int col;

        //计算两格子之间的距离
        public int Distance(GridUnitData target)
        {
            //计算行移动量
            int rowGap = Mathf.Abs(target.gridPosition.x - gridPosition.x);
            //计算列移动量
            int colGap = Mathf.Abs(target.gridPosition.y - gridPosition.y);

            return rowGap + colGap;
        }
    }
}
