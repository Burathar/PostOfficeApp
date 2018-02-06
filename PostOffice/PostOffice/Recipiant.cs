using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostOffice
{
    public class Recipiant
    {
        public string Name { get; private set; }
        public int? Number { get; private set; }
        public int? Id { get; private set; }

        public Recipiant(string name, int? number, int? id = null)
        {
            if (name == "" && Number == null) throw new ArgumentException(@"Name is empty and Number is Null");
            Name = name;
            Number = number;
            Id = id;
        }

        public Recipiant(string name, int? id = null)
        {
            Name = name;
            if (name == "") throw new ArgumentException(@"string is empty.", nameof(name));
            Id = id;
        }

        public Recipiant(int number, int? id)
        {
            Number = number;
            Id = id;
        }

        public override string ToString()
        {
            string output = Name;
            if (Number != null)
            {
                if (output == "") output = Number.ToString();
                else
                {
                    output += ", ";
                    output += Number.ToString();
                }
            }
            if (output == "") output = "Error";
            return output;
        }
    }
}