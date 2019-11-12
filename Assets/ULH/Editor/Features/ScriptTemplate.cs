using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEditor;

namespace LittleHelper
{
    public class ScriptTemplate : AssetPostprocessor
    {
        private static FileSystemWatcher watcher;

        public static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            foreach (var asset in importedAssets)
            {
                if (asset.EndsWith(".cs") == false)
                    continue;

                var text = File.ReadAllText(asset);

                if (text.Contains("// Start is called before the first frame update") == false)
                    return;
                if (text.Contains("// Update is called once per frame") == false)
                    return;
                if (text.Split('\n').Length != 19)
                    return;

                var template = FindTemplate(asset);
                if (template != null)
                {
                    template = template.Replace("{{NAME}}", Path.GetFileNameWithoutExtension(asset));
                    File.WriteAllText(asset, template);
                }
            }
        }

        private static string FindTemplate(string path)
        {
            var p = Path.GetDirectoryName(path);

            while (true)
            {
                var templatePath = Path.Combine(p, "template.tcs");
                if (File.Exists(templatePath))
                    return File.ReadAllText(templatePath);

                var di = Directory.GetParent(p);
                if (di.Exists == false)
                    break;

                p = di.FullName;

                if (p.EndsWith(Path.DirectorySeparatorChar + "Assets"))
                    break;
            }

            return null;
        }
    }
}
