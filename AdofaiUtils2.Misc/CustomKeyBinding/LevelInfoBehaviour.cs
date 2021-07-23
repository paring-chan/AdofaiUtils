using ADOFAI;
using UnityEngine;

namespace AdofaiUtils2.Misc.CustomKeyBinding
{
    public class LevelInfoBehaviour : MonoBehaviour
    {
        private LevelDataCLS map;
        private string levelId;
        private static GUIStyle buttonStyle;
        private Vector2 _scroll;

        public void SetMap(LevelDataCLS _map, string id)
        {
            map = _map;
            levelId = id;
        }

        private void OnGUI()
        {
            if (map == null)
            {
                return;
            }

            if (buttonStyle == null)
            {
                buttonStyle = GUI.skin.button;
                buttonStyle.stretchWidth = false;
            }

            GUILayout.BeginArea(new Rect(Screen.width / 2 - 200, Screen.height / 2 - 100, 400, 200), "맵 정보",
                GUI.skin.window);
            
            _scroll = GUILayout.BeginScrollView(_scroll);

            var items = new[]
            {
                new[]
                {
                    "제목",
                    RDUtils.RemoveRichTags(map.artist + " - " + map.song)
                },
                new[]
                {
                    "제작자",
                    RDUtils.RemoveRichTags(map.author)
                },
                new[]
                {
                    "다운로드 링크",
                    "https://steamcommunity.com/sharedfiles/filedetails/?id=" + levelId
                }
            };
            
            foreach (var item in items)
            {
                GUILayout.Label(item[0]);
                GUILayout.BeginHorizontal();
                GUILayout.TextArea(item[1]);
                if (GUILayout.Button("복사", buttonStyle))
                {
                    GUIUtility.systemCopyBuffer = item[1];
                }
            
                GUILayout.EndHorizontal();
            }

            GUILayout.EndScrollView();

            GUILayout.EndArea();
        }
    }
}