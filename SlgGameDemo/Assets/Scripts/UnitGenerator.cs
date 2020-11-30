using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace slg
{
    public class UnitGenerator : MonoBehaviour
    {
        private static UnitGenerator instance;
        public static UnitGenerator Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new UnitGenerator();
                    instance.Init();
                }
                return instance;
            }
        }
        private bool inited = false;
        [SerializeField] Unit unitModel;
        [SerializeField] private Transform unitsRoot;
        BattleField bf;
        private void Init()
        {
            if (inited) return;
            inited = true;
            Debug.Log("UnitGenerator inited.");
        }
        List<Unit> unitList;

        //list->generate->
        public void Generate()//（属性，位置）
        {
            if (bf.gridUnits == null) return;
            GridUnit gu = bf.gridUnits[0, 0];

            UnitData ud = new UnitData();
            ud.unitType = UnitType.Player;
            ud.unitState = UnitState.Wait;
            ud.unitPosition.x = 1;
            ud.unitPosition.y = 1;
            ud.atk = 100;
            ud.def = 50;
            ud.hp = 1500;
            ud.spd = 10;
            ud.moverange = 4;



            Unit unit = CreateUnit(gu);//todo 100,200,1500,10,4
            if (unit != null)
            {
                unit.name = string.Format("unit_{0}", unitList.Count);
                unit.unitData = ud;
                unit.Refresh();
                unit.gameObject.SetActive(true);
            }

        }

        //todo
        private Unit CreateUnit(GridUnit grid)
        {
            if (unitList == null)
                unitList = new List<Unit>();

            for (int i = 0; i < unitList.Count; ++i)
            {
                if (!unitList[i].gameObject.activeSelf)
                    return unitList[i];
            }

            var unit = Instantiate<Unit>(unitModel);
            unit.transform.SetParent(grid.transform);//到生成格子
            unit.transform.localPosition = Vector3.zero;
            unit.transform.localScale = Vector3.one;
            unit.transform.localRotation = Quaternion.identity;

            unitList.Add(unit);

            return unit;
        }
    }
}