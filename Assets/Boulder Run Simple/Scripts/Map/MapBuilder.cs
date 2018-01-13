using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace BoulderRun.Map
{
    public class MapBuilder : MonoBehaviour
    {
        public readonly static Dictionary<Color, PartType> s_ColorTypes = new Dictionary<Color, PartType>()
        {
           { Color.black, PartType.Flat },
           { Color.white, PartType.Hole },
           { new Color32(255, 255, 0, 255), PartType.Ramp },
           { Color.red, PartType.Wall },
           { Color.green, PartType.Ledge },
           { Color.blue, PartType.Goal }
        };

        [SerializeField]
        Texture2D m_MapImage = null;

        [SerializeField]
        PartInfo m_PlaceHolderPrefab = null;
        [SerializeField]
        PartInfo[] m_PartInfoPrefabs = null;

        void Awake()
        {

            Dictionary<PartType, PartInfo> prefabs = new Dictionary<PartType, PartInfo>()
            {
               { PartType.Flat, m_PartInfoPrefabs.Where(x => x.partType == PartType.Flat).FirstOrDefault() },
               { PartType.Hole, m_PartInfoPrefabs.Where(x => x.partType == PartType.Hole).FirstOrDefault() },
               { PartType.Ramp, m_PartInfoPrefabs.Where(x => x.partType == PartType.Ramp).FirstOrDefault() },
               { PartType.Wall, m_PartInfoPrefabs.Where(x => x.partType == PartType.Wall).FirstOrDefault() },
               { PartType.Ledge, m_PartInfoPrefabs.Where(x => x.partType == PartType.Ledge).FirstOrDefault() },
               { PartType.Goal, m_PartInfoPrefabs.Where(x => x.partType == PartType.Goal).FirstOrDefault() }
            };

            for (int x = 0; x < m_MapImage.width; x++)
            {
                for (int y = 0; y < m_MapImage.height; y++)
                {
                    Color c = m_MapImage.GetPixel(x, y);
                    PartType partType = s_ColorTypes[c];
                    PartInfo partInfo = null;
                    if (prefabs[partType])
                    {
                        partInfo = Instantiate(prefabs[partType]);
                    }
                    else
                    {
                        partInfo = Instantiate(m_PlaceHolderPrefab);
                    }
                    partInfo.transform.position = new Vector3(x, 0.0f, y - 1);
                    partInfo.transform.rotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
                    partInfo.transform.SetParent(transform);
                }
            }
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, -20.0f);
        }
    }
} 

