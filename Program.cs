using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complexify_CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            Complexify C = new Complexify(8, 0.5f, BanMode.strict);
            Int32 complexity = 0;

            while(true)
            {
                Console.WriteLine("Enter password to test :");
                var InputString = Console.ReadLine();

                var IsValid = C.EvaluateSecurity(InputString, out complexity);
                if (IsValid) Console.WriteLine(String.Format("Password matches criterias with a complexity of {0}", complexity));
                else Console.WriteLine(String.Format("Password does not match criterias with a complexity of only {0}", complexity));
            }

        }
    }
}
