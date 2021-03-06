# Serials for .NET

## Project Description
Hello, I'm Jason Williams. Serials was created to provide a data type to manage things like serial numbers, order numbers, and other alpha-numeric string-based values.

## Download & Install

Nuget Package [Serials](https://www.nuget.org/packages/Serials/)

```powershell
Install-Package Serials
```
Minimum Requirements: **.NET Standard 2.0**.

## Usage
```csharp
var config = new SerialNumberConfiguration();
var sn = new SerialNumber("1A4RSLM", config);
sn.IncreaseBy(1);
var newSerialNumber = sn.ToString(); //1A4RSLN
```
### Alternate Encoders
By default, serial numbers use alpha numerics (letters and numbers / Base-36), but you can change that by using a different encoder.
```csharp
var config = new SerialNumberConfiguration(new Base10Encoder());
var sn = new SerialNumber("1000123", config);
sn.IncreaseBy(1);
var newSerialNumber = sn.ToString(); //1000124
```
Alternatively, you can also use a letters-only encoder called ```AlphabetEncoder``` if you require no numeric digits.

### Minimum Length
You can add a minimum length so serial numbers must be a set number of digits.
```csharp
var config = new SerialNumberConfiguration() { MinimumLength = 10 };
var sn = new SerialNumber("0001A4RSLM", config);
sn.IncreaseBy(1);
var newSerialNumber = sn.ToString(); //0001A4RSLN
```
The character used to pad serial numbers ('0' by default) can be set with the ```SerialNumberConfiguration.PadCharacter``` property.

### Prefixes
Many times certain types of numbers (invoice numbers for instance) require a static prefix be prepended at the front of the number. You can accomplish this with the ```SerialNumberConfiguration.Prefix``` property.
```csharp
var config = new SerialNumberConfiguration(new Base10Encoder())
{
    MinimumLength = 10,
    Prefix = "INV"
};
var sn = new SerialNumber("INV0000015879", config);
sn.IncreaseBy(1);
var newSerialNumber = sn.ToString(); //INV0000015880
```

## Other Notes
Very large serial numbers are supported. However, using large serial numbers can incurr a performance penalty.

If performance is a concern, try using the ```SmallSerialNumber``` class in place of the ```SerialNumber``` type. ```SmallSerialNumber``` has a maximum of approximately 18,446,744,073,709,551,615 (over 18.4 quintillion) numbers.