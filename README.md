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
var sn = new SerialNumber("INV000123", config);
sn.IncreaseBy(1);
var newInvoiceNumber = sn.ToString(); //INV000124
```

## Other Notes
Very large numbers (serial numbers greater than 13 characters) are now supported. However, using large serial numbers can incurr a performance penalty.

If performance is a concern, you should use the ```SmallSerialNumber``` class in place of the ```SerialNumber``` type. ```SmallSerialNumber``` has a maximum supported value of ```"3W5E11264SGSF"```