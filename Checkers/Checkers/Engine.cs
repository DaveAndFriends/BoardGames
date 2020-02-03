using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BoardGames.Checkers
{
    class Engine
    {
        public static Queue<string> ParseMovesFile(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException();

            var result = new Queue<string>();
            string[] lines = File.ReadAllLines(path);

            foreach(string line in lines)
            {
                result.Enqueue(line);
            }

            return result;
        }
    }
}
