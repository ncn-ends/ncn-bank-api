using Xunit;

namespace Tests.Collections;

[CollectionDefinition("SequentialTesting", DisableParallelization = true)]
public class SequentialCollection : ICollectionFixture<RandomizerData>
{ }