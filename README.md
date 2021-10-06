# GameDriver Sample Test

This project is intended to demonstrate the structure and usage of a GameDriver NUnit test for Unity. The project includes a Sample .NET 4.7.x project in Visual Studio with the NUnit 3 Test Adapter, NUnit Console Runner, and requires the latest version of the GameDriver API client which can be found at GameDriver.io/download

The test setup in this example includes techniques for running against the Unity Editor, standalone build on macOS or Windows, or running on a Mobile device. This is provided for example purposes only, and is not required in all testing. A basic setup can simply follow the form:

```
ApiClient api;

[OneTimeSetup]
public void Connect()
{
  api = new ApiClient();
  api.Connect(testHost, 19734, false, 30);
}
```

This template can be used for testing against the editor or a standelone build, and can be modified to include additional tests - simply add the test attribute:

```
[Test]
public void AnotherTest()
{
  // Do some more testing
}
```

Additional documentation can be found at on the [GameDriver Support](https://support.gamedriver.io) website.

Contributions are encouraged, and appreciated.

Visit GameDriver.io for more information.
