using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace slg
{
    public class BattleCreator : MonoBehaviour
    {
        private static BattleCreator instance;
        public static BattleCreator Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BattleCreator();
                    instance.Init();
                }
                return instance;
            }
        }

        private bool inited = false;

        private void Init()
        {
            if (inited) return;
            inited = true;
            Debug.Log("Battle creator inited.");
        }

        //创建一场战斗
        public BattleData CreateBattle()
        {
            BattleData bd = new BattleData();
            bd.Generate(10, 8, 10, 2);
            return bd;
        }
    }
}

