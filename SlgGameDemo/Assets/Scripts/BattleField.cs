using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
namespace slg
{

    public class BattleField : MonoBehaviour
    {
        private static BattleField instance;
        public static BattleField Instance
        {
            get
            {
                return instance;
            }
        }
        private BattleData currentData;

        [SerializeField] GridUnit gridUnitModel;
        [SerializeField] private Transform gridUnitsRoot;

        public GridUnit[,] gridUnits;

        List<GridUnit> gridPool;

        public void LoadBattleData(BattleData battleData)
        {
            if (currentData != null) UnloadBattleData();
            currentData = battleData;
            PrepareBattleMap();
        }
        void Generate(int width, int height, int obstacle, int gap)
        {
            if (width <= 0 || height <= 0) return;


        }
        void PrepareBattleMap()
        {
            if (currentData == null)
            {
                Debug.Log("prepare battle map failed:no battle data");
                return;
            }
            gridUnits = new GridUnit[currentData.mapWidth, currentData.mapHeight];

            for (int row = 0; row < currentData.mapHeight; row++)
            {
                for (int column = 0; column < currentData.mapWidth; column++)
                {
                    GridUnitData gud = currentData.mapGrids[column, row];
                    gud.col = column;
                    gud.row = row;
                    if (gud != null)
                    {
                        GridUnit gu = CreateGrid();//创建一个用于显示的格子对象
                        if (gu != null)
                        {
                            gridUnits[column, row] = gu;
                            gu.transform.localPosition = gud.localPosition;
                            gu.name = string.Format("Grid_{0}_{1}", row, column);
                            gu.gridData = gud;
                            gu.Refresh();
                            gu.gameObject.SetActive(true);
                        }
                    }
                }
            }

        }
        void UnloadBattleData()
        {
            RecycleAllGrids();
            currentData = null;
        }

        //创建格子
        private GridUnit CreateGrid()
        {
            if (gridPool == null)
                gridPool = new List<GridUnit>();

            for (int i = 0; i < gridPool.Count; ++i)
            {
                if (!gridPool[i].gameObject.activeSelf)
                    return gridPool[i];
            }

            var gu = Instantiate<GridUnit>(gridUnitModel);
            gu.transform.SetParent(gridUnitsRoot);
            gu.transform.localPosition = Vector3.zero;
            gu.transform.localScale = Vector3.one;
            gu.transform.localRotation = Quaternion.identity;

            gridPool.Add(gu);

            return gu;
        }
        //回收所有格子
        private void RecycleAllGrids()
        {
            if (gridPool == null)
                return;

            for (int i = 0; i < gridPool.Count; ++i)
            {
                gridPool[i].transform.localPosition = Vector3.zero;
                gridPool[i].name = "UNUSED";
                gridPool[i].gameObject.SetActive(false);
            }

            gridUnits = null;
        }
        private void Awake()
        {
            instance = this;
        }
    }


}