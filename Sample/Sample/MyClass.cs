﻿using System;
using System.Diagnostics;
using NUnit.Framework;
using gdio.unity_api.v2;
using gdio.common.objects;

namespace DemoTest
{
    [TestFixture]
    public class UnitTest
    {
        /* These parameters can be used to override settings used to test when running from the NUnit command line
         * Example: mono ../../ext/nunit3-console.exe ./Sample.dll --testparam:Mode=standalone --testparam:pathToExe=/Users/user/Desktop/GameDriver_Demo.app/Contents/MacOS/GameDriver_Demo
         */
        public string testMode = TestContext.Parameters.Get("Mode", "IDE");
        public string pathToExe = TestContext.Parameters.Get("pathToExe", null); // replace null with the path to your executable as needed, or via the command line as shown above

        // Here we initialize the ApiClient
        ApiClient api;

        [OneTimeSetUp]
        public void Connect()
        {
            api = new ApiClient();

            try
            {
                // First we need to create an instance of the ApiClient

                // If an executable path was supplied, we will launch the standalone game
                if (pathToExe != null)
                {
                    ApiClient.Launch(pathToExe);
                    api.Wait(3000); // Give the executable time to load
                    api.Connect("localhost", 19734, false, 30);
                }

                // If no executable path was given, we will attempt to connect to the Unity editor and initiate Play mode
                else if (testMode == "IDE")
                {
                    api.Connect("localhost", 19734, true, 30);
                }
                // Otherwise, attempt to connect to an already playing game
                else api.Connect("localhost", 19734, false, 30);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            // Enable input hooking
            api.EnableHooks(HookingObject.ALL);

            // Give the application some time to load. Time for your app may vary.
            api.Wait(3000);
        }

        [Test]
        public void Test1()
        {
            // Do something. Example:
            /// Wait for a button to exist
            /// api.WaitForObject("//*[@name='Button']", 30);
            /// Click the button
            /// api.ClickObject(MouseButtons.LEFT, "//*[@name='Button']", 30);
            /// api.Wait(1000);
            /// Check that some text appeared
            /// Assert.AreEqual("Success", api.GetObjectFieldValue<string>("//*[@name='Text']/fn:component('TMPro.TextMeshProUGUI')/@text"), "Text doesn't match");
        }

        [Test]
        public void Test2()
        {
            // Do something else. Tests should be able to run independently after the steps in [OneTimeSetup] and can use try/catch blocks to avoid exiting prematurely on failure
        }

        [OneTimeTearDown]
        public void Disconnect()
        {
            // Disconnect the GameDriver client from the agent
            api.DisableHooks(HookingObject.ALL);
            api.Wait(1000);
            api.Disconnect();
            api.Wait(1000);
            // Comment out this line if you want to keep the editor in Play mode
            api.StopEditorPlay();
        }
    }
}
