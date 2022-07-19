using Bogus;
using DataAccess.Models;
using Tests.Fixtures;
using Xunit;

namespace Tests.Collections;

public class RandomizerData
{
    private readonly Faker _faker = new();
    public readonly int number;

    public RandomizerData()
    {
        number = _faker.Random.Number(1, 1000000);
    }
}

[CollectionDefinition("RandomizerTesting", DisableParallelization = true)]
public class RandomizerTestingCollection : ICollectionFixture<RandomizerData>
{ }