using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace GameLinker.Models
{
    [Serializable]
    public class GamesLibrary
    {
        private List<Game> library;
        public GamesLibrary()
        {
            library = new List<Game>();
        }

        public void AddGame(Game item)
        {
            library.Add(item);
        }

        public void RemoveGame(Game item)
        {
            library.Remove(item);
        }

        public string CheckGameDataExistance(string savePath, string dataPath, string name)
        {
            if (library.Where(game => game.DataPath == dataPath && game.DataPath != "").Count() > 0) return "La ruta de datos seleccionada ya está en uso para otro juego.";
            if (library.Where(game => game.SavePath == savePath && game.SavePath != "").Count() > 0) return "La ruta de archivos de guardado seleccionada ya está en uso para otro juego.";
            if (library.Where(game => game.GameName.ToUpper() == name.ToUpper()).Count() > 0) return "Ya existe un juego en la biblioteca con el nombre proporcionado.";
            return "";
        }
    }
}
