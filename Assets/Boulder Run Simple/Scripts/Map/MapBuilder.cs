using System.Linq;
using UnityEngine;
using BoulderRun.Parts;

namespace BoulderRun.Map
{
    public class MapBuilder : MonoBehaviour
    {
        [SerializeField]
        Texture2D m_MapImage = null;

        [SerializeField]
        PartInfo m_PlaceHolderPrefab = null;
        [SerializeField]
        PartInfo[] m_PartInfoPrefabs = null;

        void Awake()
        {
            for (int x = 0; x < m_MapImage.width; x++)
            {
                for (int y = 0; y < m_MapImage.height; y++)
                {
                    Color c = m_MapImage.GetPixel(x, y);
                    PartInfo prefab = m_PartInfoPrefabs.Where(m => m.partColor == c).FirstOrDefault();

                    if (prefab == null) prefab = m_PlaceHolderPrefab;

                    PartInfo partInfo = Instantiate(prefab);

                    partInfo.transform.position = new Vector3(x, 0.0f, y - 1);
                    partInfo.transform.rotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
                    partInfo.transform.SetParent(transform);
                }
            }
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, -20.0f);
        }
    }
} 

