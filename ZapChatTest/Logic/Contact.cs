using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZapChatTest.Logic
{
    public class Contact
    {
        public Contact()
        {
            _Name = Guid.NewGuid().ToString().Substring(0, 14);
        }

        public Contact(string Name)
        {
            _Name = Name;
        }

        public int Id { get; set; }
        private string _Name { get; set; }
        public string Name
        {
            get
            {
                return _Name;
            }

            private set
            {
                _Name = value;
            }
        }        
        private Random r = new Random();
        public string Talk
        {
            get
            { return Speech[r.Next(4)]; }
        }
        protected List<string> Speech = new List<string>()
        {
            "Привет!",
            "Как дела?",
            "Нормально...",
            "Electric funeral?"
        };
    }
}