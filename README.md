# LevelUp QR Code Listener

## Description
The goal of this project was to create a headless listener that was capable of intercepting and identifying LevelUp QR codes on Win32 machines regardless of which Windows application had focus.

I've included two sample applications, since the contexts are a bit different.

The first application is a simple, templated console application that consumes the library (see [usage](#usage)). 

The second application is a simple, templated Winforms application which also consumes the library.

The library wraps the unmanaged Windows API. By intercepting key strokes, we can detect whether or not a LevelUp QR code was scanned, extract that QR code, and pass it back to the consumer of this library via delegate method.

![](https://github.com/forestb/levelup-qr-code-listener/blob/master/readme-assets/example.png)

## Table of Contents
- [Installation](#Installation)
- [Usage](#Usage)
- [Contributing](#Contributing)
- [Credits](#Credits)
- [License](#License)

## Installation
This library is not (yet) available as a NuGet package; references must be added manually: `Project -> Add -> Reference`.

## Usage
```csharp
class Program
{
    static void Main(string[] args)
    {
        using (var listener = LevelUpQrCodeListener.Instance)
        {
            listener.StartListener(LevelUpPaymentTokenFound);

            Application.Run();

            listener.StopListener();
        }
    }
    
    private static void LevelUpPaymentTokenFound(string levelUpPaymentToken)
    {
        Console.WriteLine($"LevelUp QR code found: {levelUpPaymentToken}");
    }
}
```

## Contributing
N/A

## Credits
N/A

## License
This project is licensed under the MIT license.