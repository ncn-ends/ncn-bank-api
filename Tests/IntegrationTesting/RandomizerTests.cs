using Tests.Collections;
using Xunit;
using Xunit.Abstractions;

namespace Tests.IntegrationTesting;

[Collection("RandomizerTesting")]
public class RandomizerTests
{
    private readonly RandomizerData _randomizer;
    private readonly ITestOutputHelper _output;
    private int _count = 1;

    public RandomizerTests(RandomizerData randomizer, ITestOutputHelper output)
    {
        _randomizer = randomizer;
        _output = output;
    }

    [Fact]
    public void TestOne()
    {
        
        _output.WriteLine($"The randomizer id is: {_randomizer.number}");
        _output.WriteLine($"Count is {_count}");
        _count += 1;
    }
    
    [Fact]
    public void TestTwo()
    {
        
        _output.WriteLine($"The randomizer id is: {_randomizer.number}");
        _output.WriteLine($"Count is {_count}");
        _count += 1;
    }
    
    
    [Fact]
    public void TestThree()
    {
        
        _output.WriteLine($"The randomizer id is: {_randomizer.number}");
        _output.WriteLine($"Count is {_count}");
    }
}