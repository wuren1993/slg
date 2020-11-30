using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace slg
{
    public class GridUnit : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer tileRenderer;
        public GridUnitData gridData;
        
        public void Refresh()
        {
            switch (gridData.gridType)
            {
                case GridType.Normal:
                    tileRenderer.color = Color.white;
                    break;
                case GridType.Obstacle:
                    tileRenderer.color = Color.gray;
                    break;
                case GridType.Selected:
                    tileRenderer.color = Color.green;
                    break;
                case GridType.Inrange:
                    tileRenderer.color = Color.cyan;
                    break;
                default:
                    tileRenderer.color = Color.white;
                    break;
            }
        }
    }
}