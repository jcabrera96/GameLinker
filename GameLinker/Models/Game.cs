using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLinker
{


    class Game
    {
        private string savePath, dataPath;

        public Game(string savePath = null, string dataPath = null)
        {
            if (savePath == null && dataPath == null)
            {
                throw new ArgumentNullException();
            }
            this.savePath = savePath;
            this.dataPath = dataPath;
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
    }
}
