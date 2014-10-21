/*
 * Name: Wait for Process Start
 * Author: Justin Yanke
 * Date: 10/21/14
 * Description: A program that merely waits for a process to fire an event elegantly.
 */

using System;
using System.Management; // Be sure to also add a reference to this using "Project -> Add Reference"

namespace prjWaitForProcessStart
{
    class Program
    {
        static void Main(string[] args)
        {
            string process = "notepad.exe";
            // You can remove the "and TargetInstance.Name..." part if you dont want to specify the TargetInstance.
            string conditions = "TargetInstance isa \"Win32_Process\" and TargetInstance.Name = '" + process + "'";

            var query = new WqlEventQuery("__InstanceCreationEvent", new TimeSpan(0, 0, 1), conditions);

            using (var watcher = new ManagementEventWatcher(query))
            {
                ManagementBaseObject e = watcher.WaitForNextEvent();

                // Our program we are watching for fired its event.
                Console.WriteLine((((ManagementBaseObject)e["TargetInstance"])["Name"]) + " has been initialiized!");

                watcher.Stop();
            }

            Console.ReadKey();
        }
    }
}