using GameLinker.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLinker
{
    [Serializable]
    public class Game
    {
        private string savePath, dataPath, gameName;
        private int dataSize, saveSize, dataParts, savesParts;

        public Game(string savePath = "", string dataPath = "", string gameName = "")
        {
            if (savePath == "" && dataPath == "")
            {
                throw new ArgumentNullException("No se pueden dejar vacias ambas rutas de archivos (saves y datos)", new ArgumentNullException());
            }
            else if (gameName == "")
            {
                throw new ArgumentNullException("No se puede dejar en blanco el nombre del juego", new ArgumentNullException());
            }
            else
            {
                string existanceError = LibraryHelper.Library.CheckGameDataExistance(savePath, dataPath, gameName);
                if(existanceError != "") throw new ArgumentNullException(existanceError, new ArgumentNullException());
            }
            this.savePath = savePath;
            this.dataPath = dataPath;
            this.gameName = gameName;
        }

        public string GameName
        {
            get => gameName;
            set => gameName = value;
        }

        public string SavePath
        {
            get => savePath;
            set => savePath = value;
        }

        public string DataPath
        {
            get => dataPath;
            set => dataPath = value;
        }

        public int DataSize
        {
            get => dataSize;
            set => dataSize = value;
        }

        public int SaveSize
        {
            get => saveSize;
            set => saveSize = value;
        }

        public int DataParts
        {
            get => dataParts;
            set => dataParts = value;
        }

        public int SavesParts
        {
            get => savesParts;
            set => savesParts = value;
        }

        public override string ToString()
        {
            return $"Data Path: {(dataPath == "" ? "- None -" : dataPath)}" +
                   $"{Environment.NewLine}Saves Path: {(savePath == "" ? "- None -" : savePath)}" +
                   $"{Environment.NewLine}Game Name: {gameName}" +
                   $"{Environment.NewLine}Data Size: {dataSize}" +
                   $"{Environment.NewLine}Saves Size: {saveSize}" +
                   $"{Environment.NewLine}Data Parts: {dataParts}" +
                   $"{Environment.NewLine}Saves Parts: {savesParts}";
        }
    }
}
