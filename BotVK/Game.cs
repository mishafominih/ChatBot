using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotVK
{
    public class Game
    {
        private string answer;
        static List<string> cities;

        public Game()
        {
            cities = File.ReadAllLines("citi.txt").Select(x => x.ToLower()).ToList();
        }
        public string GetSiti(string previous)
        {
            if(answer != null)
                if (answer.Last() != previous.First() || !cities.Contains(previous))
                {
                    answer = null;
                    cities = File.ReadAllLines("citi.txt").Select(x => x.ToLower()).ToList();
                    return "ты проиграл, что бы начать заного введи наименование города";
                }
            cities.Remove(previous);
            answer = cities.Where(x => x.First() == previous.Last()).FirstOrDefault();
            if (answer == null)
            {
                cities = File.ReadAllLines("citi.txt").Select(x => x.ToLower()).ToList();
                return "ты выиграл! Введи название города, что бы начать заного";
            }
            cities.Remove(answer);
            return answer;
        }
    }
}
