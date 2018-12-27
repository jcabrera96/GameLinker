using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLinker
{
    [Serializable]
    class Game
    {
        private string savePath, dataPath, gameName;

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

        public override string ToString()
        {
            return $"Data Path: {(dataPath == "" ? "- None -" : dataPath)}" +
                   $"{Environment.NewLine}Saves Path: {(savePath == "" ? "- None -" : savePath)}" +
                   $"{Environment.NewLine}Game Name: {(gameName == "" ? "- None -" : gameName)}";
        }
    }
}
