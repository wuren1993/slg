using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace slg
{
    public class BattleData
    {
        private static int mapCount = 0;

        Camera cam = Camera.main;

        //地图信息
        public int mapID;
        //地图宽高
        public int mapWidth = 0;
        public int mapHeight = 0;

        //地图格子信息
        public GridUnitData[,] mapGrids;
        List<GridUnitData> normalGrids;
        List<GridUnitData> obstacleGrids;

        //战场中铺设格子（信息）
        public void Generate(int width, int height, int obstacle, int gap)
        {
            if (width <= 0 || height <= 0)
                return;

            cam.transform.position = new Vector3((width - 1) / 2, height / 2, -10.0f);

            //地图编号自增
            mapID = mapCount++;
            //记录地图宽高
            mapWidth = width;
            mapHeight = height;
            //生成格子数组
            mapGrids = new GridUnitData[mapWidth, mapHeight];
            //记录普通格子和障碍格子
            normalGrids = new List<GridUnitData>();
            obstacleGrids = new List<GridUnitData>();

            //全部生成为普通格子
            for (int r = 0; r < mapHeight; ++r)
            {
                for (int c = 0; c < mapWidth; ++c)
                {
                    GridUnitData gud = new GridUnitData();
                    gud.localPosition = new Vector3(c, r, 0);

                    //设置格子参数
                    gud.gridPosition = new Vector2Int(r, c);     //位置
                    //todo sprite size
                    //float width = GetComponent<SpriteRenderer>().bounds.size.x;
                    //float height = GetComponent<SpriteRenderer>().bounds.size.y;


                    //初始设置为普通格子
                    SetGridType(gud, GridType.Normal);
                    //保存
                    mapGrids[c, r] = gud;
                }
            }

            //随机一些障碍格子
            DisposeGridUnits(obstacle, gap);
        }

        //设置格子类型
        private void SetGridType(GridUnitData gud, GridType gt)
        {
            switch (gt)
            {
                case GridType.Normal:
                    normalGrids.Add(gud);
                    break;
                case GridType.Obstacle:
                    obstacleGrids.Add(gud);
                    break;
                default:
                    break;
            }
            gud.gridType = gt;
        }

        //放置一些障碍格子
        private void DisposeGridUnits(int obstacle, int gap)
        {
            obstacle = Mathf.Min(mapWidth * mapHeight, obstacle);

            for (int i = 0; i < obstacle; ++i)
            {
                int randomIdx = -1;
                GridUnitData target = null;
                int tryTimes = 999;
                while (tryTimes > 0 && target == null)
                {
                    randomIdx = Random.Range(0, normalGrids.Count);
                    target = normalGrids[randomIdx];
                    //判断距离
                    for (int j = 0; j < obstacleGrids.Count; ++j)
                    {
                        var distance = obstacleGrids[j].Distance(target);
                        if (obstacleGrids[j].Distance(target) < gap)
                        {
                            target = null;
                            break;
                        }
                    }
                    --tryTimes;
                }
                if (target != null)
                {
                    SetGridType(target, GridType.Obstacle);
                    normalGrids.RemoveAt(randomIdx);
                }
                else
                {
                    Debug.Log("Dispose grid unit data warning.");
                }
            }
        }
    }
}
