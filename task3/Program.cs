using System;
using System.Threading;

namespace SingletonTask
{
    public sealed class Authenticator
    {
        private static Authenticator _instance;
        private static readonly object _lock = new object();

        private Authenticator()
        {
            Console.WriteLine("System: Authenticator instance created.");
        }

        public static Authenticator Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new Authenticator();
                        }
                    }
                }
                return _instance;
            }
        }

        public void AuthenticateUser(string username)
        {
            Console.WriteLine($"User '{username}' is authenticated via instance: {this.GetHashCode()}");
        }
    }

    class Program
    {
        static void Main()
        {
            Console.WriteLine("Authentication System");
            Console.WriteLine(new string('=', 30));

            Thread thread1 = new Thread(() =>
            {
                Authenticator auth1 = Authenticator.Instance;
                auth1.AuthenticateUser("Bohdan");
            });

            Thread thread2 = new Thread(() =>
            {
                Authenticator auth2 = Authenticator.Instance;
                auth2.AuthenticateUser("Admin");
            });

            thread1.Start();
            thread2.Start();

            thread1.Join();
            thread2.Join();

            Authenticator test1 = Authenticator.Instance;
            Authenticator test2 = Authenticator.Instance;

            Console.WriteLine(new string('-', 30));
            Console.WriteLine($"test1 and test2 refer to the exact same object in memory: {ReferenceEquals(test1, test2)}");
        }
    }
}