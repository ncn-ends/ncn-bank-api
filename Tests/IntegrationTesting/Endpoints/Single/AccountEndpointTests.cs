using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using DataAccess;
using DataAccess.Access;
using DataAccess.Models;
using DataAccess.Setup;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq.Language.Flow;
using Tests.Helpers;
using Xunit;

namespace Tests.IntegrationTesting.Endpoints;

[Collection("SequentialTesting")]
public class AccountEndpointTests
{

    [Fact]
    public async Task TestAccountHolderEndpoints()
    {
        var waf = new CustomWAF<Program>();
        using var scope = waf.Services.CreateScope();
        var setupAccess = scope.ServiceProvider.GetRequiredService<ISetupAccess>();
        var holderAccess = scope.ServiceProvider.GetRequiredService<IAccountHolderAccess>();
        await setupAccess.EnsureDatabaseSetup();
        var randomHolder = await holderAccess.GetRandomOne();
        if (randomHolder is null) throw new Exception("Sample Account Holder wasn't created properly");

        var client = new HttpClientBroker("/api/account", injectedClient: waf.CreateClient());
        var sample = new AccountFormDTO
        {
            account_holder_id = randomHolder.account_holder_id,
            account_type_key = "stu_ca",
            initial_deposit = 10000
        };
        var postResponse = await client.SendPost(sample);
        postResponse.EnsureSuccessStatusCode();

        var postContent = await JsonMapper.MapHttpContentAs<AccountInsertionReturn>(postResponse);
        Debugger.Break();

        //
        // postContent.Should().NotBeNull();
        // postContent.account_holder_id.Should().NotBeEmpty();
        // postContent.account_holder_id.ToString().Should().Contain("-", Exactly.Times(4));

        // var holderResponse = await client.SendGet(route: $"/{postContent.account_holder_id}");
        // var getContent = await JsonMapper.MapHttpContentAs<AccountHolderBO>(holderResponse);
        // getContent.Should().NotBeNull();
        // getContent.account_holder_id.Should().Be(postContent.account_holder_id);
        // getContent.birthdate.Should().Be(sampleAccountHolderData.birthdate);
        // getContent.firstname.Should().Be(sampleAccountHolderData.firstname);
        // getContent.middlename.Should().Be(sampleAccountHolderData.middlename);
        // getContent.lastname.Should().Be(sampleAccountHolderData.lastname);
        // getContent.phone_number.Should().Be(sampleAccountHolderData.phone_number);
        // getContent.job_title.Should().Be(sampleAccountHolderData.job_title);
        // getContent.expected_salary.Should().Be(sampleAccountHolderData.expected_salary);
    }
}