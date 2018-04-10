using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            ProcessDelegate1 pd1 = new ProcessDelegate1(new Program().PorgramMethod1);
            Console.WriteLine(pd1("test1 ", "test2"));
            ProcessDelegate<string, int> pd2 = new ProcessDelegate<string, int>(new Program().ProgramMethod2);
            Console.WriteLine(pd2("text", 3));
            //key word event
            Test t = new Test();
            t.ProcessEvent += t_ProcessEvent1;
            t.ProcessEvent += new ProcessDelegateEvent(t_ProcessEvent2);
            Console.WriteLine(000);
            Console.WriteLine(t.Process());
            //callback
            Testt t1 = new Testt();
            Testt2 t2 = new Testt2();
            string s1 = t1.Process("Text1", "Text2", t2.Process1);
            string s2 = t1.Process("Text1", "Text2", new ProcessDelegate3(t2.Process2));
            string s3 = t1.Process("Text1", "Text2", t2.Process3);
            Console.WriteLine(s1);
            Console.WriteLine(s2);
            Console.WriteLine(s3);
            Console.ReadKey();
        }
        //委托
        public delegate string ProcessDelegate1(string str1, string str2);
        private string PorgramMethod1(string str1, string str2)
        {
            return str1 + str2;
        }
        //泛型委托
        public delegate string ProcessDelegate<T, S>(T t, S s);
        private string ProgramMethod2(string str, int inte)
        {
            return str + inte;
        }
        //事件 关键字event
        private static void t_ProcessEvent1(object sender, EventArgs e)
        {
            Test t = (Test)sender;
            Console.WriteLine(2);
            t.Text1 = "t_ProcessEvent Text11.";
            t.Text2 = "t_ProcessEvent Text12.";
        }
        private static void t_ProcessEvent2(object sender, EventArgs e)
        {
            Test t = (Test)sender;
            Console.WriteLine(3);
            t.Text1 = "t_ProcessEvent Text21.";
            t.Text2 = "t_ProcessEvent Text22.";
        }
    }
    //事件 关键字event
    public delegate void ProcessDelegateEvent(object sender, EventArgs e);
    //事件 关键字event
    class Test
    {
        public string Text1 { get; set; }
        public string Text2 { get; set; }
        public event ProcessDelegateEvent ProcessEvent;
        void ProcessAction(object sender, EventArgs e)
        {
            //if (ProcessEvent == null)
            //{
            ProcessEvent += new ProcessDelegateEvent(t_ProcessEvent);
            //}
            Console.WriteLine(1);
            ProcessEvent(sender, e);
            Console.WriteLine(5);
        }
        void t_ProcessEvent(object sender, EventArgs e)
        {
            //throw new Exception("The method or operation is not implemented!");
            Console.WriteLine(4);
            Text1 = "The Text1 value of Test own.";
            Text2 = "The Text2 value of Test own.";
        }
        void OnProcess()
        {
            this.ProcessAction(this, EventArgs.Empty);
        }
        public string Process()
        {
            OnProcess();
            return Text1 + Text2;
        }
    }
    //callback
    public delegate string ProcessDelegate3(string str1, string str2);
    public class Testt
    {
        public string Process(string s1, string s2, ProcessDelegate3 process)
        {
            return process(s1, s2);
        }
    }
    public class Testt2
    {
        public string Process1(string s1, string s2)
        {
            return s1 + s2;
        }
        public string Process2(string s1, string s2)
        {
            return s1 + Environment.NewLine + s2;
        }
        public string Process3(string s1, string s2)
        {
            return s2 + s1;
        }
    }

}