using UnityEditor;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace PSDUIImporter
{
    //------------------------------------------------------------------------------
    // class definition
    //------------------------------------------------------------------------------
    public class PSDImportMenu : Editor
    {

        [MenuItem("PSD2UGUI/PSDImport ...")]
        static public void ImportHogSceneMenuItem()
        {
			string inputFile = EditorUtility.OpenFilePanel("Choose PSDUI File to Import", PSDImporterConst.SELECT_XML_PATH, "xml");
            if ((inputFile != null) && (inputFile != "") && (inputFile.StartsWith(Application.dataPath)))
            {
                PSDImportCtrl import = new PSDUIImporter.PSDImportCtrl(inputFile);
                import.BeginDrawUILayers();
//                import.BeginSetUIParents();
            }
            GC.Collect();
        }
    }
}
