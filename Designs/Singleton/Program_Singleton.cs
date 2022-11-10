using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singleton
{
    /*A SIngleton is used to: 
      - ensure that a class has only one instance 
      - provide a global access point to that instance
      
      Examples where a Singleton can be used: database, files */

    public sealed class Singleton //the singleton should always cointain the 'sealed' modifier to prevent inheritance
    {
        //variable that stores the singleton instance
        //must be static
        private static Singleton database_instance;

        //private constructor to prevent direct constructor calls with the 'new' operator
        private Singleton()
        {
            //connect to database server
        } 


        //this method controls whether a new instance is created or not
        public static Singleton GetInstance()
        {
            if (database_instance == null)
            {
                database_instance = new Singleton();
            }

            return database_instance;
        }


        //any method which can be executed on the singleton's instance
        public void Query(string sql_statement)
        {
            //some logic
        }
    }

    class Program_Singleton
    {
        static void Main(string[] args)
        {
            Singleton s1 = Singleton.GetInstance();
            Singleton s2 = Singleton.GetInstance();

            if (s1 == s2)
                Console.WriteLine("Singleton successul. Both variables contain the same instance.");

            else
                Console.WriteLine("Singleton unsuccessful. The two variables have different instances.");

            Console.ReadKey();
        }

    }
}
